/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：CommonBehaviour
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：状态机运行MonoBehaviour
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 状态机运行MonoBehaviour
    /// </summary>
	public class StateMachineRunner : MonoBehaviour
	{
        /// <summary>
        /// 状态机列表
        /// </summary>
		private List<IStateMachine> stateMachineList = new List<IStateMachine>();

		/// <summary>
		/// 初始化 
		/// </summary>
		/// <typeparam name="T">状态转移枚举</typeparam>
		/// <param name="component">维护状态机运行的MonoBehaviour</param>
		/// <returns>状态机</returns>
		public StateMachine<T> Initialize<T>(MonoBehaviour component) 
            where T : struct, IConvertible, IComparable
		{
			var fsm = new StateMachine<T>(this, component);
			stateMachineList.Add(fsm);
			return fsm;
		}

        /// <summary>
        /// 初始化 
        /// </summary>
        /// <typeparam name="T">状态转移枚举</typeparam>
        /// <param name="component">维护状态机运行的MonoBehaviour</param>
        /// <param name="startState">初始状态</param>
        /// <returns>状态机</returns>
        public StateMachine<T> Initialize<T>(MonoBehaviour component, T startState) 
            where T : struct, IConvertible, IComparable
		{
			var fsm = Initialize<T>(component);
			fsm.ChangeState(startState);
			return fsm;
		}

        /// <summary>
        /// 执行状态机的状态更新
        /// </summary>
		void FixedUpdate()
		{
			for (int i = 0; i < stateMachineList.Count; i++)
			{
				var fsm = stateMachineList[i];
                if (!fsm.IsInTransition && fsm.Component.enabled)
                {
                    fsm.CurrentStateMap.FixedUpdate();
                }
			}
		}

        /// <summary>
        /// 执行状态机的状态更新
        /// </summary>
		void Update()
		{
			for (int i = 0; i < stateMachineList.Count; i++)
			{
				var fsm = stateMachineList[i];
				if (!fsm.IsInTransition && fsm.Component.enabled)
				{
					fsm.CurrentStateMap.Update();
				}
			}
		}

        /// <summary>
        /// 执行状态机的状态更新
        /// </summary>
		void LateUpdate()
		{
			for (int i = 0; i < stateMachineList.Count; i++)
			{
				var fsm = stateMachineList[i];
				if (!fsm.IsInTransition && fsm.Component.enabled)
				{
					fsm.CurrentStateMap.LateUpdate();
				}
			}
		}

        /// <summary>
        /// 空回调函数，当用户不指定回调函数时调用此函数
        /// </summary>
		public static void DoNothing()
		{
            //空回调函数，当用户不指定回调函数时调用此函数
        }

        /// <summary>
        /// 空回调函数，当用户不指定回调函数时调用此函数
        /// </summary>
        /// <param name="other">碰撞对象</param>
        public static void DoNothingCollider(Collider other)
		{
            //空回调函数，当用户不指定回调函数时调用此函数
        }

        /// <summary>
        /// 空回调函数，当用户不指定回调函数时调用此函数
        /// </summary>
        /// <param name="other">碰撞对象</param>
        public static void DoNothingCollision(Collision other)
		{
            //空回调函数，当用户不指定回调函数时调用此函数
        }

        /// <summary>
        /// 空回调函数，当用户不指定回调函数时调用此函数
        /// </summary>
        /// <returns></returns>
        public static IEnumerator DoNothingCoroutine()
		{
            //空回调函数，当用户不指定回调函数时调用此函数
            yield break;
		}
	}

	/// <summary>
    /// 状态映射表
    /// </summary>
	public class StateMapping
	{
        /// <summary>
        /// 状态对象
        /// </summary>
		public object State;

        /// <summary>
        /// 是否进入协程
        /// </summary>
		public bool HasEnterRoutine;

        /// <summary>
        /// 状态进入回调
        /// </summary>
		public Action EnterCall = StateMachineRunner.DoNothing;

        /// <summary>
        /// 状态进入协程
        /// </summary>
		public Func<IEnumerator> EnterRoutine = StateMachineRunner.DoNothingCoroutine;

        /// <summary>
        /// 协程执行完了吗？
        /// </summary>
		public bool HasExitRoutine;

        /// <summary>
        /// 退出回调
        /// </summary>
		public Action ExitCall = StateMachineRunner.DoNothing;

        /// <summary>
        /// 退出协程
        /// </summary>
		public Func<IEnumerator> ExitRoutine = StateMachineRunner.DoNothingCoroutine;

        /// <summary>
        /// 状态Final方法
        /// </summary>
		public Action Finally = StateMachineRunner.DoNothing;

        /// <summary>
        /// 状态Update方法
        /// </summary>
		public Action Update = StateMachineRunner.DoNothing;

        /// <summary>
        /// 状态LateUpdate方法
        /// </summary>
		public Action LateUpdate = StateMachineRunner.DoNothing;

        /// <summary>
        /// 状态FixedUpdate方法
        /// </summary>
		public Action FixedUpdate = StateMachineRunner.DoNothing;

        /// <summary>
        /// 碰撞进入
        /// </summary>
		public Action<Collision> OnCollisionEnter = 
            StateMachineRunner.DoNothingCollision;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="state">状态</param>
		public StateMapping(object state)
		{
			this.State = state;
		}
	}
}


