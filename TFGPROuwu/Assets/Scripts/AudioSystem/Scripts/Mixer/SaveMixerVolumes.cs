using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SaveMixerVolumes : MonoBehaviour
{
    public AudioMixer audioMixer;
    private AudioMixerGroup[] GroupsToSaveLoad;

    private void OnEnable()
    {
        GroupsToSaveLoad = audioMixer.FindMatchingGroups(string.Empty);
    }
    private void OnDisable()
    {
        SaveOptionsConfig();
    }

    public void SaveOptionsConfig()
    {

        for (int i = 0; i < GroupsToSaveLoad.Length; i++)
        {
            string _name_ = GroupsToSaveLoad[i].name;
            GroupsToSaveLoad[i].
            audioMixer.GetFloat(_name_, out float volume);

            float convertedVolume = Mathf.Pow(10,(volume/20));
            PlayerPrefs.SetFloat(_name_, convertedVolume);
        }
        PlayerPrefs.Save();
    }
}



