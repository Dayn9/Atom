using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Atom
{
    public class PeriodicTable : MonoBehaviour
    {
        [SerializeField] private Atom atom;
        private Text[] texts;
        public Element Element { get; private set; }

        private void Awake()
        {
            texts = GetComponentsInChildren<Text>();

            for(int i = 0; i<18; i++)
            {
                int protonCount = i + 1; //create a new int for button to reference

                Element element = Elements.GetElement(protonCount);
                if (element != null)
                {
                    texts[i].text = element.Abbreviation;

                    //Hook up button to show the element data
                    Button b = texts[i].GetComponentInParent<Button>();
                    if(b != null)
                    {
                        b.onClick.AddListener(() => SetElement(protonCount));
                    }
                }
            }
        }

        private void SetElement(int protonCount)
        {

            Debug.Log("Show element: " + Elements.GetElement(protonCount).Name);

            if(atom != null)
            {
                atom.ForceToCommon(protonCount);
            }
        }
    }
}
