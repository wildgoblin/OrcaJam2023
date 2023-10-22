using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioSource musicSource;
    [SerializeField] AudioClip introClip;
    [SerializeField] AudioClip loopClip;
    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        StopMusic();
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void StartMusic()
    {
        musicSource.clip = introClip;
        musicSource.Play();
        StartCoroutine(PlayLoopsWhenMusicDone());

    }

    IEnumerator PlayLoopsWhenMusicDone()
    {
        //Play first
        yield return new WaitUntil(() => !musicSource.isPlaying);
        musicSource.clip = loopClip;
        musicSource.Play();
        StartCoroutine(PlayLoopsWhenMusicDone());
    }
}
