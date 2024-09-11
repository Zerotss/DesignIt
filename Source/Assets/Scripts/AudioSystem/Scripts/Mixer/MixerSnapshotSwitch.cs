using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName ="Systems/AudioSystem/SnapshotSwitch")]
public class MixerSnapshotSwitch : ScriptableObject
{
    public AudioMixerSnapshot snapshot;

    public void SwitchTo(float time)
    {
        snapshot.TransitionTo(time);
    }
}
