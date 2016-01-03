using Assets.blueprints;
using Assets.Scripts;
using UnityEngine;


public class organ : MonoBehaviour, ISelectable {

    private Material originalMaterial;

    // Use this for initialization
    void Start () {
        OrganMetadataManager.OrganMetadata metadata = OrganMetadataManager.Instance.GetOrganMetadata(name);

        // Create a label for this organ
        GameObject labelManagerGameObject = GameObject.Find("LabelManager");
        labelManager manager = labelManagerGameObject.GetComponent<labelManager>();
        manager.AddLabelFor(gameObject);

        // Save the original material used on this mesh
        foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            originalMaterial = meshRenderer.material;
            break;
        }
    }

    #region ISelectable

    public void Select()
    {
        Debug.Log("Selecting " + name);
        SetMaterial(selectionManager.Instance.SelectedMaterial);
    }

    public void Deselect()
    {
        Debug.Log("Selecting " + name);
        SetMaterial(originalMaterial);
    }

    private void SetMaterial(Material material)
    {
        Debug.Log("Setting '" + material.name + "' on '" + name +"'");
        foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material = material;
        }
    }

    #endregion ISelectable
}
