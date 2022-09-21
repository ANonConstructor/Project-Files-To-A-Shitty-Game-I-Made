using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    protected GameManager gameManager;
    private PlayerController playerController;
    private AudioSource healthPickupSource;
    public AudioClip healthPickupSound;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        healthPickupSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PickUpLogic();
        }
    }
    void PickUpLogic()
    {
        if(gameObject.name == "LegsPickup")
        {
            gameManager.hasLegs = true;
        }
        else if (gameObject.name == "KnifePickup")
        {
            gameManager.hasKnife = true;
            playerController.SwapWeapon(1);
        }
        else if (gameObject.name == "PistolPickup")
        {
            gameManager.hasPistol = true;
            playerController.SwapWeapon(2);
        }
        else if (gameObject.name == "ShotgunPickup")
        {
            gameManager.hasShotgun = true;
            playerController.SwapWeapon(3);
        }
        else if (gameObject.name == "HealthPickup" || gameObject.name == "HealthPickup(Clone)")
        {
            healthPickupSource = GameObject.Find("Player").GetComponent<AudioSource>();
            healthPickupSource.PlayOneShot(healthPickupSound);
            gameManager.health = 100;
            gameManager.hpText.text = "Health is " + gameManager.health;
        }

        else
        {
            Debug.Log("You fucked up bitch");
        }
        Destroy(gameObject);
    }
}
