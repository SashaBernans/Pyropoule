using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyropoule : MonoBehaviour
{
    [SerializeField] private AssetRecycler assetRecycler;
    [SerializeField] private float fireRate;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        assetRecycler = AssetRecycler.Instance;
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        canShoot = true;
    }

    private void OnEnable()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ManageLookingDirection(collision.gameObject);
            if (canShoot)
            {
                Shoot(collision.gameObject);
                StartCoroutine(ShootDelay());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private void Shoot(GameObject target)
    {
        GameObject projectile = assetRecycler.PyropouleProjectilePool.Find(p => !p.activeInHierarchy);
        projectile.SetActive(true);
        projectile.transform.position = transform.position;
        audioSource.PlayOneShot(SoundManager.Instance.ShootFlame);
        projectile.GetComponent<ProjectileMovement>().Target = target.transform.position;
        canShoot = false;
    }

    private void ManageLookingDirection(GameObject target)
    {
        Vector2 toTarget = target.transform.position - transform.position;
        float dotProduct = Vector2.Dot(transform.right, toTarget.normalized);
        if (dotProduct > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (dotProduct < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
