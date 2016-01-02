using Assets.blueprints;
using UnityEngine;


public class organ : MonoBehaviour {

    // Use this for initialization
    void Start () {
        OrganMetadataManager.OrganMetadata metadata = OrganMetadataManager.Instance.GetOrganMetadata(name);

        // Create a label for this organ
        GameObject labelManagerGameObject = GameObject.Find("LabelManager");
        labelManager manager = labelManagerGameObject.GetComponent<labelManager>();
        manager.AddLabelFor(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
