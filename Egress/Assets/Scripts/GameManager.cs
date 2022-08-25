using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("Other shit")]
    public TMP_Text hpText;
    public int walkSpeed = 5;
    public int health;
    public BoxCollider2D crawlingCollider;
    public BoxCollider2D walkingCollider;

    private static GameManager Instance;

    [Header("Pickups")]
    public bool hasLegs = false;
    public bool hasKnife = false;
    public bool hasPistol = false;
    public bool hasShotgun = false;

    [Header("Do you Remember?")]
    public int savedHealth;

    public bool savedLegs;
    public bool savedKnife;
    public bool savedPistol;
    public bool savedShotgun;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        hpText = GameObject.Find("Health").GetComponent<TMP_Text>();
        hpText.text = "Health is " + health;

        savedHealth = health;
        savedLegs = hasLegs;
        savedKnife = hasKnife;
        savedPistol = hasPistol;
        savedShotgun = hasShotgun;
    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        health = savedHealth;
        hasLegs = savedLegs;
        hasKnife = savedKnife;
        hasPistol = savedPistol;
        hasShotgun = savedShotgun;
        hpText = GameObject.Find("Health").GetComponent<TMP_Text>();
        hpText.text = "Health is " + health;
        crawlingCollider.enabled = false;
        walkingCollider.enabled = true;
    }
}
