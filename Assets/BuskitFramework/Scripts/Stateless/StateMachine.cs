/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：StateMachine
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：简单的状态机实现
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = System.Object;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 状态转移
    /// </summary>
	public enum StateTransition
	{
		/// <summary>
		/// 等待携程结束，切换状态
		/// </summary>
		Safe,
		/// <summary>
		/// 停止所有携程，直接切换状态
		/// </summary>
		Overwrite,
	}

    /// <summary>
    /// 状态机接口
    /// </summary>
	public interface IStateMachine
	{
        /// <summary>
        /// 状态机所属MonoBehaviour
        /// </summary>
		MonoBehaviour Component { get; }

        /// <summary>
        /// 状态机映射表
        /// </summary>
		StateMapping CurrentStateMap { get; }

        /// <summary>
        /// 实在转移状态吗？
        /// </summary>
		bool IsInTransition { get; }
	}

    /// <summary>
    /// 简单的状态机实现
    /// </summary>
    /// <typeparam name="T">状态</typeparam>
	public class StateMachine<T> : IStateMachine where T : struct, IConvertible, IComparable
	{
        /// <summary>
        /// 回调函数表
        /// </summary>
		public event Action<T> Changed;

        /// <summary>
        /// 状态机运行入口
        /// </summary>
		private StateMachineRunner engine;

        /// <summary>
        /// 所属MonoBehaviour
        /// </summary>
		private MonoBehaviour component;

        /// <summary>
        /// 状态映射表
        /// </summary>
		private StateMapping lastState;

        /// <summary>
        /// 当前状态
        /// </summary>
		private StateMapping currentState;

        /// <summary>
        /// 目标状态
        /// </summary>
		private StateMapping destinationState;

        /// <summary>
        /// 状态搜索表
        /// </summary>
		private Dictionary<object, StateMapping> stateLookup;

        /// <summary>
        /// 忽略名称
        /// </summary>
		private readonly string[] ignoredNames = new[] { "add", "remove", "get", "set" };

        /// <summary>
        /// 是否正在转移
        /// </summary>
		private bool isInTransition = false;

        /// <summary>
        /// 状态转移协程
        /// </summary>
		private IEnumerator currentTransition;
		private IEnumerator exitRoutine;
		private IEnumerator enterRoutine;
		private IEnumerator queuedChange;

		/// <summary>
		/// /构造函数
		/// </summary>
		/// <param name="engine">状态更新组件</param>
		/// <param name="component">MonoBehaviour组件</param>
		public StateMachine(StateMachineRunner engine, MonoBehaviour component)
		{
			this.engine = engine;
			this.component = component;

			//Define States
			var values = Enum.GetValues(typeof(T));
			if (values.Length < 1) { throw new ArgumentException("Enum provided to Initialize must have at least 1 visible definition"); }

			stateLookup = new Dictionary<object, StateMapping>();
			for (int i = 0; i < values.Length; i++)
			{
				var mapping = new StateMapping((Enum) values.GetValue(i));
				stateLookup.Add(mapping.State, mapping);
			}

			//反射所有方法
			var methods = component.GetType().GetMethods(
                BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public |
									  BindingFlags.NonPublic);

			//绑定方法到函数
			var separator = "_".ToCharArray();
			for (int i = 0; i < methods.Length; i++)
			{
				if (methods[i].GetCustomAttributes(
                    typeof(CompilerGeneratedAttribute), true).Length != 0)
				{
					continue;
				}
				var names = methods[i].Name.Split(separator);
				if (names.Length <= 1)
				{
					continue;
				}

				Enum key;
				try
				{
					key = (Enum) Enum.Parse(typeof(T), names[0]);
				}
				catch (ArgumentException)
				{
					continue;
				}

				var targetState = stateLookup[key];

				switch (names[1])
				{
					case "Enter":
						if (methods[i].ReturnType == typeof(IEnumerator))
						{
							targetState.HasEnterRoutine = true;
							targetState.EnterRoutine = CreateDelegate<Func<IEnumerator>>(methods[i], component);
						}
						else
						{
							targetState.HasEnterRoutine = false;
							targetState.EnterCall = CreateDelegate<Action>(methods[i], component);
						}
						break;
					case "Exit":
						if (methods[i].ReturnType == typeof(IEnumerator))
						{
							targetState.HasExitRoutine = true;
							targetState.ExitRoutine = CreateDelegate<Func<IEnumerator>>(methods[i], component);
						}
						else
						{
							targetState.HasExitRoutine = false;
							targetState.ExitCall = CreateDelegate<Action>(methods[i], component);
						}
						break;
					case "Finally":
						targetState.Finally = CreateDelegate<Action>(methods[i], component);
						break;
					case "Update":
						targetState.Update = CreateDelegate<Action>(methods[i], component);
						break;
					case "LateUpdate":
						targetState.LateUpdate = CreateDelegate<Action>(methods[i], component);
						break;
					case "FixedUpdate":
						targetState.FixedUpdate = CreateDelegate<Action>(methods[i], component);
						break;
					case "OnCollisionEnter":
						targetState.OnCollisionEnter = CreateDelegate<Action<Collision>>(methods[i], component);
						break;
				}
			}
			currentState = new StateMapping(null);
		}

        /// <summary>
		/// 创建代理
		/// </summary>
		/// <typeparam name="V">委托类型</typeparam>
		/// <param name="method">回调方法</param>
		/// <param name="target">目标对象</param>
		/// <returns></returns>
		private V CreateDelegate<V>(MethodInfo method, Object target) where V : class
		{
			var ret = (Delegate.CreateDelegate(typeof(V), target, method) as V);

			if (ret == null)
			{
				throw new ArgumentException("Unabled to create delegate for method called " + method.Name);
			}
			return ret;
		}

        /// <summary>
        /// 创建状态
        /// </summary>
        /// <param name="newState">新状态</param>
		public void ChangeState(T newState)
		{
			ChangeState(newState, StateTransition.Safe);
		}

        /// <summary>
        /// 创建状态
        /// </summary>
        /// <param name="newState">新状态</param>
        /// <param name="transition">状态转移类型</param>
		public void ChangeState(T newState, StateTransition transition)
		{
			if (stateLookup == null)
			{
				throw new Buskit3DException(
                    "States have not been configured, please call initialized before trying to set State");
			}

			if (!stateLookup.ContainsKey(newState))
			{
				throw new Buskit3DException(
                    "No State with the name " + newState.ToString() + " can be found. Please make sure you are called the correct type the statemachine was initialized with");
			}

			var nextState = stateLookup[newState];
            if (currentState == nextState)
            {
                return;
            }

			if (queuedChange != null)
			{
				engine.StopCoroutine(queuedChange);
				queuedChange = null;
			}

			switch (transition)
			{
				case StateTransition.Safe:
					if (isInTransition)
					{
						if (exitRoutine != null) 
						{
							destinationState = nextState;
							return;
						}

						if (enterRoutine != null) 
						{
							queuedChange = WaitForPreviousTransition(nextState);
							engine.StartCoroutine(queuedChange);
							return;
						}
					}
					break;
				case StateTransition.Overwrite:
					if (currentTransition != null)
					{
						engine.StopCoroutine(currentTransition);
					}
					if (exitRoutine != null)
					{
						engine.StopCoroutine(exitRoutine);
					}
					if (enterRoutine != null)
					{
						engine.StopCoroutine(enterRoutine);
					}
					break;
			}

			if ((currentState != null && currentState.HasExitRoutine) || nextState.HasEnterRoutine)
			{
				isInTransition = true;
				currentTransition = ChangeToNewStateRoutine(nextState, transition);
				engine.StartCoroutine(currentTransition);
			}
            else
            {
				if (currentState != null)
				{
					currentState.ExitCall();
					currentState.Finally();
				}

				lastState = currentState;
				currentState = nextState;
				if (currentState != null)
				{
					currentState.EnterCall();
					if (Changed != null)
					{
						Changed((T) currentState.State);
					}
				}
				isInTransition = false;
			}
		}

        /// <summary>
        /// 创建新的状态协程
        /// </summary>
        /// <param name="newState">状态映射表</param>
        /// <param name="transition">状态转移类型</param>
        /// <returns></returns>
		private IEnumerator ChangeToNewStateRoutine(
            StateMapping newState, StateTransition transition)
		{
			destinationState = newState; 

			if (currentState != null)
			{
				if (currentState.HasExitRoutine)
				{
					exitRoutine = currentState.ExitRoutine();

					if (exitRoutine != null && transition != StateTransition.Overwrite) 
					{
						yield return engine.StartCoroutine(exitRoutine);
					}

					exitRoutine = null;
				}
				else
				{
					currentState.ExitCall();
				}

				currentState.Finally();
			}

			lastState = currentState;
			currentState = destinationState;

			if (currentState != null)
			{
				if (currentState.HasEnterRoutine)
				{
					enterRoutine = currentState.EnterRoutine();

					if (enterRoutine != null)
					{
						yield return engine.StartCoroutine(enterRoutine);
					}

					enterRoutine = null;
				}
				else
				{
					currentState.EnterCall();
				}
				if (Changed != null)
				{
					Changed((T) currentState.State);
				}
			}
			isInTransition = false;
		}

        /// <summary>
        /// 等待前一个转移结束
        /// </summary>
        /// <param name="nextState">状态映射表</param>
        /// <returns></returns>
		IEnumerator WaitForPreviousTransition(StateMapping nextState)
		{
			while (isInTransition)
			{
				yield return null;
			}

			ChangeState((T) nextState.State);
		}

        /// <summary>
        /// 终态
        /// </summary>
		public T LastState
		{
			get
			{
				if (lastState == null) return default(T);

				return (T) lastState.State;
			}
		}

        /// <summary>
        /// 状态
        /// </summary>
		public T State
		{
			get { return (T) currentState.State; }
		}

        /// <summary>
        /// 是处于转移过程中吗？
        /// </summary>
		public bool IsInTransition
		{
			get
            {
                return isInTransition;
            }
		}

        /// <summary>
        /// 当前状态
        /// </summary>
		public StateMapping CurrentStateMap
		{
			get
            {
                return currentState;
            }
		}

        /// <summary>
        /// 所属MonoBehaviour
        /// </summary>
		public MonoBehaviour Component
		{
			get
            {
                return component;
            }
		}

		/// <summary>
		/// 初始化状态机
		/// </summary>
		/// <param name="component">所属MonoBehaviour</param>
		/// <returns>状态机</returns>
		public static StateMachine<T> Initialize(MonoBehaviour component)
		{
			var engine = component.GetComponent<StateMachineRunner>();
			if (engine == null) engine = component.gameObject.AddComponent<StateMachineRunner>();

			return engine.Initialize<T>(component);
		}

        /// <summary>
        /// 初始化状态机并指定初始状态
        /// </summary>
        /// <param name="component">所属MonoBehaviour</param>
        /// <param name="startState">初始状态</param>
        /// <returns>状态机</returns>
        public static StateMachine<T> Initialize(MonoBehaviour component, T startState)
		{
			var engine = component.GetComponent<StateMachineRunner>();
            if (engine == null)
            {
                engine = component.gameObject.AddComponent<StateMachineRunner>();
            }
			return engine.Initialize<T>(component, startState);
		}
	}

}
