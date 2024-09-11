using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<HolderItem> holderItems;
    [SerializeField]
    private GameObject slideScreen;
    [SerializeField]
    private GameObject showSlideScreen;
    [SerializeField]
    private GameObject editableScreen;
    [SerializeField]
    private GameObject errorPopUp;
    public static UiManager Instance { get; private set; }

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

    public void resetContainer(GameObject container) {
        container.transform.localScale = Vector3.one;
        foreach (Transform child in container.transform)
        {
            Object.Destroy(child.gameObject);
        }
    }

    public void changeToEditableScreen() {
        changeToGame();
        editableScreen.SetActive(true);
        
    }
    public void showErrorPopUp()
    {
        //disable rest of them
        errorPopUp.SetActive(true);
    }
    // Update is called once per frame
    public  void changeToList()
    {
        slideScreen.SetActive(true);
        showSlideScreen.SetActive(true);
    }
    public  void changeToGame()
    {
        editableScreen.SetActive(false);
        if(slideScreen.gameObject.activeInHierarchy)
            slideScreen.GetComponent<Animator>().SetTrigger("Close");
        showSlideScreen.SetActive(true);
        errorPopUp.SetActive(false);
    }

    public HolderItem FindPrefabWithScales(GameObject obj)
    {
        foreach (var item in holderItems)
        {
            if (item.prefab.gameObject.GetComponent<FurnitureModel>().Specs.renderer==  obj.gameObject.GetComponent<FurnitureModel>().Specs.renderer)
            {
                return item;
            }
        }
        return holderItems[0];
    }
}
[System.Serializable]
public struct HolderItem
{
    public GameObject prefab;
    public float scale;
   

}