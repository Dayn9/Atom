using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DUI
{
    [RequireComponent(typeof(DUIAnchor))]
    public class DUIButton : MonoBehaviour
    {
        /// <summary>
        /// sends out events when mouse enters, clicks, and exits DUIanchor bounds
        /// </summary>

        private DUIAnchor anchor; //ref to spriteRenderer component

        private bool active = true; //true when the button is able to be clicked 
        private bool mouseOver = false; //true when the mouse is over bounds

        public UnityEvent OnEnter; //called when the mouse enters bounds
        public UnityEvent OnClick; //called when the mouse clicks while in bounds
        public UnityEvent OnExit; //called when the mouse exits bounds

        protected virtual void Awake()
        {
            anchor = GetComponent<DUIAnchor>();

            OnEnter.AddListener(Enter);
            OnExit.AddListener(Exit);
        }

        protected virtual void Update()
        {
            //only check for events when active
            if (active)
            {
                bool over = anchor.Bounds.Contains(DUI.mousePos);

                //call enter when mouse is first over
                if (!mouseOver && over)
                {
                    OnEnter?.Invoke();
                }
                //call exit when mouse first leaves
                else if (mouseOver && !over)
                {
                    OnExit?.Invoke();
                }
                //call click when over and mouse down
                if (mouseOver && Input.GetMouseButtonDown(0))
                {
                    OnClick?.Invoke();
                }
            }
            //call exit if disabled with mouse over
            else if (mouseOver)
            {
                OnExit?.Invoke();
            }
        }

        private void Enter()
        {
            mouseOver = true;
        }

        private void Exit()
        {
            mouseOver = false;
        }
    }
}
