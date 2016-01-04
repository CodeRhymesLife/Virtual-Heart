using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.blueprints;

namespace Assets.Scripts
{
    public class organLoader : MonoBehaviour {

        // Container for loaded organ
        public GameObject loadedOrganContainer;

        public GameObject selectedOrganLabelManager;

        // Use this for initialization
        void Start() {
            labelManager lblManager = selectedOrganLabelManager.GetComponent<labelManager>();

            OrganMetadataManager.OrganMetadata[] organPartContainerMetadata = OrganMetadataManager.LoadMetadata();
            foreach (OrganMetadataManager.OrganMetadata metadata in organPartContainerMetadata)
            {
                // Give the user the option to load an organ
                ISelectable label = lblManager.AddLabel("Label to load: " + metadata.Name, "Load " + metadata.Name);
                OrganMetadataManager.OrganMetadata metadataForDelegate = metadata; // Create a new variable for the delegate
                label.Selected += l =>
                {
                    LoadOrgan(metadataForDelegate);
                };
            }
        }

        /// <summary>
        /// Dynamically loads an organ
        /// </summary>
        /// <param name="organMetadata">Metadata describing the organ</param>
        private void LoadOrgan(OrganMetadataManager.OrganMetadata organMetadata)
        {
            Debug.Log("Creating organ for: " + organMetadata.Name);

            Reset();

            // Create the new organ
            GameObject organObj = new GameObject(organMetadata.Name);
            organ script = organObj.AddComponent<organ>();
            script.Metadata = organMetadata;

            // Place the organ in the proper place
            organObj.transform.parent = loadedOrganContainer.transform;
            organObj.transform.localPosition = new Vector3();
        }

        /// <summary>
        /// Clear everything
        /// </summary>
        private void Reset()
        {
            // Clear any organ that was previously loaded
            foreach (Transform child in loadedOrganContainer.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            // Clear the organ label manager
            GameObject.Find("OrganLabelManager").GetComponent<labelManager>().Clear();
        }
    }
}
