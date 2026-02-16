using System;
using UnityEngine;

namespace Tips.Part_6_End
{
    /// <summary>
    /// DoorController encapsulates door state and animation triggering.
    /// It exposes simple commands (Open/Close/Toggle) and a read-only
    /// AnimationDone flag to gate interactions until animations complete.
    /// </summary>
    public class DoorController : MonoBehaviour
    {
        [SerializeField]
        private Animator m_animator;

        // Gates interaction while the open/close animation is playing.
        // DoorInteractable reads this (Article 6) and will not re-trigger
        // while AnimationDone == false to avoid overlapping animations.
        public bool AnimationDone { get; private set; } = true;
        private bool m_isDoorClosed = true;
        
        public void Open()
        {
            m_animator.ResetTrigger("Close");
            m_animator.SetTrigger("Open");
            m_isDoorClosed = false;
            AnimationDone = false;
        }

        public void Close()
        {
            m_animator.ResetTrigger("Open");
            m_animator.SetTrigger("Close");
            m_isDoorClosed = true;
            AnimationDone = false;
        }

        public void FinishAnimation()
        {
            AnimationDone = true;
        }

        // Public command used by interaction code; keeps caller unaware of
        // animation details (encapsulation).
        public void Toggle()
        {
            if(m_isDoorClosed)
                Open();
            else
                Close();
        }
    }
}
