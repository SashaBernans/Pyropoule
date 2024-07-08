using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContacts : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private HealthSystem healthSystem;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    Material defaultMaterial;

    private bool canBeHit = true;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        healthSystem = HealthSystem.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Flame")
        {
            audioSource.PlayOneShot(SoundManager.Instance.ChickenHurt);
            StartCoroutine(Flash());
            healthSystem.TakeDamage(10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bucket")
        {
            healthSystem.HealDamage(50);
            audioSource.PlayOneShot(SoundManager.Instance.PowerUp);
        }
        if (collision.gameObject.tag == "Laser")
        {
            if (canBeHit)
            {
                audioSource.PlayOneShot(SoundManager.Instance.ChickenHurt);
                StartCoroutine(Flash());
                healthSystem.TakeDamage(10);
                canBeHit = false;
                StartCoroutine(ManageLaserHits());
            }
        }
    }

    IEnumerator Flash()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(0.125f);
        spriteRenderer.material = defaultMaterial;
        yield return new WaitForSeconds(0.125f);
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(0.125f);
        spriteRenderer.material = defaultMaterial;
    }

    IEnumerator ManageLaserHits()
    {
        yield return new WaitForSeconds(0.3f);
        canBeHit = true;
    }
}
