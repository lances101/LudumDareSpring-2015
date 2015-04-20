using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

	public class AudioController : MonoBehaviour 
	{
		private class SoundClipEntry
		{
			public SoundClipEntry(AudioClip audio, DateTime timeStamp)
			{
				this.audioClip = audio;
				lastAccess = timeStamp;
			}

			public AudioClip audioClip;
			public DateTime lastAccess;
		};

		private Dictionary<string, SoundClipEntry> loadedAudioClips = new Dictionary<string, SoundClipEntry>();
		private int maxAudioClips = 18;
		public string audioResourcesPath = "Audio/";
		List<AudioSource> globalFxAudioSources = new List<AudioSource>();
		protected AudioSource ambientMusicSource {get; set;}
		protected AudioSource themeMusicSource {get; set;}
		private int maxAudioSources = 5;


		void Start()
		{
			for (int i=0; i<maxAudioSources; i++){
				AudioSource temp = this.gameObject.AddComponent<AudioSource>();
				temp.spatialBlend = 0.0f;
				globalFxAudioSources.Add(temp);
			}

			ambientMusicSource = this.gameObject.AddComponent<AudioSource>();
			ambientMusicSource.spatialBlend = 0.0f;
            ambientMusicSource.volume = 0.2f;
			ambientMusicSource.loop = true;
            themeMusicSource = this.gameObject.AddComponent<AudioSource>();
			themeMusicSource.spatialBlend = 0.0f;
            themeMusicSource.volume = 0.7f;
			themeMusicSource.loop = true;

            GameController.Instance.GetComponent<GameController>().AudioControllerReady();
		}

		private void AddToCache(string audioId, AudioClip clip)
		{
			if (loadedAudioClips.Count >= maxAudioClips){
				string olderKey = "";
				DateTime timeStamp = DateTime.Now;

				foreach (KeyValuePair<string, SoundClipEntry> pair in loadedAudioClips){

					if (pair.Value.lastAccess.CompareTo(timeStamp) < 0){
						olderKey = pair.Key;
						timeStamp = pair.Value.lastAccess;
					}
				}

				loadedAudioClips.Remove(olderKey);
			}

			loadedAudioClips.Add(audioId, new SoundClipEntry(clip, DateTime.Now));
		}

		public AudioClip GetAudioClip(string audioId)
		{
			AudioClip outClip = null;
			if (loadedAudioClips.ContainsKey(audioId)){
				loadedAudioClips[audioId].lastAccess = DateTime.Now;
				outClip = loadedAudioClips[audioId].audioClip;
			}

			if (outClip == null){
				outClip = Resources.Load<AudioClip>(audioResourcesPath + audioId);

				if (outClip != null){
					AddToCache(audioId, outClip);

				} 
			}

			return outClip;
		}


	    public void PlayGlobalFX(string audioId)
	    {
	        PlayGlobalFX(audioId, 1);
	    }
		public void PlayGlobalFX(string audioId, float volume)
		{
			AudioClip audioClip = GetAudioClip(audioId);

			if (audioClip != null){

				foreach (AudioSource audioSource in globalFxAudioSources){

					if (!audioSource.isPlaying){
                        audioSource.Stop();
					    audioSource.volume = volume;
						audioSource.clip = audioClip;
						audioSource.Play();
						break;
					}
				}
			}
		}

	    public bool IsFXPlaying(string audioId)
	    {
	        int count = 0;
	        AudioClip clip = GetAudioClip(audioId);
	        foreach(AudioSource audioSource in globalFxAudioSources)
	        {
	            if (audioSource.clip == clip)
	            {
	                if (audioSource.isPlaying)
	                    count++;
	            }
	        }
	        if (count > 0) return true;
	        return false;
	    }

        
		public bool PlayAmbient(string audioId)
		{
			bool outValue = false;
			AudioClip audioClip = GetAudioClip(audioId);
			
			if (audioClip != null){
				outValue = true;
                ambientMusicSource.Stop();
                ambientMusicSource.clip = audioClip;
                ambientMusicSource.Play();
			}

			return outValue;
		}

	    public void StopAmbient()
	    {	        
            ambientMusicSource.Stop();
	    }

	    public bool PlayTheme(string audioId)
	    {
            bool outValue = false;
            AudioClip audioClip = GetAudioClip(audioId);

            if (audioClip != null)
            {
                outValue = true;
                themeMusicSource.Stop();
                themeMusicSource.clip = audioClip;
                themeMusicSource.Play();
            }

            return outValue;
	    }
	}
