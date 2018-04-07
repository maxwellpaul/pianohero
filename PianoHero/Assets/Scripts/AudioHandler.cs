using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {

	AudioSource music;
	const float delay = 2.2f;

	void Start () {
		music = GetComponent<AudioSource> ();
        string songName = Utility.songChoiceToken;
		LoadFile(PlayerPrefs.GetString(Const.resourcePathKey) + "WAVFiles/" + songName + ".wav");
		music.PlayDelayed (delay);
	}

	void LoadFile (string path) {
		WWW www = new WWW("file://" + path);
		AudioClip clip = www.GetAudioClip();
		while (!clip.isReadyToPlay);
		clip = www.GetAudioClip(false);
		music.clip = clip;
	}

    public void PauseSong() {
        print("PAUSED");
        music.Pause();
    }

    public void ResumeSong() {
        print("RESUME");
        music.Play();
    }
}