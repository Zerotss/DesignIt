using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseSettings : MonoBehaviour
{
    public void Close()
    {
        SceneManager.UnloadSceneAsync("Settings");
    }
    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings",LoadSceneMode.Additive);
    }
}
