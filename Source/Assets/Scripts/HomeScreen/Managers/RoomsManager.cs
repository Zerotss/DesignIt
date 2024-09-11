using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    public static RoomsManager Instance;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Transform container;

    [SerializeField] private TMP_InputField lengthField;
    [SerializeField] private TMP_InputField widthField;
    [SerializeField] private TMP_InputField roomNameField;
    [SerializeField] private TMP_Text warningMessage;
    float[] temporaryPlanescales = new float[3];
    private string roomName;
    // Start is called before the first frame update

    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void createContainerWithButtons()
    {
        Debug.Log("uwuw");
        var rooms = DataSaver.Instance.rooms;
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Room room in rooms)
        {
            Debug.Log(room.name);
            GameObject obj = Instantiate(prefab, container);
            //Assign room to the button
            var buttonRoom = obj.GetComponent<ButtonRoom>();
            buttonRoom.room = room;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = room.name; 
        }
    }
    public void retrieveDataFromPopUp()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(lengthField.text)
                && string.IsNullOrWhiteSpace(widthField.text)
                && string.IsNullOrWhiteSpace(roomNameField.text))
            {

                warningMessage.text = "Dont leave the fields empty";
                return;
            }
            if (!(float.Parse(lengthField.text) > 200  && float.Parse(widthField.text) > 200 ))
            {
                warningMessage.text = "To small room to render";
                return;
            }
            temporaryPlanescales[0] = float.Parse(lengthField.text)/1000;
            temporaryPlanescales[1] = 0.5f;
            temporaryPlanescales[2] =float.Parse(widthField.text) / 1000;
            
            roomName = roomNameField.text;
            lengthField.text = "";
            widthField.text = "";
            roomNameField.text = "";
            warningMessage.text = "";

            asignNewRoom();
            SceneController.Instance.LoadScene("EscenaHabitacion");
            UIManager.Instance.OpenRoomsPanel();
        }
        catch (Exception e)
        {
            warningMessage.text = "Non accepted values detected";
        }

    }
    private void asignNewRoom()
    {
        DataSaver.Instance.currentRoom = DataSaver.Instance.CreateNewRoom(roomName, temporaryPlanescales);
    }
    // Update is called once per frame

}
