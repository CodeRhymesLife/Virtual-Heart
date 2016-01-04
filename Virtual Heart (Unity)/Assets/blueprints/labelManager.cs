using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;

public class labelManager : MonoBehaviour {

    public GameObject label;

    private const float DistanceBetweenLabels = 0.06f;

    private List<selectableLabel> _labels;
    private List<selectableLabel> Labels
    {
        get { return _labels ?? (_labels = new List<selectableLabel>()); }
        set { _labels = value; }
    }

    /// <summary>
    /// Add a label to this manager
    /// </summary>
    /// <param name="name">Name of new label</param>
    /// <param name="text">New label text</param>
    /// <returns>Created label</returns>
    public ISelectable AddLabel(string name, string text)
    {
        // Create the label, parent it, and set it's position
        GameObject labelObj = Instantiate(label) as GameObject;
        Vector3 position = gameObject.transform.position + new Vector3(0, Labels.Count * DistanceBetweenLabels, 0);
        labelObj.transform.parent = transform;
        labelObj.transform.position = position;

        selectableLabel labelComponent = labelObj.GetComponent<selectableLabel>();
        labelComponent.name = name;

        // Set text on the label
        TextMesh textMesh = labelObj.GetComponent<TextMesh>();
        textMesh.text = text;
        Labels.Add(labelComponent);

        return labelComponent;
    }

    /// <summary>
    /// Clear the list of labels
    /// </summary>
    public void Clear()
    {
        // Remove all game objects
        foreach(selectableLabel label in Labels)
        {
            GameObject.Destroy(label.gameObject);
        }

        Labels.Clear();
    }
}
