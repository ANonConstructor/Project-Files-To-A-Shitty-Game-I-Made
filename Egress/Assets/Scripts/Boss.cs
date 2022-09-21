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
    private GameObject player;
    private GameObject boss;

    private TMP_Text bossHealthText;
    private float randomSpawnInterval;
    private int randomNumber;
    public bool bossTrigger;
    static private Vector2 spawnLocation;
    static private int enemiesOnScreen;
    IEnumerator Start()
    {
        targetScript = GetComponent<Target>();
        boss = this.gameObject;
        player = GameObject.Find("Player");
        bossHealthText = GameObject.Find("BossHealth").GetComponent<TMP_Text>();
        bossHealthText.gameObject.SetActive(true);
        while (targetScript.health > 0)
        {
            yield return enemiesOnScreen > 10;
            yield return new WaitForSeconds(randomSpawnInterval);
            enemySpawn();
        }
    }
    private void Update()
    {
        bossHealthText.text = "Block Of Neodymium: " + targetScript.health;
        enemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    void enemySpawn()
    {
        randomSpawnInterval = Random.Range(1f, 2.5f);
        randomNumber = Random.Range(1, 20);
        GenerateSpawnLocation();
        if((spawnLocation - (Vector2)player.transform.position).magnitude > 6 || (spawnLocation - (Vector2)boss.transform.position).magnitude > 9)
        {
            GenerateSpawnLocation();
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
    void GenerateSpawnLocation()
    {
        spawnLocation = new Vector2(Random.Range(-3, 4), Random.Range(7, 15));
    }
}
