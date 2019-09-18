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

        private Nucleus nucleus;
        private Stack<Shell> shells;
        private DUIAnchor anchor;

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
            anchor = GetComponent<DUIAnchor>();
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

        public bool Contains(Vector2 pos)
        {
            return anchor.Bounds.Contains(pos);
        }

        public bool RemoveElectron(Particle particle)
        {
            //remove the Outer shell if empty, next shell is now the Outer shell
            if (OuterShell.Empty && OuterShell.NextShell != null)
                RemoveShell();

            return OuterShell.RemoveParticle(particle);
        }

        private void RemoveShell()
        {
            Destroy(shells.Pop().gameObject);
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
            shells.Push(shell);
        }

    }
}

