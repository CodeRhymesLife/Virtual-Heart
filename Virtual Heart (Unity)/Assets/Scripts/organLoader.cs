using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.blueprints;

public class organLoader : MonoBehaviour {

    // Use this for initialization
    void Start() {
        OrganMetadataManager.OrganMetadata[] organPartContainerMetadata = OrganMetadataManager.LoadMetadata();
        foreach(OrganMetadataManager.OrganMetadata metadata in organPartContainerMetadata)
        {
            // Give the user the option to load an organ
        }
    }
}
