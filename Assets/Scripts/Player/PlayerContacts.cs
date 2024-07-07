using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContacts : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Material flashMaterial;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    Material defaultMaterial;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
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
            gameManager.LooseLife();
            audioSource.PlayOneShot(SoundManager.Instance.ChickenHurt);
            StartCoroutine(Flash());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bucket")
        {
            gameManager.addLife();
            audioSource.PlayOneShot(SoundManager.Instance.PowerUp);
        }
        if (collision.gameObject.tag == "Laser")
        {
            //print("player collision");
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
}
