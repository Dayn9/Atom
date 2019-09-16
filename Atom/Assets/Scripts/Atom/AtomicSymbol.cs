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
            nameUI.text = Elements.GetName(atom.Nucleus.ProtonCount);
            abbreviationUI.text = Elements.GetAbbreviation(atom.Nucleus.ProtonCount);
            atomicNumberUI.text = atom.Nucleus.ProtonCount.ToString();
            massNumberUI.text = (atom.Nucleus.ProtonCount + atom.Nucleus.NeutronCount).ToString();

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

