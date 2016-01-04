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

    public ISelectable AddLabel(string name, string text)
    {
        // Create the label and set it's anchor
        GameObject labelObj = Instantiate(label) as GameObject;
        Vector3 position = gameObject.transform.position + new Vector3(0, Labels.Count * DistanceBetweenLabels, 0);
        labelObj.transform.position = position;

        selectableLabel labelComponent = labelObj.GetComponent<selectableLabel>();
        labelComponent.name = name;

        // Set text on the label
        TextMesh textMesh = labelObj.GetComponent<TextMesh>();
        textMesh.text = text;
        Labels.Add(labelComponent);

        return labelComponent;
    }
}
