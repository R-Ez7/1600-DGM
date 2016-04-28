using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

	//We are using this script to play our audio clips

	public AudioSource efxSource;
	public AudioSource musicSource;
	public static Sounds instance = null;

	public float lowPitchRange = .95f;
	public float highPitchRange = 1.05f;


	void Awake () 
	{
		if (instance == null)
						instance = this;
				else if (instance != this)
						Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}

	public void PlayClips (AudioClip clip)
	{
		efxSource.clip = clip;
		efxSource.Play ();
	}

	public void RandomizeSfx (params AudioClip [] clips)
	{
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		efxSource.pitch = randomPitch;
		efxSource.clip = clips [randomIndex];
		efxSource.Play();
	}

}
