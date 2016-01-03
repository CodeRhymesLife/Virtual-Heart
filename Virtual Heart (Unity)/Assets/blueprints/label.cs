using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class label : MonoBehaviour, ISelectable {

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

            // Draw the line between the label and the anchor
            {
                // Start the line at the label
                UpdateLinePositionOnLabel();

                // End the line at the closest point on the anchor
                LinePositionOnAnchor = GetClosestAnchorPoint(referencePoint: LinePositionOnLabel);
                Line.SetPosition(1, LinePositionOnAnchor);
            }
        }
    }

    /// <summary>
    /// The line drawn between the label and the line
    /// </summary>
    private LineRenderer _line;
    private LineRenderer Line
    {
        get
        {
            return _line ?? (_line = gameObject.GetComponent<LineRenderer>());
        }
    }

    /// <summary>
    /// Position of line on label
    /// </summary>
    public Vector3 LinePositionOnLabel
    {
        get;
        private set;
    }

    /// <summary>
    /// Position of line on anchor
    /// </summary>
    public Vector3 LinePositionOnAnchor
    {
        get;
        private set;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.hasChanged)
            UpdateLinePositionOnLabel();
    }

    /// <summary>
    /// Start the line at the bottom of the label
    /// </summary>
    private void UpdateLinePositionOnLabel()
    {
        // Start the line at the bottom of the label
        LinePositionOnLabel = transform.position - new Vector3(0, transform.localScale.y, 0);
        Line.SetPosition(0, LinePositionOnLabel);
    }

    /// <summary>
    /// Gets the closes point on the anchor from the given starting point
    /// </summary>
    /// <param name="referencePoint">Reference point that shortest distance is calculated with</param>
    /// <returns>Closest point to the starting point, or the anchor's tranform if the anchor doesn't have child meshes</returns>
    private Vector3 GetClosestAnchorPoint(Vector3 referencePoint)
    {
        // Default with the anchor's transform position
        Vector3 closestPoint = Anchor.transform.position;

        // Get all the child meshes and add their verties to a single collection
        List<Vector3> vertices = new List<Vector3>();
        foreach (MeshFilter meshFilter in Anchor.GetComponentsInChildren<MeshFilter>())
        {
            vertices.AddRange(meshFilter.mesh.vertices);
        }

        // Sort the child vertices and retrieve the closest one
        if (vertices.Count > 0)
        {
            closestPoint = vertices.OrderBy(v => (Anchor.transform.TransformPoint(v) - referencePoint).sqrMagnitude).Take(1).First();
            closestPoint = Anchor.transform.TransformPoint(closestPoint);
        }

        return closestPoint;
    }

    #region ISelectable

    public void Select()
    {
        Debug.Log(name + " selected");
    }

    public void Deselect()
    {
        Debug.Log(name + " deselected");
    }

    #endregion ISelectable
}
