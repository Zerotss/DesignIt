using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static EditorPanelManager;
using UnityEngine.UIElements;
using Xeiv.AudioSystem;

public class SaverManager : MonoBehaviour
{
    [SerializeField]
    private Transform FurnitureHolder;
    [SerializeField]
    private Transform Plane;
    [SerializeField]
    public List<GameObject> resourcesList;

    

    private void Awake()
    {
        resourcesList = new List<GameObject>(Resources.LoadAll<GameObject>("Furniture"));
        loadRoomInGame();
    }
    // Start is called before the first frame update

    // si lo cierra empezará con una habitacion 5x5
    //Preguntar de cuanto
    
    private List<FurnitureSpecs> retrieveAllSpecs() 
    {
            var furnituresInGame = FurnitureHolder.GetComponentsInChildren<FurnitureModel>();
            List<FurnitureSpecs> specsList = new List<FurnitureSpecs> ();

            foreach (var item in furnituresInGame)
            {
                specsList.Add(item.Specs);
            }
            foreach (var furniture in specsList)
            {
                Debug.Log(furniture.ToString());
            }
            return specsList;
     }
    public void saveAll()
     {
        DataSaver.Instance.SaveData();
     }
    
    public void modifyRoomAndSave()
    {
        Room room = DataSaver.Instance.currentRoom;
        //The only thing you will be able to modify from the room is its furniture
        room.furnitures=retrieveAllSpecs();
        DataSaver.Instance.ModifyAndSaveRoom(room);
    }
    public void loadRoomInGame()
    {
        Room room = DataSaver.Instance.currentRoom;
        Plane.localScale = new Vector3(room.planeScales[0], room.planeScales[1], room.planeScales[2]);
        if (room.furnitures == null) room.furnitures = new List<FurnitureSpecs>();
        foreach (var item in room.furnitures)
        {
            foreach(var furnitureModel in resourcesList)
            {
                if(item.renderer ==  furnitureModel.GetComponent<FurnitureModel>().Specs.renderer)
                {
                    Debug.Log(item.positions[0].ToString());
                    GameObject objeto = Instantiate(furnitureModel);
                    objeto.transform.SetParent(FurnitureHolder);
                    var model = objeto.GetComponent<FurnitureModel>();
                    var furnitureSpecs = model.Specs;

                    furnitureSpecs.positions = item.positions;
                    furnitureSpecs.colors= item.colors;
                    furnitureSpecs.scales = item.scales;
                    furnitureSpecs.rotations = item.rotations;

                    model.onPositionsChanged();
                    model.onColorsChanged();
                    model.onRotationChanaged();
                    model.onScaleChanged();
                }
            }
        }



    }

}
