using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
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
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.sceneLoaded += sceneLoadedEvent;
        LoadScene("Initializator");
    }
    private void sceneLoadedEvent(Scene scene, LoadSceneMode loadSceneMode)
    {
        DataSaver.Instance.LoadData();
        if (scene.name == "EscenaHabitacion")
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("EscenaHabitacion"));

        }
    }
    public void LoadScene(string sceneName)
    {
        DataSaver.Instance.LoadData();
        foreach (Scene scene in SceneManager.GetAllScenes())
        {
            if (scene.name == sceneName)
            {
                return;
            }
        }
         SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

    }
    
}
