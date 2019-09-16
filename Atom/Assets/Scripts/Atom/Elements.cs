using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public static class Elements
    {
        private static Element[] elements = new Element[]
        {
            //Period 1 elements
            new Element("Hydrogen", "H"),
            new Element("Helium", "He"),

            //Period 2 elements
            new Element("Lithium", "Li"),
            new Element("Beryllium", "Be"),
            new Element("Boron", "B"),
            new Element("Carbon", "C"),
            new Element("Nitrogen", "N"),
            new Element("Oxygen", "O"),
            new Element("Fluorine", "F"),
            new Element("Neon", "He"),

            //Period 3 elements
            new Element("Sodium", "Na"),
            new Element("Magnesium", "Mg"),
            new Element("Aluminium", "Al"),
            new Element("Silicon", "Si"),
            new Element("Phosphorus", "P"),
            new Element("Sulfur", "S"),
            new Element("Chlorine", "Cl"),
            new Element("Argon", "Ar")
        };

        public static string GetName(int protonCount)
        {
            if (protonCount > 0 && protonCount <= elements.Length)
            {
                return elements[protonCount - 1].name;
            }
            return "---";
        }

        public static string GetAbbreviation(int protonCount)
        {
            if (protonCount > 0 && protonCount <= elements.Length)
            {
                return elements[protonCount - 1].abbreviation;
            }
            return "-";
        }
    }

    public struct Element
    {
        public string name;
        public string abbreviation;

        public Element(string name, string abbreviation)
        {
            this.name = name;
            this.abbreviation = abbreviation;
        }
    }
}

