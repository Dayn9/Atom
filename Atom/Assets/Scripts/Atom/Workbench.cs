using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DUI;
using Atom;

[RequireComponent(typeof(DUIAnchor))]
public class Workbench : MonoBehaviour
{
    [SerializeField] private GameObject ProtonPrefab;
    [SerializeField] private GameObject NeutronPrefab;
    [SerializeField] private GameObject ElectronPrefab;

    private Atom.Atom atom;

    private void Awake()
    {
        atom = FindObjectOfType<Atom.Atom>();
        //make sure there are at least 3 children
        if (transform.childCount >= 3)
        {
            GameObject proton = Instantiate(ProtonPrefab, transform.GetChild(0));
            proton.transform.localPosition = Vector3.zero;

            GameObject neutron = Instantiate(NeutronPrefab, transform.GetChild(1));
            neutron.transform.localPosition = Vector3.zero;

            GameObject electron = Instantiate(ElectronPrefab, transform.GetChild(2));
            electron.transform.localPosition = Vector3.zero;
        }
    }

    public void NewProton()
    {
        Debug.Log("New Proton");

        GameObject obj = Instantiate(ProtonPrefab, transform.GetChild(0));

        Proton proton = obj.GetComponent<Proton>();
        if (proton != null)
        {
            proton.OnSelect?.Invoke();
        }
    }

    public void NewNeutron()
    {
        Debug.Log("New Neutron");

        GameObject obj = Instantiate(NeutronPrefab, transform.GetChild(1));

        Neutron neutron = obj.GetComponent<Neutron>();
        if (neutron != null)
        {
            neutron.OnSelect?.Invoke();
        }
    }

    public void NewElectron()
    {
        Debug.Log("New Electron");

        GameObject obj = Instantiate(ElectronPrefab, transform.GetChild(2));

        Electron electron = obj.GetComponent<Electron>();
        if (electron != null)
        {
            electron.OnSelect?.Invoke();
        }
    }
}

