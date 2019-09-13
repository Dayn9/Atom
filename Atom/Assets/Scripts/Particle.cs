using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Particle : MonoBehaviour
{
    /// <summary>
    /// Parent class for Protons, Neutrons, Electorns
    /// Contains particle data and helper methods
    /// </summary>

    protected float mass;
    protected byte charge = 0;

    protected bool selected = false;
    public UnityEvent OnSelect;
    public UnityEvent OnDeselect;

    protected virtual void Awake()
    {
        OnSelect.AddListener(Select);
        OnDeselect.AddListener(Deselect);
    }

    protected virtual void Update()
    {
        if (selected)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (Input.GetMouseButtonUp(0))
            {
                OnDeselect?.Invoke();
            }
        }
    }

    protected void Select()
    {
        selected = true;
    }

    protected void Deselect()
    {
        selected = false;
    }

}
