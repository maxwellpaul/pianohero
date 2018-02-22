using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {

	AudioSource music;

	// Use this for initialization
	void Start () {
		music = GetComponent<AudioSource> ();
		LoadFile(Const.LocalWAVPath + PlayerPrefs.GetString (Const.songChoiceTokenKey) + ".wav");
		music.PlayDelayed (2.2f);
	}

	void LoadFile (string path) {
		WWW www = new WWW("file://" + path);
		AudioClip clip = www.GetAudioClip();
		while (!clip.isReadyToPlay);
		clip = www.GetAudioClip(false);
		music.clip = clip;
	}
}