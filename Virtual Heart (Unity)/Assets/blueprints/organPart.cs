using Assets.blueprints;
using Assets.Scripts;
using System;
using UnityEngine;


public class organPart : MonoBehaviour {

    // Global handlers for selection and deselection
    public delegate void OrganHandler(organPart o);
    public static event OrganHandler AnOrganPartHighlighted;
    public static event OrganHandler AnOrganPartUnHighlighted;

    private Material defaultOrganPartColor;
    private Material selectedMaterial;

    public OrganMetadataManager.OrganPartMetadata Metadata
    {
        get;
        set;
    }

    // Use this for initialization
    void Start () {
        if (Metadata == null)
            throw new ArgumentNullException("Metadata", "organPart Metadata needs to be set by the creator of this script");

        defaultOrganPartColor = (Material) Resources.Load("Materials/defaultOrganPartColor", typeof(Material));
        selectedMaterial = (Material)Resources.Load("Materials/highlightYellow", typeof(Material));

        // Create the mesh for this organ part
        GameObject meshObj = new GameObject(Metadata.Name + "_MESH");
        MeshFilter meshFilter = meshObj.AddComponent<MeshFilter>();
        meshFilter.mesh = Resources.Load<Mesh>(Metadata.MeshFile);
        meshObj.AddComponent<MeshRenderer>();
        meshObj.transform.parent = gameObject.transform;

        // Make sure we're starting with the default material and default opacity
        SetMaterial(defaultOrganPartColor);
        SetMaterialOpaque(opaque: false);

        // Create a label for this organ
        GameObject labelManagerGameObject = GameObject.Find("OrganLabelManager");
        labelManager manager = labelManagerGameObject.GetComponent<labelManager>();
        ISelectable label = manager.AddLabel("Lable for: " + gameObject.name, Metadata.Name);
        label.Selected += l => {
            Debug.Log("Highlighting " + name);
            SetMaterial(selectedMaterial);
            AnOrganPartHighlighted(this);
        };
        label.Deselected += l => {
            Debug.Log("Unhighlighting " + name);
            SetMaterial(defaultOrganPartColor);
            AnOrganPartUnHighlighted(this);
        };

        // When an organ is selected and it's not this organ
        // make this organ opaque
        AnOrganPartHighlighted += o =>
        {
            if (o != this)
                SetMaterialOpaque(opaque: true);
        };

        // When an organ is deselected and it's not this organ
        // make this organ non-opaque
        AnOrganPartUnHighlighted += o =>
        {
            if (o != this)
                SetMaterialOpaque(opaque: false);
        };
    }

    /// <summary>
    /// Set a material on this organ part
    /// </summary>
    /// <param name="material">material to set</param>
    private void SetMaterial(Material material)
    {
        Debug.Log("Setting '" + material.name + "' on '" + name + "'");
        foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material = material;
        }
    }

    /// <summary>
    /// Set the opacity on this organ part
    /// </summary>
    /// <param name="opaque">opaque or not</param>
    private void SetMaterialOpaque(bool opaque)
    {
        float opacity = opaque ? 0.1f : 1f;
        Shader shader = opaque ? Shader.Find("Transparent/Diffuse") : Shader.Find("Standard");

        Debug.Log("Setting opacoty to " + opacity + " with shader '" + shader.name + "' on '" + name + "'");
        foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            Color currentColor = meshRenderer.material.color;
            meshRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, opacity);
            meshRenderer.material.shader = shader;
        }
    }
}
