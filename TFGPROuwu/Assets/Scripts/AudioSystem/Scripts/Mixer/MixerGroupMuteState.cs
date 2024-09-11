using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerGroupMuteState : MonoBehaviour
{
    public AudioMixerGroup group;
    public Button muteBtn;
    public Button unmuteBtn;

    private bool state;

    private void OnEnable()
    {
        string _name_ = group.name;
        if (PlayerPrefs.HasKey(_name_ + "MuteState"))
        {
            state = PlayerPrefs.GetFloat(_name_ + "MuteState") == 1 ? true : false;
        }    
        else
        {
            PlayerPrefs.SetFloat(_name_ + "MuteState", 0);

        }
        PlayerPrefs.Save();

        if (state)
            MuteWithClick();
        else
            UnmuteWithClick();

        Save();
    }

    private void OnDisable()
    {
        Save();
    }

    public void Save()
    {
        string _name_ = group.name;
        PlayerPrefs.SetFloat(_name_ + "MuteState", state ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void MuteWithClick()
    {
        state = true;
        muteBtn.onClick.Invoke();
        muteBtn.gameObject.SetActive(false);
        unmuteBtn.gameObject.SetActive(true);
        Save();
    }

    public void UnmuteWithClick()
    {
        state = false;
        unmuteBtn.onClick.Invoke();
        muteBtn.gameObject.SetActive(true);
        unmuteBtn.gameObject.SetActive(false);
        Save();
    }

    public void Mute()
    {
        state = true;
        muteBtn.gameObject.SetActive(false);
        unmuteBtn.gameObject.SetActive(true);
        Save();

    }

    public void Unmute()
    {
        state = false;
        muteBtn.gameObject.SetActive(true);
        unmuteBtn.gameObject.SetActive(false);
        Save();
    }
}
