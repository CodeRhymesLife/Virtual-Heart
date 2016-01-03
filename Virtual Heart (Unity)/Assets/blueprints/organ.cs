using Assets.blueprints;
using Assets.Scripts;
using System;
using UnityEngine;


public class organ : MonoBehaviour, ISelectable {

    public Material defaultMaterial;
    public Material selectedMaterial;

    // Handlers for selection and deselection
    private delegate void OrganHandler(organ o);
    private static event OrganHandler OrganSelected;
    private static event OrganHandler OrganDeselected;

    // Use this for initialization
    void Start () {
        OrganMetadataManager.OrganMetadata metadata = OrganMetadataManager.Instance.GetOrganMetadata(name);

        // Create a label for this organ
        GameObject labelManagerGameObject = GameObject.Find("LabelManager");
        labelManager manager = labelManagerGameObject.GetComponent<labelManager>();
        manager.AddLabelFor(gameObject);

        // Make sure we're starting with the default material and default opacity
        SetMaterial(defaultMaterial);
        SetMaterialOpaque(opaque: false);

        // When an organ is selected and it's not this organ
        // make this organ opaque
        OrganSelected += o =>
        {
            if (o != this)
                SetMaterialOpaque(opaque: true);
        };

        // When an organ is deselected and it's not this organ
        // make this organ non-opaque
        OrganDeselected += o =>
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
        OrganSelected(this);
    }

    public void Deselect()
    {
        Debug.Log("Selecting " + name);
        SetMaterial(defaultMaterial);
        OrganDeselected(this);
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
