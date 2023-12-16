using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyropouleProjectilesManager : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private AssetRecycler assetRecycler;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        assetRecycler = AssetRecycler.Instance;
        //StartCoroutine(ShootProjectile());
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(ShootProjectile());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.gameObject;
            StartCoroutine(ShootProjectile());
            Debug.Log("Player targheted");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = null;
            StopCoroutine(ShootProjectile());
        }
    }

    IEnumerator ShootProjectile()
    {
        while (target != null)
        {
            // Trouver projectile inactif dans la liste
            GameObject newProjectile = assetRecycler.PyropouleProjectilePool.Find(p => !p.activeInHierarchy);

            if (newProjectile != null)
            {
                newProjectile.SetActive(true);
                newProjectile.transform.position = transform.position;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
