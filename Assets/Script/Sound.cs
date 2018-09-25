using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
	public string Name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float Volume = 1;

	[Range(.1f, 3f)]
	public float Pitch = 1;

	public bool Loop;

	[HideInInspector]
	public AudioSource Source;
}
