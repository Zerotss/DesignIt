using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<FurnitureModel> resourcesList;
    [SerializeField]
    private GameObject itemContainer;
    [SerializeField]
    private Transform listContainer;
    [SerializeField]
    private GameObject prefabContainer;
    public FurnitureType enumUwu;
    void Start()
    {
        changeToLivingRoom();  
        resourcesList = new List<FurnitureModel>(Resources.LoadAll<FurnitureModel>("Furniture"));
        redoList();

    }
    private void redoList()
    {
        resetList();
        for (int i = 0; i < resourcesList.Count; i++)
        {
            if (resourcesList[i].Type == enumUwu)
            {
                GameObject imageGO = Instantiate(itemContainer);
                imageGO.transform.SetParent(listContainer, false);

                ListUiPrefab component = imageGO.transform.GetComponent<ListUiPrefab>();

                component.foto.sprite = resourcesList[i].Image;
                component.prefab = resourcesList[i].gameObject;
                component.title.text = component.prefab.GetComponent<FurnitureModel>().Name;
                component.prefabContainer = prefabContainer;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void  resetList()
    {
        foreach (Transform child in listContainer)
        {
            Object.Destroy(child.gameObject);
        }
    }
    public void changeToLivingRoom()
    {
        enumUwu = FurnitureType.LIVING_ROOM;
        redoList();
    }
    public void changeToKitchen()
    {
        enumUwu = FurnitureType.KITCHEN;
        redoList();

    }
    public void changeToBedroom()
    {
        enumUwu = FurnitureType.BEDROOM;
        redoList();
    }
    public void changeToWC()
    {
        enumUwu = FurnitureType.WC;
        redoList();
    }
    public List<FurnitureModel> getList()
    {
        return resourcesList;
    }

}
