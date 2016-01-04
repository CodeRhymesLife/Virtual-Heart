using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Assets.Scripts;

public class selectableLabel : MonoBehaviour, ISelectable {

    private Color SelectedColor = Color.yellow;

    void Start()
    {
        // Add a box collider around the text so we can select it
        gameObject.AddComponent<BoxCollider>();
    }

    #region ISelectable

    public event SelectionHandler Selected;
    public event SelectionHandler Deselected;

    public void Select()
    {
        SetTextColor(SelectedColor);
        if (Selected != null)
            Selected(this);
    }

    public void Deselect()
    {
        SetTextColor(Color.black);
        if (Deselected != null)
            Deselected(this);
    }

    /// <summary>
    /// Sets a color on the text
    /// </summary>
    /// <param name="color">color to set on text</param>
    private void SetTextColor(Color color)
    {
        Debug.Log(name + ": setting label color to " + color);
        gameObject.GetComponent<TextMesh>().color = color ;
    }

    #endregion ISelectable
}
