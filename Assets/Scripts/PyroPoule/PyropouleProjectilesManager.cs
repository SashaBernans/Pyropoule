using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyropouleProjectilesManager : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    private int poolSize = 10; // Nombre de projectile instancié dès le début
    private List<GameObject> projectilePool;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        InitializeProjectilePool();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            ShootProjectile();
        }
    }

    private void InitializeProjectilePool()
    {
        projectilePool = new List<GameObject>();

        // Instancie une liste de projectile inactif
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            newProjectile.SetActive(false);
            projectilePool.Add(newProjectile);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target = collision.gameObject;
            Debug.Log("Player targheted");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target = null;
        }
    }

    private void ShootProjectile()
    {
        // Trouver projectile inactif dans la liste
        GameObject newProjectile = projectilePool.Find(p => !p.activeInHierarchy);

        // Agrandir la liste si aucun inactif trouvé
        if (newProjectile == null)
        {
            newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectilePool.Add(newProjectile);
        }
        newProjectile.SetActive(true);

        newProjectile.transform.position = transform.position;
    }
}
