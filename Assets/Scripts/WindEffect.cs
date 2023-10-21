using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    [SerializeField] List<AudioClip> windSounds = new List<AudioClip>();

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<FlyingBehaviour>().WindEffect();
            if(windSounds.Count > 0)
            {
                int randIndex = Random.Range(0, windSounds.Count - 1);
                audioSource.clip = windSounds[randIndex];
                audioSource.Play();
            }

        }
    }
}
