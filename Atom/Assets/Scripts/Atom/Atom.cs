using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DUI;

namespace Atom
{
    [RequireComponent(typeof(DUIAnchor))]
    public class Atom : MonoBehaviour
    {
        /// <summary>
        /// Controls the atom
        /// </summary>

        private Nucleus nucleus; //ref to the Atom's nucleus
        private Stack<Shell> shells; //stack of electron shells
        private DUIAnchor anchor; //ref to own DUI anchor

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
        [SerializeField] private float nucleusRadius; //TODO replace with actual calculation
        [SerializeField] private float spacing; //spacing between electron shells 

        private void Awake()
        {
            anchor = GetComponent<DUIAnchor>();
            nucleus = GetComponentInChildren<Nucleus>();
            shells = new Stack<Shell>();

            //add the first shell
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

        /// <summary>
        /// check if position is within the bounds of the Atom
        /// </summary>
        /// <param name="pos">position to check</param>
        /// <returns>true when pos in anchor bounds</returns>
        public bool Contains(Vector2 pos)
        {
            return anchor.Bounds.Contains(pos);
        }

        /// <summary>
        /// try to Remove a specified electron from the atom
        /// </summary>
        /// <param name="particle">particle to remove</param>
        /// <returns>removal scucess</returns>
        public bool RemoveElectron(Particle particle)
        {
            //remove the Outer shell if empty, next shell is now the Outer shell
            if (OuterShell.Empty && OuterShell.NextShell != null)
                RemoveShell();

            //start the recursive call to remove from outer shell
            return OuterShell.RemoveParticle(particle);
        }

        /// <summary>
        /// Remove and Destroy the Outer shell
        /// </summary>
        private void RemoveShell()
        {
            Destroy(shells.Pop().gameObject);
        }

        /// <summary>
        /// Add a new outer shell
        /// </summary>
        private void AddShell()
        {
            //create a new shell object
            GameObject obj = Instantiate(shellTemplate, transform);
            obj.SetActive(true);
            obj.transform.localPosition = Vector3.zero;

            //add the new shell to the stack
            Shell shell = obj.GetComponent<Shell>();

            //set attributes based on shell layer
            shell.radius = (shells.Count * spacing) + nucleusRadius;
            if (shells.Count == 0)
            {
                shell.MaxParticles = 2;
                shell.NextShell = null;
            }
            else
            {
                shell.MaxParticles = 8;
                shell.NextShell = OuterShell;
            }

            //push shell onto stack
            shells.Push(shell);
        }

    }
}

