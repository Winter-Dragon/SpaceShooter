using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class AnimateSpriteFrames : AnimationBase
    {
        [SerializeField] private SpriteRenderer m_Renderer;
        [SerializeField] private Sprite[] m_Frames;

        protected override void AnimateFrame()
        {
            int frame = System.Convert.ToInt32(NormalizedAnimationTime * (m_Frames.Length - 1));
            m_Renderer.sprite = m_Frames[frame];
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