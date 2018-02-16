using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {

	AudioSource music;

	// Use this for initialization
	void Start () {
		music = GetComponent<AudioSource> ();
		music.clip = Resources.Load<AudioClip> (PlayerPrefs.GetString (Const.songChoiceTokenKey));
		music.Play();
	}
}
