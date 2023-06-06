using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Скрипт проигрывания набора анимация последовательно.
    /// </summary>
    public class AnimationContainerSequential : AnimationBase
    {
        /// <summary>
        /// Последовательные анимации.
        /// </summary>
        [SerializeField] private AnimationBase[] m_Animations;

        protected override void AnimateFrame()
        {
            
        }

        protected override void OnAnimationEnd()
        {
            
        }

        private int m_CurrentSubAnimation;

        protected override void OnAnimationStart()
        {
            m_CurrentSubAnimation = 0;

            m_Animations[m_CurrentSubAnimation].OnEventEnd.AddListener(OnSubAnimationEnded);
            m_Animations[m_CurrentSubAnimation].StartAnimation();
        }

        private void OnSubAnimationEnded()
        {
            m_Animations[m_CurrentSubAnimation].OnEventEnd.RemoveListener(OnSubAnimationEnded);

            m_CurrentSubAnimation++;

            if (m_CurrentSubAnimation < m_Animations.Length)
            {
                m_Animations[m_CurrentSubAnimation].OnEventEnd.AddListener(OnSubAnimationEnded);
                m_Animations[m_CurrentSubAnimation].StartAnimation();
            }
            else
            {
                StopAnimation();
            }
        }

        public override void PrepareAnimation()
        {
            m_AnimatoinTime = 0;

            foreach(var v in m_Animations)
            {
                v.SetAnimationScale(m_AnimationScale);
                m_AnimatoinTime += v.AnimationTime;

                v.PrepareAnimation();
            }
        }
    }
}