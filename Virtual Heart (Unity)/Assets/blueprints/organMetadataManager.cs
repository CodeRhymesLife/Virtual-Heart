using SimpleJSON;
using System;
using System.IO;
using UnityEngine;

namespace Assets.blueprints
{
    public class OrganMetadataManager
    {
        private const string MetadataFilename = @"D:\Apps\Virtual-Heart\Virtual Heart (Unity)\Assets\blueprints\heart\organ_metadata.json";

        private JSONNode _organMetadata;

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
                using (StreamReader reader = new StreamReader(MetadataFilename))
                {
                    var json = reader.ReadToEnd();
                    _organMetadata = JSON.Parse(json);
                    Debug.Log("Metadata loaded successfully");
                }
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
                FMAId = _organMetadata[organName]["FMAId"].Value,
                Description = _organMetadata[organName]["description"],
            };
        }

        public class OrganMetadata
        {
            public string FMAId;
            public string Description;
        }
    }
}
