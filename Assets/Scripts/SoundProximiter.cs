using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundProximiter : MonoBehaviour
{
    public float proximityDistance = 5f;  // Distance at which the sound starts playing

    AudioSource audioSource;
    private bool isPlaying = false;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        // Calculate volume based on proximity
        float volume = 1f - Mathf.Clamp01(distanceToPlayer / proximityDistance);

        audioSource.volume = volume;
        // Check if the player is within proximityDistance units from the sound emitter
        if (distanceToPlayer < proximityDistance && !audioSource.isPlaying)
        {
            audioSource.Play();                
        }

        else if(distanceToPlayer >= proximityDistance && audioSource.isPlaying)
        {
            audioSource.Stop();
        }


        


    }

    // Draw Gizmo to visualize the proximity distance
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, proximityDistance);
    }

}
