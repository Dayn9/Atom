using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DUIAnchor))]
public class Workbench : MonoBehaviour
{
    [SerializeField] private GameObject ProtonPrefab;

    private void Awake()
    {
        //make sure there are at least 3 children
        if(transform.childCount >= 3)
        {
            GameObject proton = Instantiate(ProtonPrefab, transform.GetChild(0));
            proton.transform.localPosition = Vector3.zero;

            GameObject neutron = Instantiate(ProtonPrefab, transform.GetChild(1));
            proton.transform.localPosition = Vector3.zero;

            GameObject electron = Instantiate(ProtonPrefab, transform.GetChild(2));
            proton.transform.localPosition = Vector3.zero;
        }
    }

    private void Start()
    {
       
    }
}
