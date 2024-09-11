using System;
using UnityEngine;
namespace Xeiv.AudioSystem
{
	[CreateAssetMenu(menuName = "Systems/AudioSystem/Events/AudioAskerChannel")]
	public class AudioAskerChannelSO : ScriptableObject
	{
		public Action<AudioAskerChannelSO, SoundsContainer, AudioSourceSettingsSO, Vector3> OnEventCall;

		public void CallEvent(AudioAskerChannelSO channel, SoundsContainer audioCue, AudioSourceSettingsSO audioConfiguration, Vector3 position)
		{
			OnEventCall.Invoke(channel, audioCue, audioConfiguration, position);
		}
	}
}
