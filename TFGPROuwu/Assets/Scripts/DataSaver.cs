using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public string userId;
    public List<Room> rooms; 
    DatabaseReference database;
    public static DataSaver Instance;
    public Room currentRoom;
    FirebaseAuth auth;
    // Start is called before the first frame update
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            database = FirebaseDatabase.DefaultInstance.RootReference;
            auth = FirebaseAuth.DefaultInstance;
            DontDestroyOnLoad(gameObject);
            
        }
    }

    public void Sign_Out()
    {
        auth.SignOut();
    }
    public void SaveData()
    {
        Debug.Log("Estoy intentando guardar datos");    
       string json = JsonConvert.SerializeObject(rooms);
        Debug.Log($"{database}");
        Debug.Log(json);
        database.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }
    public void LoadData()
    {
        StartCoroutine(LoadDataEnum( ));
        
    }
    IEnumerator LoadDataEnum()
    {
        Debug.Log("UserId once loading" + DataSaver.Instance.userId);
        var serverData= database.Child("users").Child(userId).GetValueAsync();
        yield return new WaitUntil(predicate: () => serverData.IsCompleted);
        DataSnapshot snapshot = serverData.Result;
        string jsonData = snapshot.GetRawJsonValue();

        if (string.IsNullOrEmpty(jsonData))
        {
            rooms = new List<Room>();
            RoomsManager.Instance.createContainerWithButtons();

        }
        else
        {
            rooms = JsonConvert.DeserializeObject<List<Room>>(jsonData);
 
           RoomsManager.Instance.createContainerWithButtons();
            AssignIndexesToRooms();
        }

        

    }
    public void ModifyAndSaveRoom( Room modifiedRoom)
    {
        if (modifiedRoom.index < 0 || modifiedRoom.index >= rooms.Count)
        {
            return;
        }

        rooms[modifiedRoom.index] = modifiedRoom;
        SaveData();
    }
    private void AssignIndexesToRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            rooms[i].index = i;
        }
    }
    public Room CreateNewRoom(string name, float[] planeScales)
    {
        Room newRoom = new()
        {
            name = name,
            index = (rooms.Count > 0)? rooms.Count : 0, // Asignamos el índice de la nueva habitación rooms.Count
            planeScales = planeScales,
            furnitures = new List<FurnitureSpecs>()
    };

        rooms.Add(newRoom);
        Debug.Log(rooms.Count);
        SaveData();
        return newRoom;
        
    }
}
[System.Serializable]

public class Room
{
    public string name;
    public int index;
    public float[] planeScales;
    public List<FurnitureSpecs> furnitures;
}
