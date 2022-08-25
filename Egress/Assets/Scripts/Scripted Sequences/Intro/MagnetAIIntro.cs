using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetAIIntro : MonoBehaviour
{
    private Coroutine watchDog;
    protected Transform player;
    protected Rigidbody2D rb;
    protected float speed = 1.5f;
    protected uint maxDist = 8;
    protected uint pushForce = 200;
    protected float freezeTime = 0.5f;
    private bool FREEZE;
    private void OnEnable()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {


        if (Vector3.Distance(transform.position, player.position) <= maxDist)
        {
            Vector2 delta = (player.position - transform.position).normalized;
            float angle = Vector3.SignedAngle(Vector3.up, delta, Vector3.forward);
            Quaternion newRotation = Quaternion.Euler(0.0f, 0.0f, angle);

            transform.rotation = newRotation;
            if (!FREEZE)
            {
                rb.MovePosition((Vector2)transform.position + delta * speed * Time.deltaTime);
            }
        }
        
    }
    protected void StunAfterHit()
    {
        if(watchDog != null)
        {
            return;
        }
        else
        {
            watchDog = StartCoroutine(StunAfterHitCoroutine(freezeTime));
        }
    }
    private IEnumerator StunAfterHitCoroutine(float duration)
    {
        FREEZE = true;
        yield return new WaitForSeconds(duration);
        FREEZE = false;
    }
    
}
