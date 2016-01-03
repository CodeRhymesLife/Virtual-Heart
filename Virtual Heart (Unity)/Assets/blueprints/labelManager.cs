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
        label labelComponent = lbl.GetComponent<label>();
        labelComponent.Anchor = anchor;
        Labels.Add(labelComponent);

        RepositionLables();
    }

    /// <summary>
    /// Reposition labels to decrease the odds their lines cross
    /// </summary>
    private void RepositionLables()
    {
        // To limit the number of lines that cross
        // sort the labels based on what they are connecting to,
        // then set their position based on their order
        Labels = Labels.OrderBy(l => l.LinePositionOnAnchor.y).ToList();
        for (int labelIndex = 0; labelIndex < Labels.Count; labelIndex++)
        {
            // Place the new label above the previous label, or at the bottom if it's the first label
            Vector3 position = gameObject.transform.position + new Vector3(0, labelIndex * DistanceBetweenLabels, 0);
            Labels[labelIndex].transform.position = position;
        }
    }
}
