using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearCoroutine : MonoBehaviour
{
    public AudioClip doorSound;
    public AudioSource sfxSrc;
    void Start()
    {
        Invoke("Destroy", 35);
    }

    private void Destroy()
    {
        Destroy(gameObject);
        sfxSrc.PlayOneShot(doorSound);
    }
}
