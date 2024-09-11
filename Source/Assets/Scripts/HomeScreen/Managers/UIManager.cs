using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private GameObject loginPanel;

    [SerializeField]
    private GameObject registrationPanel;

    [SerializeField]
    private GameObject entryPanel;
    [SerializeField]
    private GameObject roomsPanel;
    [SerializeField]
    private GameObject popUp;


    private void Awake()
    {
        CreateInstance();
        OpenEntryPanel();
        
    }

    private void CreateInstance()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void OpenLoginPanel()
    {

        loginPanel.SetActive(true);
        registrationPanel.SetActive(false);
        entryPanel.SetActive(false);
        roomsPanel.SetActive(false);
        popUp.SetActive(false);
    }
    public void OpenEntryPanel()
    {
        loginPanel.SetActive(false);
        registrationPanel.SetActive(false);
        entryPanel.SetActive(true);
        popUp.SetActive(false);
        roomsPanel.SetActive(false);
    }

    public void OpenRegistrationPanel()
    {
        loginPanel.SetActive(false);
        registrationPanel.SetActive(true);
        entryPanel.SetActive(false);
        roomsPanel.SetActive(false);
        popUp.SetActive(false);
    }
    public void OpenRoomsPanel()
    {
        loginPanel.SetActive(false);
        registrationPanel.SetActive(false);
        entryPanel.SetActive(false);
        roomsPanel.SetActive(true);
        popUp.SetActive(false);
        
    }
    public void showPopUpNewRoom()
    {
        loginPanel.SetActive(false);
        registrationPanel.SetActive(false);
        entryPanel.SetActive(false);
        roomsPanel.SetActive(true);
        popUp.SetActive(true);
    }
}
