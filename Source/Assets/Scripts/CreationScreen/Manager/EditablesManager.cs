using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class EditablesManager : MonoBehaviour
{
    public GameObject currentSelectedOnPlane;
    [SerializeField]
    private Camera secondaryCamera;
    [SerializeField]
    private Transform ItemHolder;
    [SerializeField]
    private Transform furnitureHolder;
    [SerializeField]
    private GameObject rawImageButton;
    [SerializeField]
    private GameObject plane;
    private FurnitureSelectionManager selectionManager;
    [SerializeField] Image primaryColor;
    [SerializeField] Image secondaryColor;
    [SerializeField] EditorPanelManager editorPanelManager;
    private UiManager uiManager;
    
    public static EditablesManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        uiManager = UiManager.Instance;
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        selectionManager = FurnitureSelectionManager.Instance;
    }


    public void showRawImage()
    {
        if (rawImageButton.activeInHierarchy)
        {
            currentSelectedOnPlane = null;
            selectionManager.emptyselectedFurniture();
        }
        if (rawImageButton != null)
        {
            rawImageButton.SetActive(true);
        }
    }
    public  void setPrefabOnPlane()
    {
        
        if (ItemHolder.childCount==0)
        {
            return;
        }
        if (currentSelectedOnPlane != null)
        {
            deleteCurrentSelectedFromHolder();
            selectionManager.emptyselectedFurniture();
        }
        currentSelectedOnPlane = null;
        GameObject obj = ItemHolder.GetChild(0).gameObject;
        changeLayer(obj, "Furniture");
        obj.transform.SetParent(furnitureHolder, false);

        FurnitureModel model = obj.GetComponent<FurnitureModel>();

        //Set the scale on the plane
        model.onScaleChanged();

        //When i set it to the plane i need to change the rotation since i dont have the same on the second camera
        float[] rotationsObj = model.Specs.rotations;
        rotationsObj[1] = rotationsObj[1] - 180f + 45f - plane.transform.eulerAngles.y;
        obj.transform.localEulerAngles = new Vector3(rotationsObj[0], rotationsObj[1], rotationsObj[2]);
        
        model.onPositionsChanged();
        float[] positionsObj= model.Specs.positions;
        obj.transform.localPosition = new Vector3(positionsObj[0],positionsObj[1], positionsObj[2]);
        model.editable.isOnEdit = true;
        
        deleteCurrentSelectedFromHolder();
        
    }

    public  void retrievePrefabFromPlane(GameObject selectedObjFromPlane)
    {
        //Setting the object from plane that will be edited as well
        currentSelectedOnPlane = selectedObjFromPlane;
        if (ItemHolder.childCount != 0)
        {
            deleteCurrentSelectedFromHolder();
        }
        GameObject obj = Instantiate(selectedObjFromPlane);
        changeLayer(obj,"ListItemLayer");
        obj.transform.SetParent(ItemHolder, false);
        obj.transform.position = ItemHolder.position;
        //When i set it to the plane i need to change the rotation since i dont have the same on the second camera
        float[] rotationsObj = obj.GetComponent<FurnitureModel>().Specs.rotations;
        rotationsObj[1] = rotationsObj[1]  + plane.transform.eulerAngles.y - 45f+ 180f;
        obj.transform.localEulerAngles = new Vector3(rotationsObj[0], rotationsObj[1], rotationsObj[2]);
        HolderItem holderScales= uiManager.FindPrefabWithScales(selectedObjFromPlane) ;
        ItemHolder.localScale = holderScales.scale*Vector3.one;
        Debug.Log("localScaleONItemHoolder" + ItemHolder.localScale.x +","+ ItemHolder.localScale.y +","+ ItemHolder.localScale.z);
        obj.transform.localScale = new Vector3(1,1,1);
        showRawImage();

    }
    public void changeLayer(GameObject obj,string layerName)
    {
        obj.layer = LayerMask.NameToLayer(layerName);
        if (obj.transform.childCount == 0)
        {
            return;
        }
        Transform[] childList = obj.GetComponentsInChildren<Transform>();
        foreach (Transform childTransform in childList)
        {
            GameObject child = childTransform.gameObject;
            child.layer = LayerMask.NameToLayer(layerName);
        }
    }
    private void RotateObject(float rotationAngle)
    {
        if (currentSelectedOnPlane !=null) {
            FurnitureModel furnitureModelOnPlane= currentSelectedOnPlane.GetComponent<FurnitureModel>();
            float currentRotationOnPlane = furnitureModelOnPlane.Specs.rotations[1];
            currentRotationOnPlane += rotationAngle;
            // Ensure rotation stays within 360 degrees
            currentRotationOnPlane %= 360f;
            furnitureModelOnPlane.Specs.rotations[1] = currentRotationOnPlane;
            furnitureModelOnPlane.onRotationChanaged();
        }
        GameObject obj = ItemHolder.GetChild(0).gameObject;
        FurnitureModel furnitureModel = obj.GetComponent<FurnitureModel>();
        
            float currentRotation = furnitureModel.Specs.rotations[1];
            currentRotation += rotationAngle;
            // Ensure rotation stays within 360 degrees
            currentRotation %= 360f;
            furnitureModel.Specs.rotations[1] = currentRotation;
            furnitureModel.onRotationChanaged();

    }
    public void RotateRight()
    {
        RotateObject(-45f); // Rotate right by -45 degrees
    }

    public void RotateLeft()
    {
        RotateObject(45f); // Rotate left by 45 degrees
    }
    private void ChangeColor(FurnitureModel furnitureModel)
    {
        float[,] newColors = new float[2, 4];
        newColors[0, 0] = primaryColor.color.r;
        newColors[0, 1] = primaryColor.color.g;
        newColors[0, 2] = primaryColor.color.b;
        newColors[0, 3] = primaryColor.color.a;
        furnitureModel.editable.colors.Clear();
        furnitureModel.editable.colors.Add(new Color(newColors[0, 0], newColors[0, 1], newColors[0, 2], newColors[0, 3]));
        if (furnitureModel.Specs.colors.Length > 4)
        {
            newColors[1, 0] = secondaryColor.color.r;
            newColors[1, 1] = secondaryColor.color.g;
            newColors[1, 2] = secondaryColor.color.b;
            newColors[1, 3] = secondaryColor.color.a;
            furnitureModel.editable.colors.Add(new Color(newColors[1, 0], newColors[1, 1], newColors[1, 2], newColors[1, 3]));
        }
        furnitureModel.Specs.colors = newColors;
        furnitureModel.onColorsChanged();
    }
    public void ApplyChangesButton()
    {
        EditorPanelManager.checkStatus status = editorPanelManager.checkAllFields();
        if (status == EditorPanelManager.checkStatus.OK)
        {
            editorPanelManager.resetFields();
            ChangeColorsOnGameObject();
            ResizeObject();
            uiManager.changeToGame();
            
        }
        else if (status == EditorPanelManager.checkStatus.EMPTY)
        {
            editorPanelManager.resetFields();
            ChangeColorsOnGameObject();
            Debug.Log("OnlyColors");
            uiManager.changeToGame();
        }
        else if (status == EditorPanelManager.checkStatus.ERROR)
        {
            editorPanelManager.resetFields();
            uiManager.showErrorPopUp();
        }

    }

    private void ResizeObject()
    {
        if (currentSelectedOnPlane != null)
        {
            FurnitureModel furnitureModel = currentSelectedOnPlane.GetComponent<FurnitureModel>();
            furnitureModel.Specs.scales[0] = (editorPanelManager.selectedHeight / furnitureModel.editable.initialMeasures[0]);
            furnitureModel.Specs.scales[1] = (editorPanelManager.selectedWidht / furnitureModel.editable.initialMeasures[1]);
            furnitureModel.Specs.scales[2] = (editorPanelManager.selectedDepth / furnitureModel.editable.initialMeasures[2]);
            Debug.Log("ResizesCheckingPlane:" + furnitureModel.Specs.scales[0] + "," + furnitureModel.Specs.scales[1] + "," + furnitureModel.Specs.scales[2]);
            furnitureModel.onScaleChanged();
        }
        FurnitureModel furnitureModelHolder= ItemHolder.GetChild(0).gameObject.GetComponent<FurnitureModel>();
        furnitureModelHolder.Specs.scales[0] = (editorPanelManager.selectedHeight / furnitureModelHolder.editable.initialMeasures[0]);
        furnitureModelHolder.Specs.scales[1] = (editorPanelManager.selectedWidht / furnitureModelHolder.editable.initialMeasures[1]);
        furnitureModelHolder.Specs.scales[2] = (editorPanelManager.selectedDepth / furnitureModelHolder.editable.initialMeasures[2]);
        Debug.Log("ResizesCheckingOnHolcder:" + furnitureModelHolder.Specs.scales[0] + "," + furnitureModelHolder.Specs.scales[1] + "," + furnitureModelHolder.Specs.scales[2]);
    }

    public void ChangeColorsOnGameObject()
    {

       if (currentSelectedOnPlane != null)
        {

            ChangeColor(currentSelectedOnPlane.GetComponent<FurnitureModel>());
        }
        ChangeColor(ItemHolder.GetComponentInChildren<FurnitureModel>());

    }
    public void  deleteCurrentSelectedFromHolder()
    {
        foreach (Transform child in ItemHolder)
        {
            Object.Destroy(child.gameObject);
        }
        rawImageButton.SetActive(false);
    }
    public void DeleteButton()
    {
        Destroy(currentSelectedOnPlane);
        deleteCurrentSelectedFromHolder();
        showRawImage();
        rawImageButton.SetActive(false);
    }
}
