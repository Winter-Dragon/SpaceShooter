using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// ������� ����� ��������.
    /// </summary>
    public abstract class AnimationBase : MonoBehaviour
    {
        /// <summary>
        /// ������ ����� ��������.
        /// </summary>
        [SerializeField] protected float m_AnimatoinTime;
        /// <summary>
        /// �������� ������� ��������.
        /// </summary>
        [SerializeField] protected float m_AnimationScale;

        public void SetAnimationScale(float scale)
        {
            m_AnimationScale = scale;
        }

        /// <summary>
        /// ���� ������������� ��������.
        /// </summary>
        [SerializeField] private bool m_Looping;

        [SerializeField] private bool m_Reverse;

        /// <summary>
        /// ������ ����� �������� � ������ ���������.
        /// </summary>
        public float AnimationTime => m_AnimatoinTime / m_AnimationScale;

        /// <summary>
        /// ���������� ��������������� �������� �������� �� 0 �� 1.
        /// </summary>
        public float NormalizedAnimationTime
        {
            get
            {
                var t = Mathf.Clamp01(m_timer / m_AnimatoinTime);
                return m_Reverse ? (1.0f - t) : t;
            }
        }

        private float m_timer;
        private bool m_IsAnimationPlaying;

        [SerializeField] private UnityEvent m_EventStart;
        [SerializeField] private UnityEvent m_EventEnd;
        public UnityEvent OnEventEnd => m_EventEnd;

        #region UnityEvents

        private void Update()
        {
            if (!m_IsAnimationPlaying) return;

            m_timer += Time.deltaTime;

            AnimateFrame();

            if (m_timer > AnimationTime)
            {
                if (m_Looping)
                {
                    m_timer = 0;
                }
                else
                {
                    StopAnimation();
                }
            }
        }

        #endregion


        #region Public API

        /// <summary>
        /// ����� ������� ��������
        /// </summary>
        /// <param name="prepare"></param>
        public void StartAnimation(bool prepare = true)
        {
            if (m_IsAnimationPlaying) return;

            if (prepare) PrepareAnimation();

            m_IsAnimationPlaying = true;

            OnAnimationStart();

            m_EventStart?.Invoke();
        }

        /// <summary>
        /// ����� ������������ ��������.
        /// </summary>
        public void StopAnimation()
        {
            if (!m_IsAnimationPlaying) return;

            m_IsAnimationPlaying = false;

            OnAnimationEnd();

            m_EventEnd?.Invoke();
        }

        #endregion

        /// <summary>
        /// ��������� ������� ����� ��������.
        /// </summary>
        protected abstract void AnimateFrame();
        protected abstract void OnAnimationStart();
        protected abstract void OnAnimationEnd();
        /// <summary>
        /// ���������� ���������� ��������� ��������.
        /// </summary>
        public abstract void PrepareAnimation();
    }
}