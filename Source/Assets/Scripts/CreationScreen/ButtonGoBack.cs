using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonGoBack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject ContainerConfirmtion;

    public void OpenConfirmation()
    {
        ContainerConfirmtion.SetActive(true);
    }
    public void CloseConfirmation()
    {
        ContainerConfirmtion.SetActive(false);
    }
    public void goBack()
    {
        SceneController.Instance.LoadScene("EscenaPrincipalHome");
        SceneManager.UnloadScene("EscenaHabitacion");
        UIManager.Instance.OpenRoomsPanel();
    }
}
