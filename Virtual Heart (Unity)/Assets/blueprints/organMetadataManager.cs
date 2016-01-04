using SimpleJSON;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.blueprints
{
    public class OrganMetadataManager
    {
        #region Stuff to keep
        private const string OrganMetadataFolder = "organs";

        /// <summary>
        /// Load metadata for each organ
        /// </summary>
        /// <returns></returns>
        public static OrganMetadata[] LoadMetadata()
        {
            Debug.Log("Loading all metadata files");
            List<OrganMetadata> allOrganMetadata = new List<OrganMetadata>();

            // Loop over each json file and load the metadata for that file
            TextAsset[] heartMetadata = Resources.LoadAll<TextAsset>(OrganMetadataFolder);
            foreach(TextAsset metadataFile in heartMetadata)
            {
                JSONNode root = JSON.Parse(metadataFile.text);
                OrganMetadata organMetadata = new OrganMetadata(root);
                allOrganMetadata.Add(organMetadata);
                Debug.Log("Loaded metadata for organ: " + organMetadata.Name);
            }

            Debug.Log("Metadata files loaded successfully");
            return allOrganMetadata.ToArray();
        }

        #endregion Stuff to keep

        private static OrganMetadata[] s_tempData;

        /// <summary>
        /// Gets organ metadata for the specified organ
        /// </summary>
        /// <param name="organName">Name of organ to get data for</param>
        public static OrganPartMetadata GetOrganMetadata(string organName)
        {
            if (s_tempData == null)
                s_tempData = LoadMetadata();

            Debug.Log("Retrieving metadata for '" + organName + "'");
            foreach(OrganPartMetadata organPart in s_tempData[0].Parts)
            {
                if (organPart.Name == organName)
                    return organPart;
            }

            throw new ArgumentException("organName", "Invalid organ name");
        }

        /// <summary>
        /// Contains information about an organ with multiple parts (e.g. the Heart)
        /// </summary>
        public class OrganMetadata : OrganPartMetadata
        {
            public List<OrganPartMetadata> Parts;

            public OrganMetadata(JSONNode info) : base(info)
            {
                Parts = new List<OrganPartMetadata>();

                // Create metadata for each organ part
                JSONNode parts = info["parts"];
                for (int organPartIndex = 0; organPartIndex < parts.Count; organPartIndex++)
                {
                    Parts.Add(new OrganPartMetadata(parts[organPartIndex]));
                }
            }
        }

        /// <summary>
        /// Contains information about a single part of an organ (e.g. Mitral Valve of the Heart)
        /// </summary>
        public class OrganPartMetadata
        {
            public string Name;
            public string FMAId;
            public string Description;

            public OrganPartMetadata(JSONNode info)
            {
                Name = info["name"].Value;
                FMAId = info["FMAId"].Value;
                Description = info["description"].Value;
            }
        }
    }
}
