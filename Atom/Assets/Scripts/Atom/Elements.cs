using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atom
{
    public static class Elements
    {
        /// <summary>
        /// Contains ALL the data about elements
        /// </summary>

        private static Element[] elements = new Element[]
        {
            #region Period 1 elements
            new Element("Hydrogen", "H", 
                new Isotope[]{
                    new Isotope(1, true, 99.985f, "Protium"),
                    new Isotope(2, true, 0.015f, "Deuterium"),
                    new Isotope(3, false, 0.0f, "Tritium"),
                    new Isotope(4),
                    new Isotope(5),
                    new Isotope(6),
                    new Isotope(7)
                }),
            new Element("Helium", "He", 
                new Isotope[]{
                    new Isotope(2 , false, 0.0f, "DiProton"),
                    new Isotope(3, true, 0.000137f),
                    new Isotope(4, true, 99.999863f), 
                    new Isotope(5),
                    new Isotope(6),
                    new Isotope(7),
                    new Isotope(8),
                    new Isotope(9),
                    new Isotope(10),
                }),
            #endregion

            #region Period 2 elements
            new Element("Lithium", "Li", 
                new Isotope[]{
                    new Isotope(3),
                    new Isotope(4),
                    new Isotope(5),
                    new Isotope(6, true, 7.59f),
                    new Isotope(7, true, 92.41f),
                    new Isotope(8),
                    new Isotope(9),
                    new Isotope(10),
                    new Isotope(11),
                    new Isotope(12)
                }),
            new Element("Beryllium", "Be",
                new Isotope[]{
                    new Isotope(5),
                    new Isotope(6),
                    new Isotope(7),
                    new Isotope(8),
                    new Isotope(9, true, 100.0f),
                    new Isotope(10),
                    new Isotope(11),
                    new Isotope(12),
                    new Isotope(13),
                    new Isotope(14),
                    new Isotope(15),
                    new Isotope(16)
                }),
            new Element("Boron", "B",
                new Isotope[]{
                    new Isotope(6),
                    new Isotope(7),
                    new Isotope(8),
                    new Isotope(9),
                    new Isotope(10, true, 19.8f),
                    new Isotope(11, true, 80.2f),
                    new Isotope(12),
                    new Isotope(13),
                    new Isotope(14),
                    new Isotope(15),
                    new Isotope(16),
                    new Isotope(17),
                    new Isotope(18),
                }),
            new Element("Carbon", "C",
                new Isotope[]{
                    new Isotope(8),
                    new Isotope(9),
                    new Isotope(10),
                    new Isotope(11),
                    new Isotope(12, true, 98.89f),
                    new Isotope(13, true, 1.11f),
                    new Isotope(14),
                    new Isotope(15),
                    new Isotope(16),
                    new Isotope(17),
                    new Isotope(18),
                    new Isotope(19),
                    new Isotope(20),
                    new Isotope(21),
                    new Isotope(22),
                }),
            new Element("Nitrogen", "N",
                new Isotope[]{
                    new Isotope(10),
                    new Isotope(11),
                    new Isotope(12),
                    new Isotope(13),
                    new Isotope(14, true, 99.634f),
                    new Isotope(15, true, 0.366f),
                    new Isotope(16),
                    new Isotope(17),
                    new Isotope(18),
                    new Isotope(19),
                    new Isotope(20),
                    new Isotope(21),
                    new Isotope(22),
                    new Isotope(23), 
                    new Isotope(24),
                    new Isotope(25)
                }),
            new Element("Oxygen", "O",
                new Isotope[]{
                    new Isotope(12),
                    new Isotope(13),
                    new Isotope(14),
                    new Isotope(15),
                    new Isotope(16, true, 99.762f),
                    new Isotope(17, true, 0.038f),
                    new Isotope(18, true, 0.2f),
                    new Isotope(19),
                    new Isotope(20),
                    new Isotope(21),
                    new Isotope(22),
                    new Isotope(23),
                    new Isotope(24),
                    new Isotope(25),
                    new Isotope(26),
                    new Isotope(27),
                    new Isotope(28)
                }),
            new Element("Fluorine", "F",
                new Isotope[]{
                    new Isotope(14),
                    new Isotope(15),
                    new Isotope(16),
                    new Isotope(17),
                    new Isotope(18),
                    new Isotope(19, true, 100.0f),
                    new Isotope(20),
                    new Isotope(21),
                    new Isotope(22),
                    new Isotope(23),
                    new Isotope(24),
                    new Isotope(25),
                    new Isotope(26),
                    new Isotope(27),
                    new Isotope(28),
                    new Isotope(29),
                    new Isotope(30),
                    new Isotope(31)
                }),
            new Element("Neon", "Ne",
                new Isotope[]{
                    new Isotope(16),
                    new Isotope(17),
                    new Isotope(18),
                    new Isotope(19),
                    new Isotope(20, true, 90.48f),
                    new Isotope(21, true, 0.27f),
                    new Isotope(22, true, 9.25f),
                    new Isotope(23),
                    new Isotope(24),
                    new Isotope(25),
                    new Isotope(26),
                    new Isotope(27),
                    new Isotope(28),
                    new Isotope(29),
                    new Isotope(30),
                    new Isotope(31),
                    new Isotope(32),
                    new Isotope(33),
                    new Isotope(34)
                }),
            #endregion

            #region Period 3 elements
            new Element("Sodium", "Na",
                new Isotope[]{
                    new Isotope(18),
                    new Isotope(19),
                    new Isotope(20),
                    new Isotope(21),
                    new Isotope(22),
                    new Isotope(23, true, 100.0f),
                    new Isotope(24),
                    new Isotope(25),
                    new Isotope(26),
                    new Isotope(27),
                    new Isotope(28),
                    new Isotope(29),
                    new Isotope(30),
                    new Isotope(31),
                    new Isotope(32),
                    new Isotope(33),
                    new Isotope(34),
                    new Isotope(35),
                    new Isotope(36),
                    new Isotope(37)
                }),
            new Element("Magnesium", "Mg",
                new Isotope[]{
                    new Isotope(19),
                    new Isotope(20),
                    new Isotope(21),
                    new Isotope(22),
                    new Isotope(23),
                    new Isotope(24, true, 78.99f),
                    new Isotope(25, true, 10.0f),
                    new Isotope(26, true, 11.01f),
                    new Isotope(27),
                    new Isotope(28),
                    new Isotope(29),
                    new Isotope(30),
                    new Isotope(31),
                    new Isotope(32),
                    new Isotope(33),
                    new Isotope(34),
                    new Isotope(35),
                    new Isotope(36),
                    new Isotope(37),
                    new Isotope(38),
                    new Isotope(39),
                    new Isotope(40)
                }),
            new Element("Aluminium", "Al",
                new Isotope[]{
                    new Isotope(20),
                    new Isotope(21),
                    new Isotope(22),
                    new Isotope(23),
                    new Isotope(24),
                    new Isotope(25),
                    new Isotope(26),
                    new Isotope(27, true, 100.0f),
                    new Isotope(28),
                    new Isotope(29),
                    new Isotope(30),
                    new Isotope(31),
                    new Isotope(32),
                    new Isotope(33),
                    new Isotope(34),
                    new Isotope(35),
                    new Isotope(36),
                    new Isotope(37),
                    new Isotope(38),
                    new Isotope(39),
                    new Isotope(40),
                    new Isotope(41)
                }),
            new Element("Silicon", "Si",
                new Isotope[]{
                    new Isotope(22),
                    new Isotope(23),
                    new Isotope(24),
                    new Isotope(25),
                    new Isotope(26),
                    new Isotope(27),
                    new Isotope(28, true, 92.23f),
                    new Isotope(29, true, 4.683f),
                    new Isotope(30, true, 3.087f),
                    new Isotope(31),
                    new Isotope(32),
                    new Isotope(33),
                    new Isotope(34),
                    new Isotope(35),
                    new Isotope(36),
                    new Isotope(37),
                    new Isotope(38),
                    new Isotope(39),
                    new Isotope(40),
                    new Isotope(41),
                    new Isotope(42),
                    new Isotope(43),
                    new Isotope(44)
                }),
            new Element("Phosphorus", "P",
                new Isotope[]{
                    new Isotope(24),
                    new Isotope(25),
                    new Isotope(26),
                    new Isotope(27),
                    new Isotope(28),
                    new Isotope(29),
                    new Isotope(30),
                    new Isotope(31, true, 100.0f),
                    new Isotope(32),
                    new Isotope(33),
                    new Isotope(34),
                    new Isotope(35),
                    new Isotope(36),
                    new Isotope(37),
                    new Isotope(38),
                    new Isotope(39),
                    new Isotope(40),
                    new Isotope(41),
                    new Isotope(42),
                    new Isotope(43),
                    new Isotope(44),
                    new Isotope(45),
                    new Isotope(46),
                }),
            new Element("Sulfur", "S",
                new Isotope[]{
                    new Isotope(26),
                    new Isotope(27),
                    new Isotope(28),
                    new Isotope(29),
                    new Isotope(30),
                    new Isotope(31),
                    new Isotope(32, true, 95.02f),
                    new Isotope(33, true, 0.75f),
                    new Isotope(34, true, 4.21f),
                    new Isotope(35),
                    new Isotope(36, true, 0.02f),
                    new Isotope(37),
                    new Isotope(38),
                    new Isotope(39),
                    new Isotope(40),
                    new Isotope(41),
                    new Isotope(42),
                    new Isotope(43),
                    new Isotope(44),
                    new Isotope(45),
                    new Isotope(46),
                    new Isotope(47),
                    new Isotope(48),
                    new Isotope(49),
                }),
            new Element("Chlorine", "Cl",
                new Isotope[]{
                    new Isotope(28),
                    new Isotope(29),
                    new Isotope(30),
                    new Isotope(31),
                    new Isotope(32),
                    new Isotope(33),
                    new Isotope(34),
                    new Isotope(35, true, 75.77f),
                    new Isotope(36),
                    new Isotope(37, true, 24.23f),
                    new Isotope(38),
                    new Isotope(39),
                    new Isotope(40),
                    new Isotope(41),
                    new Isotope(42),
                    new Isotope(43),
                    new Isotope(44),
                    new Isotope(45),
                    new Isotope(46),
                    new Isotope(47),
                    new Isotope(48),
                    new Isotope(49),
                    new Isotope(50),
                    new Isotope(51)
                }),
            new Element("Argon", "Ar",
                new Isotope[]{
                    new Isotope(30),
                    new Isotope(31),
                    new Isotope(32),
                    new Isotope(33),
                    new Isotope(34),
                    new Isotope(35),
                    new Isotope(36, true, 0.3365f),
                    new Isotope(37),
                    new Isotope(38, true, 0.0632f),
                    new Isotope(39),
                    new Isotope(40, true, 99.6003f),
                    new Isotope(41),
                    new Isotope(42),
                    new Isotope(43),
                    new Isotope(44),
                    new Isotope(45),
                    new Isotope(46),
                    new Isotope(47),
                    new Isotope(48),
                    new Isotope(49),
                    new Isotope(50),
                    new Isotope(51),
                    new Isotope(52),
                    new Isotope(53)
                })
            #endregion
        };

        public static Element GetElement(int protonCount)
        {
            if (protonCount > 0 && protonCount <= elements.Length)
            {
                return elements[protonCount - 1];
            }
            return null;
        }

        public static int[] sblock = new int[] { 2, 2, 2, 2, 2, 2, 2 };
        public static int[] pblock = new int[] { 0, 6, 6, 6, 6, 6, 0 };
        public static int[] dblock = new int[] { 0, 0, 10, 10, 10, 10, 0 };
        public static int[] fblock = new int[] { 0, 0, 0, 14, 14, 0, 0 };

        public static int GetMaxElectrons(int shell, int protonCount)
        {
            int e = sblock[shell];
            e += protonCount > 2 ? pblock[shell] : 0;
            e += protonCount > 18 ? dblock[shell] : 0;
            e += protonCount > 54 ? fblock[shell] : 0;
            return e;
        }

        public static int GetShells(int protonCount)
        {
            if (protonCount == 0)
                return 0;
            if (protonCount <= 2)
                return 1;
            if (protonCount <= 2 + 8)
                return 2;
            if (protonCount <= 2 + 8 + 8)
                return 3;
            return 4;
        }
    }

    public class Element
    {
        public string Name { get; }
        public string Abbreviation { get; }
        public Isotope[] Isotopes { get; }

        public Element(string name, string abbreviation, Isotope[] isotopes = null)
        {
            Name = name;
            Abbreviation = abbreviation;
            Isotopes = isotopes; 
        }

        public int MaxIsotope { get { return Isotopes[Isotopes.Length - 1].Mass; } }
        public int MinIsotope { get { return Isotopes[0].Mass; } }     

        public Isotope GetIsotope(int mass)
        {
            //make sure there is actually an Isotope
            if (Isotopes != null && mass >= MinIsotope && mass <= MaxIsotope)
            {
                //index = mass - smallest possible mass
                return Isotopes[mass- Isotopes[0].Mass];
            }
            return null;
        }

        /// <summary>
        /// gets the most common isotope of the element
        /// </summary>
        /// <returns></returns>
        public Isotope GetCommon()
        {
            Isotope common = null;
            foreach(Isotope isotope in Isotopes)
            {
                if(isotope.Stable && (common == null || isotope.Abundance > common.Abundance))
                {
                    common = isotope;
                }
            }
            return common;
        }
    }

    public class Isotope
    {
        public int Mass { get; }
        public bool Stable { get; }
        public float Abundance { get; }
        public string FormalName { get; }

        public Isotope(int mass, bool stable = false, float abundance = 0.0f, string formalName = "")
        {
            Mass = mass;
            Stable = stable;
            Abundance = abundance;
            FormalName = formalName;
        }
    }
}

