using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DUI;

namespace Atom
{
    [RequireComponent(typeof(DUIAnchor))]
    public class Atom : MonoBehaviour
    {
        private Nucleus nucleus;

        public Nucleus Nucleus { get { return nucleus; } }

        private void Awake()
        {
            nucleus = GetComponentInChildren<Nucleus>();
        }
    }
}

