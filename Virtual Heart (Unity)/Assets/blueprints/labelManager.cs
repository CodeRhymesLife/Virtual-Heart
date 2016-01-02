using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class labelManager : MonoBehaviour {

    public GameObject label;

    private const float DistanceBetweenLabels = 0.06f;
    private List<label> _labels;

	// Use this for initialization
	void Start () {
        _labels = new List<label>();
	}

    public void AddLabelFor(GameObject anchor)
    {
        // Place the new label above the previous label, or at the bottom if it's the first label
        Vector3 position = gameObject.transform.position + new Vector3(0, _labels.Count * DistanceBetweenLabels, 0);
        GameObject lbl = Instantiate(label, position, Quaternion.identity) as GameObject;
        label labelComponent = lbl.GetComponent<label>();
        labelComponent.Anchor = anchor;
        _labels.Add(labelComponent);
    }
}
