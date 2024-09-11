using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class FurnitureSelectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float necessaryHoldingTime;
    public float timer;
    public GameObject selectedFurniture;
    private FurnitureSpecs selectedFurnitureSpecs;
    [SerializeField]
    private LayerMask selectFurnitureLayer;
    private bool isEditing;
    [SerializeField]
    private LayerMask moveFurnitureLayer;

    private EditablesManager editablesManager;
    public static FurnitureSelectionManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        editablesManager = EditablesManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(isEditing) { return; }
        if (selectedFurniture==null)
        {
            selectFurniture();
        }
        else
        {
            moveFurniture();
        }
        
    }
    public void selectFurniture()
    {
        if (Input.touchCount == 1 && Input.touches[0].phase==TouchPhase.Stationary&&timer<=necessaryHoldingTime)
        {
           
                RaycastHit hit;

                Ray rayOrigin = Camera.main.ScreenPointToRay(Input.touches[0].position);
                if (Physics.Raycast(rayOrigin, out hit, Mathf.Infinity, selectFurnitureLayer))
                {
                    if (hit.transform)
                    {
                        timer += Time.deltaTime;
                    
                        if (timer > necessaryHoldingTime)
                        {
                            Debug.Log(hit.collider.gameObject);
                            selectedFurniture = hit.collider.gameObject.transform.parent.gameObject;
                            editablesManager.retrievePrefabFromPlane(selectedFurniture);
                            selectedFurniture.GetComponent<FurnitureModel>().editable.isOnEdit = true;
                            selectedFurnitureSpecs= selectedFurniture.GetComponent<FurnitureModel>().Specs;
                        }
                            
                    }
                }
        }
        else if (Input.touchCount==0)
        {
            timer = 0;
        }
    }
    public void emptyselectedFurniture()
    {
        if (selectedFurniture == null) return;
        selectedFurniture.GetComponent<FurnitureModel>().editable.isOnEdit = false;
        selectedFurniture = null;
        selectedFurnitureSpecs = null;
    }
    public void setEditingMode(bool editing)
    {
        isEditing = editing;
    }
    public void moveFurniture()
    {
        if (Input.touchCount == 1 )
        {
 
                RaycastHit hit;

                Ray rayOrigin = Camera.main.ScreenPointToRay(Input.touches[0].position);
                if (Physics.Raycast(rayOrigin, out hit, Mathf.Infinity, moveFurnitureLayer))
                {
                    if (hit.transform)
                    {
                        selectedFurniture.transform.position = hit.point;
                        selectedFurnitureSpecs.positions = new float[] { hit.point.x, hit.point.y, hit.point.z };
                    }
                }
            
        }
        //Input.deviceOrientation=DeviceOrientation.
    }
}
