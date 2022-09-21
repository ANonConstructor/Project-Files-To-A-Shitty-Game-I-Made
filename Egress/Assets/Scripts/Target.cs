using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health;
    private Coroutine flashRoutine;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Material flashMaterial;
    private Material originalMaterial;
    private AudioSource soundSource;
    public AudioClip hitSound;
    private bool boss;
    void Start()
    {
        if (gameObject.GetComponent<Magnet>() != null)
        {
            health = 6;
        }
        else if (gameObject.GetComponent<Magnet2>() != null)
        {
            health = 2;
        }
        else if (gameObject.GetComponent<Boss>() != null)
        {
            health = 100;
            boss = true;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashMaterial = new Material(flashMaterial);
        originalMaterial = spriteRenderer.material;
        soundSource = GameObject.Find("Player").GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        if(boss == true && health <= 0)
        {
            Application.Quit();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Flash(Color.white);
        soundSource.PlayOneShot(hitSound);
    }
    public void Flash(Color color)
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine(color));
    }
    private IEnumerator FlashRoutine(Color color)
    {
        spriteRenderer.material = flashMaterial;
        flashMaterial.color = color;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
    }
}
