using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

public class ElementsJSON : MonoBehaviour
{
    [SerializeField] private TextAsset textAsset;

    private void Awake()
    {
        /*
        string json = textAsset.text;
        Debug.Log(json);

        RootObject root = JsonConvert.DeserializeObject<RootObject>(json);
        */

        StreamWriter writer = new StreamWriter(Application.persistentDataPath + "\\Elements.json");

        writer.WriteLine("[");
        foreach (Atom.Element ele in Atom.Elements.elements)
        {
            string json = JsonConvert.SerializeObject(ele);
            writer.WriteLine(json + ",");
        }
        writer.WriteLine("]");

        writer.Close();
    }

    [Serializable]
    public struct RootObject
    {
        public Atom.Element[] elements { get; set; }
    }
}
