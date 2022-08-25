using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Boss : MonoBehaviour
{
    private Target targetScript;
    //Spawner spawner;
    public GameObject Magnet;
    public GameObject Magnet2;
    public GameObject Healthkit;

    private TMP_Text bossHealthText;
    private float randomSpawnInterval;
    private int randomNumber;
    private float randomSpawnLocation;
    public bool bossTrigger;
    private Vector2 spawnLocation;
    static private int enemiesOnScreen;
    IEnumerator Start()
    {
        targetScript = GetComponent<Target>();
        //spawner = Spawner.Instance;
        bossHealthText = GameObject.Find("BossHealth").GetComponent<TMP_Text>();
        bossHealthText.gameObject.SetActive(true);
        while (targetScript.health > 0)
        {
            yield return enemiesOnScreen > 10;
            yield return new WaitForSeconds(randomSpawnInterval);
            enemySpawnCoroutine();
        }
    }
    private void Update()
    {
        bossHealthText.text = "Block Of Neodymium: " + targetScript.health;
        enemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    void enemySpawnCoroutine()
    {
        randomSpawnInterval = Random.Range(1f, 2.5f);
        randomNumber = Random.Range(1, 21);
        randomSpawnLocation = Random.Range(1, 5);
        switch (randomSpawnLocation)
        {
            case 1: spawnLocation = new Vector2(4.5f, 10);
                break;
            case 2: spawnLocation = new Vector2(-4.5f, 10);
                break;
            case 3: spawnLocation = new Vector2(0, 15);
                break;
            case 4: spawnLocation = new Vector2(0, 5);
                break;
        }
        if (randomNumber <= 2)
        {
            Instantiate(Healthkit, spawnLocation, transform.rotation);
        }
        else if (randomNumber >= 3 && randomNumber < 14)
        {
            Instantiate(Magnet2, spawnLocation, transform.rotation);
        }
        else
        {
            Instantiate(Magnet, spawnLocation, transform.rotation);
        }
        
        
    }
}
