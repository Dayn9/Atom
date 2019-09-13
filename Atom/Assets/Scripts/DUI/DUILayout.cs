using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DUIAnchor))]
public class DUILayout : MonoBehaviour
{
    public enum DUILayoutType { Horizontal, Vertical /*, Grid */ }

    private DUIAnchor anchor;

    [SerializeField] private DUILayoutType layout;

    private void Awake()
    {
        anchor = GetComponent<DUIAnchor>();
        DUIAnchor[] duias = GetComponentsInChildren<DUIAnchor>();

        Vector2 offset = new Vector2(layout == DUILayoutType.Horizontal ? 1.0f / (duias.Length - 1) : 0,
                                     layout == DUILayoutType.Vertical ? 1.0f / (duias.Length - 1) : 0);

        for(int i = 1; i < duias.Length; i++)
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
