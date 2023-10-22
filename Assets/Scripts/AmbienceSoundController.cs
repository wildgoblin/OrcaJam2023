using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceSoundController : MonoBehaviour
{
    [SerializeField] List<AudioClip> defaultAmbienceSounds = new List<AudioClip>();

    AudioSource audioSource;

    public static AmbienceSoundController Instance;
    private void Awake()
    {
        // Ensure that there is only one instance of this object in the scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (audioSource.isPlaying) return;
        if(defaultAmbienceSounds.Count > 0)
        {
            int randIndex = Random.Range(0, defaultAmbienceSounds.Count);
            audioSource.clip = defaultAmbienceSounds[randIndex];
            audioSource.Play();
        }

    }


}
