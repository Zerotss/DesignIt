using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

namespace Xeiv.AudioSystem
{
	[System.Serializable]
	public class AudioAskerChannelData
	{
		public AudioAskerChannelSO Channel;
		[HideInInspector] public AudioSourceManager AudioSource;
		public Coroutine coroutineController;

		public float FadeInValue;
		public float FadeOutValue;
	}


	public class AudioManager : MonoBehaviour
	{
		private static AudioManager Instance;

        [Header("SoundEmitters pool")]
        [SerializeField] private int EffectsAudioSourceQuantity = 20;
        [SerializeField] private GameObject AudioSourcePrefab;
		private List<AudioSourceManager> EffectsAudioSourceUsablePool = new List<AudioSourceManager>();
		private List<AudioSourceManager> EffectsAudioSourceUsedPool = new List<AudioSourceManager>();

		[Header("Listening on channels")]
		[SerializeField] private AudioAskerChannelSO _SFXEventChannel = default;
		[SerializeField] private AudioAskerChannelData[] _IndividualChannels;
		[SerializeField] private Dictionary<AudioAskerChannelSO, AudioAskerChannelData> _IndividualChannelManager;

        [Header("SoundEmitters Control Events")]
        [SerializeField] private VoidChannelSO StopAllEvent;
		[SerializeField] private VoidChannelSO PauseAllEvent;
		[SerializeField] private VoidChannelSO PlayAllEvent;

        private void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject); return;
			}
			Instance = this;
			DontDestroyOnLoad(gameObject);
			for (int i = 0; i < EffectsAudioSourceQuantity; i++)
			{
				EffectsAudioSourceUsablePool.Add(Instantiate(AudioSourcePrefab, transform).GetComponent<AudioSourceManager>());
				EffectsAudioSourceUsablePool[i].gameObject.name = "EffectAudioSource";
			}

			_IndividualChannelManager = new Dictionary<AudioAskerChannelSO, AudioAskerChannelData>();

			for (int i = 0; i < _IndividualChannels.Length; i++)
			{
				_IndividualChannels[i].AudioSource = Instantiate(AudioSourcePrefab, transform).GetComponent<AudioSourceManager>();
				_IndividualChannels[i].AudioSource.gameObject.name = _IndividualChannels[i].Channel.name;
				_IndividualChannelManager.Add(_IndividualChannels[i].Channel, _IndividualChannels[i]);
				_IndividualChannels[i].Channel.OnEventCall += PlayIndividualChannel;
			}
			_SFXEventChannel.OnEventCall += PlayAudio;

			StopAllEvent.OnEventCall += StopAll;
			PauseAllEvent.OnEventCall += PauseAll;
			PlayAllEvent.OnEventCall += PlayAll;

        }

		private void OnDisable()
		{
			_SFXEventChannel.OnEventCall -= PlayAudio;
			StopAllEvent.OnEventCall -= StopAll;
			PauseAllEvent.OnEventCall -= PauseAll;
			PlayAllEvent.OnEventCall -= PlayAll;
			for (int i = 0; i < _IndividualChannels.Length; i++)
			{
				_IndividualChannels[i].Channel.OnEventCall -= PlayIndividualChannel;
			}
        }

		public void PlayAudio(AudioAskerChannelSO channel, SoundsContainer audioCue, AudioSourceSettingsSO settings, Vector3 position = default)
		{

			AudioClip[] clipsToPlay = audioCue.GetClips();

			int nOfClips = clipsToPlay.Length;
			if (EffectsAudioSourceUsablePool.Count < nOfClips)
				return;
			for (int i = 0; i < nOfClips; i++)
			{
				AudioSourceManager soundEmitter = EffectsAudioSourceUsablePool[0];
				EffectsAudioSourceUsablePool.Remove(soundEmitter);
				EffectsAudioSourceUsedPool.Add(soundEmitter);
				if (soundEmitter != null)
				{
					soundEmitter.PlayAudioClip(clipsToPlay[i], settings, audioCue.looping, position);
					if (!audioCue.looping)
						soundEmitter.OnFinish += OnSoundEmitterFinishedPlaying;
				}
			}
		}

		public void PlayIndividualChannel(AudioAskerChannelSO channel, SoundsContainer audioCue, AudioSourceSettingsSO settings, Vector3 position = default)
		{

			AudioClip[] clipsToPlay = audioCue.GetClips();
			if (clipsToPlay != null)
			{
				if (clipsToPlay.Length != 0)
				{
					AudioClip clip = clipsToPlay[Random.Range(0, clipsToPlay.Length)];
					if (clip == _IndividualChannelManager[channel].AudioSource._audioSource.clip)
						return;
					if (_IndividualChannelManager[channel].coroutineController != null)
						StopCoroutine(_IndividualChannelManager[channel].coroutineController);
					_IndividualChannelManager[channel].coroutineController = StartCoroutine(FadeOut(_IndividualChannelManager[channel].AudioSource, clip, audioCue.looping, settings, _IndividualChannelManager[channel].FadeOutValue, _IndividualChannelManager[channel].FadeInValue));
					return;
				}
			}

			if (_IndividualChannelManager[channel].coroutineController != null)
				StopCoroutine(_IndividualChannelManager[channel].coroutineController);
			_IndividualChannelManager[channel].coroutineController = StartCoroutine(FadeOut(_IndividualChannelManager[channel].AudioSource, null, audioCue.looping, settings, _IndividualChannelManager[channel].FadeOutValue, _IndividualChannelManager[channel].FadeInValue));

		}


		private void OnSoundEmitterFinishedPlaying(AudioSourceManager soundEmitter)
		{
			soundEmitter.OnFinish -= OnSoundEmitterFinishedPlaying;
			soundEmitter.Stop();
			EffectsAudioSourceUsablePool.Add(soundEmitter);
			EffectsAudioSourceUsedPool.Remove(soundEmitter);
		}

		public IEnumerator FadeOut(AudioSourceManager controller, AudioClip clip, bool looping, AudioSourceSettingsSO settings, float timeToFadeOut, float timeToFadeIn, Vector3 position = default)
		{
			if (controller._audioSource.clip != null)
			{
				DOTweenModuleAudio.DOFade(controller._audioSource, 0, timeToFadeOut);
				yield return new WaitForSeconds(timeToFadeOut);
			}

			if (clip != null)
			{
				controller.PlayAudioClip(clip, settings, looping, position);
				controller._audioSource.volume = 0;
				DOTweenModuleAudio.DOFade(controller._audioSource, settings.Volume, timeToFadeIn);
			}
			else
				controller._audioSource.clip = null;


		}


		public void StopAll()
		{
			foreach (var item in EffectsAudioSourceUsedPool)
			{
				item.Stop();
			}
			foreach (var item in _IndividualChannels)
			{
				item.AudioSource.Stop();
			}
		}

		public void PauseAll()
		{
			foreach (var item in EffectsAudioSourceUsedPool)
			{
				item.Pause();
			}
			foreach (var item in _IndividualChannels)
			{
				item.AudioSource.Pause();
			}
		}

		public void PlayAll()
		{
			foreach (var item in EffectsAudioSourceUsedPool)
			{
				item.Resume();
			}
			foreach (var item in _IndividualChannels)
			{
				item.AudioSource.Resume();
			}
		}
	}
}
