using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListUiPrefab : MonoBehaviour
{
    [field: SerializeField]
    public Image foto;
    [field: SerializeField]
    public TextMeshProUGUI title;
    public GameObject prefab;
    public GameObject prefabContainer;
    public void onClick()
    {
        UiManager uiManager = UiManager.Instance;
        uiManager.resetContainer(prefabContainer);
        
        HolderItem holderItem= uiManager.FindPrefabWithScales(prefab);
        GameObject uwu = Instantiate(holderItem.prefab);
        uwu.layer = LayerMask.NameToLayer("ListItemLayer");

        Transform[] childList = uwu.GetComponentsInChildren<Transform>();

        foreach (Transform child in childList)
        {
            child.gameObject.layer = LayerMask.NameToLayer("ListItemLayer");
        }

        prefabContainer.transform.localScale = Vector3.one * holderItem.scale;       
        uwu.transform.SetParent(prefabContainer.transform, false);
        uiManager.changeToGame();
        //Editable manager access for createing raw image button once change to game

        EditablesManager editablesManager = EditablesManager.Instance;
        editablesManager.showRawImage();

    }

}
