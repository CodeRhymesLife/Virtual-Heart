using UnityEngine;
using System;
using Assets.blueprints;

namespace Assets.Scripts
{
    public class organ : MonoBehaviour
    {

        public OrganMetadataManager.OrganMetadata Metadata
        {
            get;
            set;
        }

        // Use this for initialization
        void Start()
        {
            if (Metadata == null)
                throw new ArgumentNullException("Metadata", "organ Metadata needs to be set by the creator of this script");

            // Create an object for each organ part and instantiate it
            foreach(OrganMetadataManager.OrganPartMetadata organPartMetadata in Metadata.Parts)
            {
                GameObject organPartObj = new GameObject(organPartMetadata.Name);
                organPart script = organPartObj.AddComponent<organPart>();
                script.Metadata = organPartMetadata;

                // Set the new part as a child of this organ
                organPartObj.transform.parent = gameObject.transform;
            }
        }
    }
}
