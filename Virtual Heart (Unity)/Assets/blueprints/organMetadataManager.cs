using SimpleJSON;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.blueprints
{
    public class OrganMetadataManager
    {
        private const string HeartMetadataFilename = "organs/heart_metadata.json";

        private JSONNode _organMetadata;
        private Dictionary<string, JSONNode> _temporaryDictionary;

        private static OrganMetadataManager _instance;
        public static OrganMetadataManager Instance
        {
            get
            {
                return _instance ?? (_instance = new OrganMetadataManager());
            }
        }

        /// <summary>
        /// Reads in the metadata file and saves it
        /// </summary>
        private OrganMetadataManager()
        {
            try
            {
                Debug.Log("Loading file: " + HeartMetadataFilename);
                TextAsset heartMetadata = (TextAsset)Resources.Load(HeartMetadataFilename, typeof(TextAsset));
                _organMetadata = JSON.Parse(heartMetadata.text);

                // TEMPORARY
                // Create dicitonary of organs for backward compatability
                Debug.Log("Adding parts to temp dict");
                _temporaryDictionary = new Dictionary<string, JSONNode>();
                for (int organPartIndex = 0; organPartIndex < _organMetadata["parts"].Count; organPartIndex++)
                {
                    JSONNode part = _organMetadata["parts"][organPartIndex];
                    _temporaryDictionary[part["name"]] = part;
                }

                Debug.Log("Metadata loaded successfully");
            }
            catch (Exception e)
            {
                Debug.Log("Error when reading metadata file: " + e.Message);
                throw e;
            }
        }

        /// <summary>
        /// Gets organ metadata for the specified organ
        /// </summary>
        /// <param name="organName">Name of organ to get data for</param>
        public OrganMetadata GetOrganMetadata(string organName)
        {
            Debug.Log("Retrieving metadata for '" + organName + "'");
            return new OrganMetadata
            {
                FMAId = _temporaryDictionary[organName]["FMAId"].Value,
                Description = _temporaryDictionary[organName]["description"],
            };
        }

        public class OrganMetadata
        {
            public string FMAId;
            public string Description;
        }
    }
}
