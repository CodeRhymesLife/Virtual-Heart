using Assets.blueprints;
using Assets.Scripts;
using System;
using UnityEngine;


public class organPart : MonoBehaviour {

    // Global handlers for selection and deselection
    public delegate void OrganHandler(organPart o);
    public static event OrganHandler AnOrganPartHighlighted;
    public static event OrganHandler AnOrganPartUnHighlighted;

    private float Opacity = 0.15f;

    private Material defaultOrganPartColor;
    private Material selectedMaterial;

    private ISelectable _label;

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
        GameObject meshObj = Instantiate(Resources.Load<GameObject>(Metadata.MeshFile));
        meshObj.transform.parent = gameObject.transform;

        // Make sure we're starting with the default material and default opacity
        SetMaterial(defaultOrganPartColor);
        SetMaterialOpaque(opaque: false);

        // Create a label for this organ
        GameObject labelManagerGameObject = GameObject.Find("OrganLabelManager");
        labelManager manager = labelManagerGameObject.GetComponent<labelManager>();
        _label = manager.AddLabel("Lable for: " + gameObject.name, Metadata.Name);
        _label.Selected += OnLabelSelected;
        _label.Deselected += OnLabelDeselected;

        // Add global highlighted event handlers
        AnOrganPartHighlighted += OnAnOrganPartHighlighted;
        AnOrganPartUnHighlighted += OnAnOrganPartUnhighlighted;
    }

    /// <summary>
    /// Remove global handlers
    /// </summary>
    void OnDestroy()
    {
        if (_label != null)
        {
            _label.Selected -= OnLabelSelected;
            _label.Deselected -= OnLabelDeselected;
        }

        AnOrganPartHighlighted -= OnAnOrganPartHighlighted;
        AnOrganPartUnHighlighted -= OnAnOrganPartUnhighlighted;
    }

    /// <summary>
    /// When the label for this organ part is selected
    /// highlight this organ part
    /// </summary>
    /// <param name="label">label for t his organ part</param>
    private void OnLabelSelected(ISelectable label)
    {
        Debug.Log("Highlighting " + name);
        SetMaterial(selectedMaterial);
        AnOrganPartHighlighted(this);
    }

    /// <summary>
    /// When the label for this organ part is deselected
    /// make sure the default material is set
    /// </summary>
    /// <param name="label">label for t his organ part</param>
    private void OnLabelDeselected(ISelectable label)
    {
        Debug.Log("Unhighlighting " + name);
        SetMaterial(defaultOrganPartColor);
        AnOrganPartUnHighlighted(this);
    }

    /// <summary>
    /// When an organ is selected and it's not this organ make this organ opaque
    /// </summary>
    /// <param name="highlightedOrgan">highlighted organ</param>
    private void OnAnOrganPartHighlighted(organPart highlightedOrgan)
    {
        if (highlightedOrgan != this)
            SetMaterialOpaque(opaque: true);
    }

    /// <summary>
    /// When an organ is selected and it's not this organ make this organ non-opaque
    /// </summary>
    /// <param name="highlightedOrgan">highlighted organ</param>
    private void OnAnOrganPartUnhighlighted(organPart highlightedOrgan)
    {
        if (highlightedOrgan != this)
            SetMaterialOpaque(opaque: false);
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
        float opacity = opaque ? Opacity : 1f;
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
