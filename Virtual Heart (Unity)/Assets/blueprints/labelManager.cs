using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class labelManager : MonoBehaviour {

    public GameObject label;

    private const float DistanceBetweenLabels = 0.06f;
    private List<label> _labels;
    private List<label> Labels
    {
        get { return _labels ?? (_labels = new List<label>()); }
    }

    public void AddLabelFor(GameObject anchor)
    {
        // Place the new label above the previous label, or at the bottom if it's the first label
        Vector3 position = gameObject.transform.position + new Vector3(0, Labels.Count * DistanceBetweenLabels, 0);
        GameObject lbl = Instantiate(label, position, Quaternion.identity) as GameObject;
        label labelComponent = lbl.GetComponent<label>();
        labelComponent.Anchor = anchor;
        Labels.Add(labelComponent);
    }
}
