using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemyIntro : MonoBehaviour
{
    public GameObject enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy.SetActive(true);
    }
}
