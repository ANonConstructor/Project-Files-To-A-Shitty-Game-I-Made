using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnable : MonoBehaviour
{
    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;
    private bool done;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!done)
        {
            foreach (GameObject thingy in objectsToEnable)
            {
                thingy.SetActive(true);
            }
            foreach (GameObject thingy in objectsToDisable)
            {
                thingy.SetActive(false);
            }
            done = true;
        }
    }
}
