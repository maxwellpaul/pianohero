using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {

    public GameManager gameManager;
	AudioSource music;
    private bool musicStart = false;
	const float delay = 2.2f;

    private void Update()
    {
        if(gameManager.ready && !musicStart) {
            musicStart = true;
            StartSong();
        }
    }

    public void StartSong () {
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
        StartCoroutine(waitToContinue());
    }

    IEnumerator waitToContinue() {
        yield return new WaitForSeconds(5);
        music.UnPause();
    }
}