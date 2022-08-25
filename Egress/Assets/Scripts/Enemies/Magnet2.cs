using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet2 : MagnetAIIntro
{
    private GameManager gameManager;
    private PlayerController playerController;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        speed = 1;
        maxDist = 5;
        freezeTime = 0.7f;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.health -= 20;
            if (gameManager.health <= 0)
            {
                gameManager.Reload();
            }
            gameManager.hpText.text = "Health is " + gameManager.health;
            playerController.Hurt();
            StunAfterHit();
        }
    }
}
