using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DUI
{

    [RequireComponent(typeof(DUIAnchor))]
    public class DUIButton : MonoBehaviour
    {
        private DUIAnchor anchor; //ref to spriteRenderer component

        private bool active = true;
        private bool mouseOver = false;

        public UnityEvent OnEnter;
        public UnityEvent OnClick;
        public UnityEvent OnExit;

        protected virtual void Awake()
        {
            anchor = GetComponent<DUIAnchor>();

            OnEnter.AddListener(Enter);
            OnExit.AddListener(Exit);
        }

        protected virtual void Update()
        {
            if (active)
            {
                bool over = anchor.Rect.Contains(DUI.mousePos);
                if (!mouseOver && over)
                {
                    OnEnter?.Invoke();
                }
                else if (mouseOver && !over)
                {
                    OnExit?.Invoke();
                }
                if (mouseOver && Input.GetMouseButtonDown(0))
                {
                    OnClick?.Invoke();
                }
            }
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
