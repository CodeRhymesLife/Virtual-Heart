using Assets.blueprints;
using UnityEngine;


public class organ : MonoBehaviour {

    // Use this for initialization
    void Start () {
        OrganMetadataManager.OrganMetadata metadata = OrganMetadataManager.Instance.GetOrganMetadata(name);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
