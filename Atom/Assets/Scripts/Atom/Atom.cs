﻿using System.Collections;
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
        private Stack<Shell> shells;

        public Nucleus Nucleus { get { return nucleus; } }
        public Shell OuterShell { get { return shells.Peek(); } }
        public int ElectronCount
        {
            get
            {
                int e = 0;
                foreach (Shell shell in shells)
                    e += shell.ElectronCount;
                return e;
            }
        }

        [SerializeField] private GameObject shellTemplate;
        [SerializeField] private float nucleusRadius;
        [SerializeField] private float spacing;

        private void Awake()
        {
            nucleus = GetComponentInChildren<Nucleus>();
            shells = new Stack<Shell>();

            AddShell();
        }

        private void Update()
        {
            //check if the outermost shell is full
            if (OuterShell.Full && shells.Count < 3)
            {
                AddShell();
            }
        }

        private void AddShell()
        {
            //create a new shell object
            GameObject obj = Instantiate(shellTemplate, transform);
            obj.SetActive(true);
            obj.transform.localPosition = Vector3.zero;

            //add the new shell to the stack
            Shell shell = obj.GetComponent<Shell>();
            shell.radius = (shells.Count * spacing) + nucleusRadius;
            shell.MaxParticles = shells.Count == 0 ? 2 : 8;
            shells.Push(shell);
        }
    }
}

