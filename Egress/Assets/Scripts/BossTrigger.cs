using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private bool done;
    public GameObject boss;
    private AudioSource playerSound;
    public AudioClip bossTheme;
    private void Start()
    {
        playerSound = GameObject.Find("Player").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!done)
        {
            boss.SetActive(true);
            playerSound.PlayOneShot(bossTheme);
            done = true;
        }
    }
}
