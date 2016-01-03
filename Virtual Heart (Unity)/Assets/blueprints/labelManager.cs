using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class labelManager : MonoBehaviour {

    public GameObject label;

    private const float DistanceBetweenLabels = 0.06f;

    private List<label> _labels;
    private List<label> Labels
    {
        get { return _labels ?? (_labels = new List<label>()); }
        set { _labels = value; }
    }

    public void AddLabelFor(GameObject anchor)
    {
        // Create the label and set it's anchor
        GameObject lbl = Instantiate(label) as GameObject;
        Vector3 position = gameObject.transform.position + new Vector3(0, Labels.Count * DistanceBetweenLabels, 0);
        lbl.transform.position = position;

        label labelComponent = lbl.GetComponent<label>();
        labelComponent.Anchor = anchor;
        Labels.Add(labelComponent);
    }
}
