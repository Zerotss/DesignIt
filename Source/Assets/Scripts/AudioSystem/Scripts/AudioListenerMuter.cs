using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class AudioListenerMuter : MonoBehaviour
{
    private Toggle _toggle;
    public void MuteStatus(bool value)
    {
        AudioListener.volume = value ? 1 : 0;
        _toggle.isOn = value;
        Save();
    }

    private void OnEnable()
    {
        Load();
    }

    public void Load()
    {
        
        if (_toggle==null)
            _toggle = GetComponent<Toggle>();

        if (PlayerPrefs.HasKey("AudioListenerMuteStatus"))
        {
            AudioListener.volume = PlayerPrefs.GetInt("AudioListenerMuteStatus");
        }   
        else
        {
            
            PlayerPrefs.SetInt("AudioListenerMuteStatus", 1);
            AudioListener.volume = 1;
        }
        _toggle.isOn = AudioListener.volume == 1 ? true : false;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("AudioListenerMuteStatus", (int)AudioListener.volume);
    }
}
