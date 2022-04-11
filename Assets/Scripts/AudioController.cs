using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController aCtrl;
    //public GameObject bgMusic1;
    //public GameObject bgMusic2;
    public AudioSource sfxSrc;
    private AudioSource levelMusic;
    private List<AudioSource> musicTracks;
    private int currentTrackNumber;
    public List<GameObject> musicSources;

    public void Awake()
    {
        if (aCtrl == null)
        {
            musicTracks = new List<AudioSource>();
            //musicTracks.Add(bgMusic1.GetComponent<AudioSource>());
            //musicTracks.Add(bgMusic2.GetComponent<AudioSource>());
            SetUpMusic();
            currentTrackNumber = 0;
            levelMusic = musicTracks[currentTrackNumber];
            levelMusic.loop = true;
            aCtrl = this;
        }
    }

    private void SetUpMusic()
    {
        foreach (GameObject track in musicSources)
        {
            musicTracks.Add(track.GetComponent<AudioSource>());
        }
    }

    public void PlaySFX()
    {
        aCtrl.sfxSrc.Play();
    }

    public void StopMusic()
    {
        levelMusic.Stop();
    }

    public void PauseMusic()
    {
        levelMusic.Pause();
    }

    public void PlayMusic()
    {
        levelMusic.Play();
    }

    public void PreviousTrack()
    {
        if (currentTrackNumber == 0)
        {
            currentTrackNumber = musicTracks.Count - 1;
        }
        else
        {
            currentTrackNumber--;
        }
        levelMusic.Stop();
        levelMusic = musicTracks[currentTrackNumber];
        levelMusic.loop = true;
        levelMusic.Play();
    }

    public void NextTrack()
    {
        if (currentTrackNumber == (musicTracks.Count - 1))
        {
            currentTrackNumber = 0;
        }
        else
        {
            currentTrackNumber++;
        }
        levelMusic.Stop();
        levelMusic = musicTracks[currentTrackNumber];
        levelMusic.loop = true;
        levelMusic.Play();
    }
}