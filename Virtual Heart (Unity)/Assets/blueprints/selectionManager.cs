using UnityEngine;
using System.Collections;
using Assets.Scripts;

/// <summary>
/// Manages selection of objects
/// All objects that want to be selected need to implement ISelectable
/// </summary>
public class selectionManager : MonoBehaviour {

    public Color SelectedColor;
    public Material SelectedMaterial;

    private SelectedObjInfo _selectedObjInfo;

    private static selectionManager s_instance;
    public static selectionManager Instance
    {
        get
        {
            return s_instance ?? (s_instance = GameObject.Find("SelectionManager").GetComponent<selectionManager>());
        }
    }

	// Update is called once per frame
	void Update () {
        // If the mouse was clicked cast a ray to see if anything was hit
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Name = " + hit.collider.name);
                Debug.Log("Tag = " + hit.collider.tag);
                Debug.Log("Hit Point = " + hit.point);
                Debug.Log("Normal = " + hit.normal);
                Debug.Log("Object position = " + hit.collider.gameObject.transform.position);

                SelectedObjInfo objToSelect = new SelectedObjInfo(hit.collider.gameObject);

                // If the object is alread selected ignore the selection
                if (_selectedObjInfo != null && objToSelect.Selectable == _selectedObjInfo.Selectable)
                {
                    Debug.Log("'" + _selectedObjInfo.Name + "' is alread selected. Deselecting.");
                    _selectedObjInfo.Selectable.Deselect();
                    _selectedObjInfo = null;
                    return;
                }

                // If an object was hit and it's selectable, select it
                if (objToSelect.CanSelect)
                {
                    Debug.Log("Selecting '" + objToSelect.Name + "'");
                    
                    // Deselect the previously selected object
                    if(_selectedObjInfo != null)
                    {
                        Debug.Log("Deselecting '" + _selectedObjInfo.Name + "'");
                        _selectedObjInfo.Selectable.Deselect();
                    }

                    // Select the new obj
                    Debug.Log("Selecting '" + objToSelect.Name + "'");
                    _selectedObjInfo = objToSelect;
                    _selectedObjInfo.Selectable.Select();
                }
            }
        }
    }

    /// <summary>
    /// Container for selected objects
    /// </summary>
    private class SelectedObjInfo
    {
        public bool CanSelect { get { return Selectable != null; } }

        public ISelectable Selectable { get; private set; }

        public string Name { get; private set; }

        public SelectedObjInfo(GameObject obj)
        {
            Selectable = obj.GetComponent<ISelectable>();
            if (Selectable != null)
                Name = obj.name;
        }
    }
}
