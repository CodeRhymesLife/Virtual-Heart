using UnityEngine;
using System;
using Assets.blueprints;

namespace Assets.Scripts
{
    public class organ : MonoBehaviour
    {
        private const float InitialSize = 0.25f; // meters

        #region Zooming consts

        private const int MinZoomLevel = 0;
        private const int MaxZoomLevel = 10;
        private const float ZoomPercentage = 0.1f;
        private const float ZoomInAmount = 1 + ZoomPercentage;
        private const float ZoomOutAmount = 1 - ZoomPercentage;

        #endregion Zooming consts

        private bool _sizeInitialized = false;
        private int _zoomLevel = 0;

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

        void Update()
        {
            if (!_sizeInitialized)
                InitializeSize();
            else
                CheckZoom();
        }

        /// <summary>
        /// Initialize this organ's size
        /// </summary>
        private void InitializeSize()
        {
            // Get the size of this organ base on the size of it's children
            Bounds combinedBounds = new Bounds();
            foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
            {
                combinedBounds.Encapsulate(childRenderer.bounds);
            }

            // If the renders haven't started yet then we can't calculate the size
            if (combinedBounds.extents == new Vector3())
                return;

            // Resize
            float largestDimension = Math.Max(Math.Max(combinedBounds.extents.x, combinedBounds.extents.y), combinedBounds.extents.z);
            gameObject.transform.localScale *= InitialSize / largestDimension;
            _sizeInitialized = true;
        }

        /// <summary>
        /// Handles zooming in/out of the organ
        /// </summary>
        private void CheckZoom()
        {
            // Zoom out
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (_zoomLevel > MinZoomLevel)
                {
                    gameObject.transform.localScale *= ZoomOutAmount; // Shrink
                    _zoomLevel--;
                }
            }
            // Zoom in
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (_zoomLevel < MaxZoomLevel)
                {
                    gameObject.transform.localScale *= ZoomInAmount; // Grow
                    _zoomLevel++;
                }
            }
        }
    }
}
