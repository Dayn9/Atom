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

        [SerializeField] private GameObject shellTemplate;
        [SerializeField] private Workbench workbench;
        [SerializeField] private float nucleusRadius; //TODO replace with actual calculation
        [SerializeField] private float spacing; //spacing between electron shells 
        [SerializeField] private float seperateSpeed; //speed particles fly away at
        private Stack<Shell> shells; //stack of electron shells
        private DUIAnchor anchor; //ref to own DUI anchor
        private List<Particle> excessParticles; //particles that are not part of the atom 

        public Nucleus Nucleus { get; private set; }
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
        public Element Element { get; private set; }
        

        private void Awake()
        {
            anchor = GetComponent<DUIAnchor>();
            Nucleus = GetComponentInChildren<Nucleus>();
            shells = new Stack<Shell>();

            excessParticles = new List<Particle>();

            //add the first shell
            AddShell();
        }

        private void Update()
        {
            //get the element
            Element = Elements.GetElement(Nucleus.ProtonCount);
            
            if(Element != null)
            {
                //set nucleus shake based on Isotope stability
                Isotope isotope = Element.GetIsotope(Nucleus.Mass);
                Nucleus.Shake = isotope != null ? !isotope.Stable : true;

                //set the min and max isotope mass
                Nucleus.MassMax = Element.MaxIsotope;
                Nucleus.MassMin = Element.MinIsotope;

                //Auto add Neutrons to make valid Isotope
                if(Nucleus.Mass < Element.MinIsotope)
                {
                    workbench.NewAutoNeutron();
                }

                //Auto remove Neutrons to make valid Isotope
                if(Nucleus.Mass > Element.MaxIsotope)
                {
                    Nucleus.TrimNeutrons();
                }
            }
            else
            {
                Nucleus.MassMax = 0;
                Nucleus.TrimNeutrons(); 
            }

            //add or remove shells to match proton count
            if (shells.Count < Elements.GetShells(Nucleus.ProtonCount))
            {
                AddShell();
            }
            else if (shells.Count > Elements.GetShells(Nucleus.ProtonCount))
            {
                RemoveShell();
            }
            
            //remove the outer shell when proton cout is less than noble gas index

            /*
            //check if the outermost shell is full
            if (OuterShell.Full && shells.Count < 3)
            {
                AddShell();
            }*/
        }

        private void FixedUpdate()
        {
            if(excessParticles.Count > 0)
            {
                //copy to array so we can mutate list in loop
                Particle[] excess = excessParticles.ToArray();
                foreach (Particle particle in excess)
                {
                    //Seperate from atom
                    Vector2 diffToAtom = particle.PhysicsObj.Position - transform.position;
                    Vector3 forceToSeperate = diffToAtom.normalized * seperateSpeed;

                    //Apply the force
                    particle.PhysicsObj.AddForce(forceToSeperate);

                    //particle is no longer in DUI
                    if (!DUI.DUI.Contains(particle.PhysicsObj.Position))
                    {
                        //Destroy it
                        excessParticles.Remove(particle);
                        Destroy(particle.gameObject);
                    }
                }
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

        public void AddExcessParticle(Particle particle)
        {
            excessParticles.Add(particle);
            particle.transform.SetParent(transform);
        }

        public void RemoveExcessParticle(Particle particle)
        {
            if (excessParticles.Contains(particle))
            {
                excessParticles.Remove(particle);
                particle.transform.SetParent(transform);
            }
        }

        /// <summary>
        /// try to Remove a specified electron from the atom
        /// </summary>
        /// <param name="particle">particle to remove</param>
        /// <returns>removal scucess</returns>
        public bool RemoveElectron(Particle particle)
        {
            /*
            //remove the Outer shell if empty, next shell is now the Outer shell
            if (OuterShell.Empty && OuterShell.NextShell != null)
                RemoveShell();*/

            //start the recursive call to remove from outer shell
            return OuterShell.RemoveParticle(particle);
        }

        /// <summary>
        /// Remove and Destroy the Outer shell
        /// </summary>
        private void RemoveShell()
        {
            //remove any particles in the outer shell
            foreach(Particle particle in OuterShell.Particles)
            {
                OuterShell.RemoveParticle(particle);
                AddExcessParticle(particle);
            }

            //destroy the shell object
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
            shell.MaxParticles = Elements.electronsPerShell[shells.Count];
            shell.NextShell = shells.Count == 0 ? null : OuterShell;

            //push shell onto stack
            shells.Push(shell);

            //fill the outer shell 
            if (OuterShell.NextShell != null)
            {
                for (int i = OuterShell.NextShell.ElectronCount; i < OuterShell.NextShell.MaxParticles; i++)
                {
                    workbench.NewAutoElectron();
                }
            }
        }

    }
}

