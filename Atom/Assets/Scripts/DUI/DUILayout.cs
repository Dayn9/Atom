using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DUI
{
    [RequireComponent(typeof(DUIAnchor))]
    public class DUILayout : MonoBehaviour
    {
        /// <summary>
        /// Calculates anchor min max values for children to layout Anchors in row
        /// </summary>

        public enum DUILayoutType { Horizontal, Vertical /*, Grid */ }

        private DUIAnchor anchor; //ref to attached DUIAnchor

        [SerializeField] private DUILayoutType layout;

        private void Awake()
        {
            anchor = GetComponent<DUIAnchor>();

            DUIAnchor[] duias = GetComponentsInChildren<DUIAnchor>();

            //calculate the offset between anchors
            Vector2 offset = new Vector2(layout == DUILayoutType.Horizontal ? 1.0f / (duias.Length - 1) : 0,
                                         layout == DUILayoutType.Vertical ? 1.0f / (duias.Length - 1) : 0);

            //set the min and max of every anchor based on anchor
            for (int i = 1; i < duias.Length; i++)
            {
                switch (layout)
                {
                    case DUILayoutType.Vertical:
                        duias[i].SetMinMax(Vector2.up - (i - 1) * offset, Vector2.one - (i * offset));
                        break;
                    case DUILayoutType.Horizontal:
                        duias[i].SetMinMax((i - 1) * offset, (i * offset) + Vector2.up);
                        break;
                }
            }
        }
    }
}
