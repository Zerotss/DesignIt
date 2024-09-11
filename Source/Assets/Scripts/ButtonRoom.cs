using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonRoom : MonoBehaviour
{
    public Room room;

    public void deleteRoom()
    {
        DataSaver.Instance.rooms.Remove(room);
        DataSaver.Instance.SaveData();
        Destroy(gameObject);
    }
    public void enterRoom()
    {
        DataSaver.Instance.currentRoom=room;
        SceneController.Instance.LoadScene("EscenaHabitacion");
        
        
    }
}
