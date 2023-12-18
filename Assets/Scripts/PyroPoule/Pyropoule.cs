using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyropoule : MonoBehaviour
{
    [SerializeField] private AssetRecycler assetRecycler;
    [SerializeField] private float fireRate;

    private AudioSource audioSource;
    private bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        assetRecycler = AssetRecycler.Instance;
        audioSource = GetComponent<AudioSource>();
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

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.gameObject;
            Debug.Log("tagerting");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = null;
        }
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (canShoot)
            {
                Shoot(collision.gameObject);
                StartCoroutine(ShootDelay());
            }
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
}
