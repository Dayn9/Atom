using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Atom
{
    public class AtomicSymbol : MonoBehaviour
    {
        [SerializeField] private Atom atom;
        [SerializeField] private Text nameUI;
        [SerializeField] private Text abbreviationUI;
        [SerializeField] private Text atomicNumberUI;
        [SerializeField] private Text massNumberUI;
        [SerializeField] private Text chargeUI;

        private void Update()
        {
            if(atom.Element != null)
            {
                nameUI.text = atom.Element.Name;
                abbreviationUI.text = atom.Element.Abbreviation;
            }
            else
            {
                nameUI.text = "";
                abbreviationUI.text = "";
            }
            
            atomicNumberUI.text = atom.Nucleus.ProtonCount > 0 ? atom.Nucleus.ProtonCount.ToString() : "";
            massNumberUI.text = atom.Nucleus.Mass > 0 ? atom.Nucleus.Mass.ToString() : "";

            int charge = atom.Nucleus.ProtonCount - atom.ElectronCount;
            if (charge > 0)
                chargeUI.text = charge + "+";
            else if (charge < 0)
                chargeUI.text = -charge + "-";
            else
                chargeUI.text = "";

        }
    }
}

