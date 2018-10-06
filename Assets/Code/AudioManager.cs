using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour
{
	public Sound[] Sounds;

	public static AudioManager Instance;

	// Use this for initialization
	void Awake () {
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		foreach(Sound sound in Sounds)
		{
			sound.Source = gameObject.AddComponent<AudioSource>();
			sound.Source.clip = sound.clip;

			sound.Source.volume = sound.Volume;
			sound.Source.pitch = sound.Pitch;
			sound.Source.loop = sound.Loop;
		}
	}

	void Start()
	{
		Play("Theme");
	}
	
	public void Play(string name)
	{
		Sound sound = Array.Find(Sounds, s => s.Name == name);

		if (sound == null) return;

		sound.Source.Play();
	}
}
