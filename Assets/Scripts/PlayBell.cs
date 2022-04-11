using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBell : MonoBehaviour
{
    private AudioSource bellAudio;
    private int timesToPlay = 6;

    private void Awake()
    {
        bellAudio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int repeat = 0; repeat < timesToPlay; repeat++)
        {
            StartCoroutine(PlaySoundAfterSeconds(bellAudio.clip.length, repeat));
            Debug.Log("Played");
        }
    }

    private IEnumerator PlaySoundAfterSeconds(float delay, int repeatNumber)
    {
        yield return new WaitForSeconds(delay * repeatNumber);
        PlayClip();
    }

    private void PlayClip()
    {
        bellAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
