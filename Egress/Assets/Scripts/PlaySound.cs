using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip sound;
    private AudioSource soundSource;
    private bool done;

    private void Start()
    {
        soundSource = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!done)
        {
            soundSource.PlayOneShot(sound);
            done = true;
        }
    }
}
