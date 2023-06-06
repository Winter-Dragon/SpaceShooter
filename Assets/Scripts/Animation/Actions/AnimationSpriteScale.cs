using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class AnimationSpriteScale : AnimationBase
    {
        [SerializeField] private SpriteRenderer m_Renderer;

        [SerializeField] private AnimationCurve m_CurveX;
        [SerializeField] private AnimationCurve m_CurveY;

        private Vector2 m_InitialSize;

        private void Start()
        {
            m_InitialSize = m_Renderer.size;
        }

        public override void PrepareAnimation()
        {
            var x = m_CurveX.Evaluate(0) * m_InitialSize.x;
            var y = m_CurveY.Evaluate(0) * m_InitialSize.y;

            m_Renderer.size = new Vector2(x, y);
        }

        protected override void AnimateFrame()
        {
            var x = m_CurveX.Evaluate(NormalizedAnimationTime) * m_InitialSize.x;
            var y = m_CurveY.Evaluate(NormalizedAnimationTime) * m_InitialSize.y;

            m_Renderer.size = new Vector2(x, y);
        }

        protected override void OnAnimationEnd()
        {
            
        }

        protected override void OnAnimationStart()
        {
            
        }
    }
}