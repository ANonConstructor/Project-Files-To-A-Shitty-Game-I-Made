using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Muzzle flash and Damage Flash")]
    public GameObject flashObj;
    [SerializeField] private Material flashMaterial;
    private Material originalMaterial;
    private SpriteRenderer spriteRenderer;

    private Coroutine flashRoutine;
    private Coroutine muzzleflashRoutine;

    [Header("Weapons")]
    private bool isMelee;
    private bool isShotgun;
    private bool isGun;
    private float firerate;
    private Coroutine fireCoroutine;
    float fireElapsedTime = 0;

    private GameManager gameManager;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float angle;

    public LayerMask enemyMask;

    [Header("Animation")]
    private Animator animator;
    private Animator legAnimator;
    [Header("Audio")]
    public AudioSource playerSound;
    [SerializeField] private AudioClip ow;
    [SerializeField] private AudioClip pistol;
    [SerializeField] private AudioClip shotgun;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        legAnimator = GameObject.Find("Legs").GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerSound = GameObject.Find("Player").GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashMaterial = new Material(flashMaterial);
        originalMaterial = spriteRenderer.material;
    }

    void Update()
    {
        Movement();
        if(gameManager.hasLegs == true)
        {
            WeaponStuff();
        }
    }
    private void FixedUpdate()
    {
        if (gameManager.hasLegs == false)
        {
            rb.MovePosition(transform.position + movement.y * Time.fixedDeltaTime * transform.up);
        }
        else
        {
            rb.MovePosition(rb.position + movement.normalized * gameManager.walkSpeed * Time.fixedDeltaTime);
        }
    }

    private bool HasMouseMoved()
    {
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }
    private void Movement()
    {
        //Rotates player sprite to face mouse location
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 delta = transform.position - mouseWorldPos;
        delta.z = 0.0f;

        angle = Vector3.SignedAngle(Vector3.down, delta, Vector3.forward);
        Quaternion newRotation = Quaternion.Euler(0.0f, 0.0f, angle);

        transform.rotation = newRotation;

        //keyboard movement
        animator.SetFloat("Speed", movement.magnitude);
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Vertical", movement.y);

        if (gameManager.hasLegs == false)
        {
            animator.SetBool("MouseMovement", HasMouseMoved());
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            animator.SetFloat("Horizontal", movement.x);
            animator.SetBool("HasLegs", true);
            legAnimator.SetFloat("Speed", movement.magnitude);
            legAnimator.SetFloat("Horizontal", movement.x);
            legAnimator.SetFloat("Vertical", movement.y);
        }
    }
    private void WeaponStuff()
    {
        //weapon swapping
        if (Input.GetKeyDown(KeyCode.Alpha1) && gameManager.hasKnife == true)
        {
            SwapWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && gameManager.hasPistol == true)
        {
            SwapWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && gameManager.hasShotgun == true)
        {
            SwapWeapon(3);
        }

        //apply weapon stats
        switch (animator.GetInteger("CurrentWeapon"))
        {
            case 1:
                isMelee = true;
                isShotgun = false;
                isGun = false;
                firerate = 0.75f;
                break;
            case 2:
                isMelee = false;
                isShotgun = false;
                isGun = true;
                firerate = 0.5f;
                break;
            case 3:
                isMelee = false;
                isShotgun = true;
                isGun = false;
                firerate = 1.25f;
                break;
            default:
                isMelee = false;
                isShotgun = false;
                isGun = false;
                firerate = 0;
                break;
        }
        fireElapsedTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireElapsedTime >= firerate)
        {
            fireElapsedTime = 0;
            if (isMelee)
            {
                MeleeAttack(1);
            }
            else if (isShotgun)
            {
                GunAttack(3);
            }
            else
            {
                GunAttack(5);
            }
        }
    }
    private void MeleeAttack(float distance)
    {
        Collider2D[] meleeHits = Physics2D.OverlapCircleAll(flashObj.transform.position, distance, enemyMask);

        foreach(Collider2D enemy in meleeHits)
        {
            enemy.GetComponent<Target>().TakeDamage(1);
        }

        if (fireCoroutine != null)
        {
            StopCoroutine(meleeAnimation());
        }
        fireCoroutine = StartCoroutine(meleeAnimation());
    }
    private IEnumerator meleeAnimation()
    {
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isAttacking", false);
    }
    private void GunAttack(int distance)
    {
        if (flashRoutine != null)
        {
            StopCoroutine(muzzleflashRoutine);
        }
        muzzleflashRoutine = StartCoroutine(MuzzleFlashRoutine());
        if (isShotgun == true)
        {
            RaycastHit2D bulletHit = Physics2D.Raycast(transform.position, gameObject.transform.up, distance, enemyMask);
            playerSound.PlayOneShot(shotgun);
            Target target = bulletHit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(10);
            }
        }
        else if (isGun == true)
        {
            RaycastHit2D bulletHit = Physics2D.Raycast(transform.position, gameObject.transform.up, distance, enemyMask);
            playerSound.PlayOneShot(pistol);
            Target target = bulletHit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(2);
            }
        }
    }

    private IEnumerator MuzzleFlashRoutine()
    {
        flashObj.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        flashObj.SetActive(false);
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
    public void SwapWeapon(int currentWeapon)
    {
        if (gameManager.hasLegs == true)
        {
            animator.SetInteger("CurrentWeapon", currentWeapon);
        }
    }

    public void Hurt()
    {
        playerSound.PlayOneShot(ow);
        Flash(Color.white);
    }
}

