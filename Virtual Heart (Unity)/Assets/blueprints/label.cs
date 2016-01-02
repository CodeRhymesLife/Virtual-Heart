﻿using UnityEngine;
using System.Collections;

public class label : MonoBehaviour {

    public GameObject Anchor;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<TextMesh>().text = Anchor.name;
	}
	
	// Update is called once per frame
	void Update () {
        // Draw the line between the label and the anchor
        LineRenderer line = gameObject.GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position - new Vector3(0, transform.localScale.y, 0)); // Place the line at the bottom of the label
        line.SetPosition(1, Anchor.transform.position);
    }
}
