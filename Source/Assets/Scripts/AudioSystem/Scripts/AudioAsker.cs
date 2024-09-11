using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xeiv.AudioSystem
{
	public class AudioAsker : MonoBehaviour
	{
		[Header("Sound definition")]
		[SerializeField] private SoundsContainer _audioContainer = default;
		[SerializeField] private bool _playOnStart = false;
		[SerializeField] private float _deleyOnStart = 0;

		[Header("Configuration")]
		[SerializeField] private AudioAskerChannelSO _audioAskerChannel = default;
		[SerializeField] private AudioSourceSettingsSO _audioConfiguration = default;

		private void Start()
		{
			if (_playOnStart)
				Invoke("PlayAudioCue",_deleyOnStart);
		}

		public void PlayAudioCue()
		{
			if (enabled)
				_audioAskerChannel.CallEvent(_audioAskerChannel, _audioContainer, _audioConfiguration, transform.position);
		}

		public void SetAudios(SoundsContainer container)
		{
			_audioContainer = container;
		}

		public void SetUp(AudioAskerChannelSO channel, SoundsContainer sounds, AudioSourceSettingsSO config)
		{
			_audioAskerChannel = channel;
			_audioContainer = sounds;
			_audioConfiguration = config;
		}

		public void Disable()
		{
			enabled = false;
		}
		public void Enable()
		{
			enabled = true;
		}
	}
}