using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.blueprints;

namespace Assets.Scripts
{
    public class organLoader : MonoBehaviour {

        // Use this for initialization
        void Start() {
            OrganMetadataManager.OrganMetadata[] organPartContainerMetadata = OrganMetadataManager.LoadMetadata();
            foreach (OrganMetadataManager.OrganMetadata metadata in organPartContainerMetadata)
            {
                // Give the user the option to load an organ

                // Temporarily load each one
                LoadOrgan(metadata);
            }
        }

        private void LoadOrgan(OrganMetadataManager.OrganMetadata organMetadata)
        {
            Debug.Log("Creating organ for: " + organMetadata.Name);
            GameObject organObj = new GameObject(organMetadata.Name);
            organ script = organObj.AddComponent<organ>();
            script.Metadata = organMetadata;
            Instantiate(organObj);

            // Size properly
        }
    }
}
