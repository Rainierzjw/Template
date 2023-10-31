/*******************************************************************************
* ��Ȩ���У��������������Ƽ����޹�˾
* �汾������v1.0.0
* ��Ŀ���ƣ�Com.Rainier.Buskit3D
* �� �� �ƣ�CommonBehaviour
* �������ڣ�2016/11/13 ���� 05:03:58
* ��������: ��־Զ
* ����������״̬������MonoBehaviour
* �޸ļ�¼��
* ���� ���� ���¹���
******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// ״̬������MonoBehaviour
    /// </summary>
	public class StateMachineRunner : MonoBehaviour
	{
        /// <summary>
        /// ״̬���б�
        /// </summary>
		private List<IStateMachine> stateMachineList = new List<IStateMachine>();

		/// <summary>
		/// ��ʼ�� 
		/// </summary>
		/// <typeparam name="T">״̬ת��ö��</typeparam>
		/// <param name="component">ά��״̬�����е�MonoBehaviour</param>
		/// <returns>״̬��</returns>
		public StateMachine<T> Initialize<T>(MonoBehaviour component) 
            where T : struct, IConvertible, IComparable
		{
			var fsm = new StateMachine<T>(this, component);
			stateMachineList.Add(fsm);
			return fsm;
		}

        /// <summary>
        /// ��ʼ�� 
        /// </summary>
        /// <typeparam name="T">״̬ת��ö��</typeparam>
        /// <param name="component">ά��״̬�����е�MonoBehaviour</param>
        /// <param name="startState">��ʼ״̬</param>
        /// <returns>״̬��</returns>
        public StateMachine<T> Initialize<T>(MonoBehaviour component, T startState) 
            where T : struct, IConvertible, IComparable
		{
			var fsm = Initialize<T>(component);
			fsm.ChangeState(startState);
			return fsm;
		}

        /// <summary>
        /// ִ��״̬����״̬����
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
        /// ִ��״̬����״̬����
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
        /// ִ��״̬����״̬����
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
        /// �ջص����������û���ָ���ص�����ʱ���ô˺���
        /// </summary>
		public static void DoNothing()
		{
            //�ջص����������û���ָ���ص�����ʱ���ô˺���
        }

        /// <summary>
        /// �ջص����������û���ָ���ص�����ʱ���ô˺���
        /// </summary>
        /// <param name="other">��ײ����</param>
        public static void DoNothingCollider(Collider other)
		{
            //�ջص����������û���ָ���ص�����ʱ���ô˺���
        }

        /// <summary>
        /// �ջص����������û���ָ���ص�����ʱ���ô˺���
        /// </summary>
        /// <param name="other">��ײ����</param>
        public static void DoNothingCollision(Collision other)
		{
            //�ջص����������û���ָ���ص�����ʱ���ô˺���
        }

        /// <summary>
        /// �ջص����������û���ָ���ص�����ʱ���ô˺���
        /// </summary>
        /// <returns></returns>
        public static IEnumerator DoNothingCoroutine()
		{
            //�ջص����������û���ָ���ص�����ʱ���ô˺���
            yield break;
		}
	}

	/// <summary>
    /// ״̬ӳ���
    /// </summary>
	public class StateMapping
	{
        /// <summary>
        /// ״̬����
        /// </summary>
		public object State;

        /// <summary>
        /// �Ƿ����Э��
        /// </summary>
		public bool HasEnterRoutine;

        /// <summary>
        /// ״̬����ص�
        /// </summary>
		public Action EnterCall = StateMachineRunner.DoNothing;

        /// <summary>
        /// ״̬����Э��
        /// </summary>
		public Func<IEnumerator> EnterRoutine = StateMachineRunner.DoNothingCoroutine;

        /// <summary>
        /// Э��ִ��������
        /// </summary>
		public bool HasExitRoutine;

        /// <summary>
        /// �˳��ص�
        /// </summary>
		public Action ExitCall = StateMachineRunner.DoNothing;

        /// <summary>
        /// �˳�Э��
        /// </summary>
		public Func<IEnumerator> ExitRoutine = StateMachineRunner.DoNothingCoroutine;

        /// <summary>
        /// ״̬Final����
        /// </summary>
		public Action Finally = StateMachineRunner.DoNothing;

        /// <summary>
        /// ״̬Update����
        /// </summary>
		public Action Update = StateMachineRunner.DoNothing;

        /// <summary>
        /// ״̬LateUpdate����
        /// </summary>
		public Action LateUpdate = StateMachineRunner.DoNothing;

        /// <summary>
        /// ״̬FixedUpdate����
        /// </summary>
		public Action FixedUpdate = StateMachineRunner.DoNothing;

        /// <summary>
        /// ��ײ����
        /// </summary>
		public Action<Collision> OnCollisionEnter = 
            StateMachineRunner.DoNothingCollision;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="state">״̬</param>
		public StateMapping(object state)
		{
			this.State = state;
		}
	}
}


