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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Name = " + hit.collider.name);
                Debug.Log("Tag = " + hit.collider.tag);
                Debug.Log("Hit Point = " + hit.point);
                Debug.Log("Normal = " + hit.normal);
                Debug.Log("Object position = " + hit.collider.gameObject.transform.position);

                label lbl = hit.collider.gameObject.GetComponent<label>();
                if(lbl != null)
                {
                    Debug.Log("Label clicked! Name - " + lbl.name);
                }
            }
        }

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
