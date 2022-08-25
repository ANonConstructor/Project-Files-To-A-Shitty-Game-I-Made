using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MagnetAIIntro
{
    private GameManager gameManager;
    private PlayerController playerController;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.health -= 25;
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
