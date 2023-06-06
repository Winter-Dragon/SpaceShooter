using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class AnimationContainerParallel : AnimationBase
    {
        /// <summary>
        /// Последовательные анимации.
        /// </summary>
        [SerializeField] private AnimationBase[] m_Animations;

        public override void PrepareAnimation()
        {
            m_AnimatoinTime = 0;

            foreach (var v in m_Animations)
            {
                v.SetAnimationScale(m_AnimationScale);
                m_AnimatoinTime += Mathf.Max(m_AnimatoinTime, v.AnimationTime);

                v.PrepareAnimation();
            }
        }

        protected override void AnimateFrame()
        {
            
        }

        protected override void OnAnimationEnd()
        {
            
        }

        protected override void OnAnimationStart()
        {
            foreach(var v in m_Animations)
            {
                v.StartAnimation();
            }
        }
    }
}