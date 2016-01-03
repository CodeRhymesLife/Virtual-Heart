using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Assets.Scripts;

public class label : MonoBehaviour, ISelectable {

    public Color SelectedColor;

    /// <summary>
    /// The object this label is anchored to
    /// </summary>
    private GameObject _anchor;
    public GameObject Anchor
    {
        get { return _anchor; }
        set
        {
            if (value == null)
            {
                Debug.Log("Anchor cannot be null");
                throw new ArgumentNullException("anchor");
            }

            _anchor = value;
            name = "Lable for: " + Anchor.name;

            // Set text on the label
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.text = Anchor.name;

            // Add a box collider around the text so we can select it
            gameObject.AddComponent<BoxCollider>();
        }
    }

    #region ISelectable

    public void Select()
    {
        SetTextColor(SelectedColor);
        ISelectable selectable = Anchor.GetComponent<ISelectable>();
        if (selectable != null)
            selectable.Select();
    }

    public void Deselect()
    {
        SetTextColor(Color.black);
        ISelectable selectable = Anchor.GetComponent<ISelectable>();
        if (selectable != null)
            selectable.Deselect();
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
