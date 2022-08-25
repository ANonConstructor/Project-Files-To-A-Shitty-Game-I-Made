using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            gameManager.savedHealth = gameManager.health;
            gameManager.savedLegs = gameManager.hasLegs;
            gameManager.savedKnife = gameManager.hasKnife;
            gameManager.savedPistol = gameManager.hasPistol;
            gameManager.savedShotgun = gameManager.hasShotgun;
            gameManager.crawlingCollider.enabled = false;
            gameManager.walkingCollider.enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            gameManager.savedHealth = gameManager.health;
            gameManager.savedLegs = gameManager.hasLegs;
            gameManager.savedKnife = gameManager.hasKnife;
            gameManager.savedPistol = gameManager.hasPistol;
            gameManager.savedShotgun = gameManager.hasShotgun;
            gameManager.crawlingCollider.enabled = false;
            gameManager.walkingCollider.enabled = true;
        }
    }
}
