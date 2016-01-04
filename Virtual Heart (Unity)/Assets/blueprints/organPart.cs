using Assets.blueprints;
using Assets.Scripts;
using System;
using UnityEngine;


public class organPart : MonoBehaviour, ISelectable {

    // Handlers for selection and deselection
    public delegate void OrganHandler(organPart o);
    public static event OrganHandler OrganPartSelected;
    public static event OrganHandler OrganPartDeselected;

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
        MeshRenderer meshRenderer = meshObj.AddComponent<MeshRenderer>();
        meshObj.transform.parent = gameObject.transform;

        // Make sure we're starting with the default material and default opacity
        SetMaterial(defaultOrganPartColor);
        SetMaterialOpaque(opaque: false);

        // Create a label for this organ
        GameObject labelManagerGameObject = GameObject.Find("LabelManager");
        labelManager manager = labelManagerGameObject.GetComponent<labelManager>();
        manager.AddLabelFor(gameObject);

        // When an organ is selected and it's not this organ
        // make this organ opaque
        OrganPartSelected += o =>
        {
            if (o != this)
                SetMaterialOpaque(opaque: true);
        };

        // When an organ is deselected and it's not this organ
        // make this organ non-opaque
        OrganPartDeselected += o =>
        {
            if (o != this)
                SetMaterialOpaque(opaque: false);
        };
    }

    #region ISelectable

    public void Select()
    {
        Debug.Log("Selecting " + name);
        SetMaterial(selectedMaterial);
        OrganPartSelected(this);
    }

    public void Deselect()
    {
        Debug.Log("Selecting " + name);
        SetMaterial(defaultOrganPartColor);
        OrganPartDeselected(this);
    }

    #endregion ISelectable

    private void SetMaterial(Material material)
    {
        Debug.Log("Setting '" + material.name + "' on '" + name + "'");
        foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material = material;
        }
    }

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
