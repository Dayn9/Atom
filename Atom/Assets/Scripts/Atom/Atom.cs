using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DUI;

namespace Atom
{
    public class Atom : MonoBehaviour
    {
        /// <summary>
        /// Controls the atom
        /// </summary>

        private Nucleus nucleus;

        public Nucleus Nucleus { get { return nucleus; } }

        private void Awake()
        {
            nucleus = GetComponentInChildren<Nucleus>();
        }
    }
}

