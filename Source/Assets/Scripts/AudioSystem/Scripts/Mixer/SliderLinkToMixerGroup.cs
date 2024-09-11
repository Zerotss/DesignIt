using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

[RequireComponent(typeof(Slider))]
public class SliderLinkToMixerGroup : MonoBehaviour
{
    private Slider slider;
    public AudioMixerGroup Group;

    private void Start()
    {
        slider=GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat(Group.name);
    }

    public void UpdateVolume(float value)
    {
        Group.audioMixer.SetFloat(Group.name, Mathf.Log10(value) * 20);
    }
}
