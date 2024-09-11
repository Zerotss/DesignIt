using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VolumeToText : MonoBehaviour
{
    public TextMeshProUGUI _text;
    public void SetText(float volume)
    {
        _text.text = $"{(int)(volume * 100)}";
    }
}
