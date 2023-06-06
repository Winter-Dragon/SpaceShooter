using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class AnimationSpriteColor : AnimationBase
    {
        [SerializeField] private SpriteRenderer m_Renderer;
        [SerializeField] private Color m_colorA;
        [SerializeField] private Color m_colorB;
        [SerializeField] private AnimationCurve m_Curve;

        protected override void AnimateFrame()
        {
            m_Renderer.color = Color.Lerp(m_colorA, m_colorB, m_Curve.Evaluate(NormalizedAnimationTime));
        }

        protected override void OnAnimationEnd()
        {
            
        }

        protected override void OnAnimationStart()
        {
            
        }

        public override void PrepareAnimation()
        {
            
        }
    }
}