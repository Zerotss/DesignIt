using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoadMixerVolumes : MonoBehaviour
{
    public AudioMixer audioMixer;
    private AudioMixerGroup[] GroupsToSaveLoad;
    [Range(0.001f, 1)] public float DefaultVolume=1;

    private void Start()
    {
        GroupsToSaveLoad = audioMixer.FindMatchingGroups(string.Empty);
        Load();
    }
    public void Load()
    {
        for (int i = 0; i < GroupsToSaveLoad.Length; i++)
        {
            string _name_ = GroupsToSaveLoad[i].name;
            if (PlayerPrefs.HasKey(_name_))
                audioMixer.SetFloat(_name_, Mathf.Log10(PlayerPrefs.GetFloat(_name_)) * 20);
            else
            {
                audioMixer.SetFloat(_name_, Mathf.Log10(DefaultVolume) * 20);
                PlayerPrefs.SetFloat(_name_, DefaultVolume);

            }
        }
        PlayerPrefs.Save();
    }
}
