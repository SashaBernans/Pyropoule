using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    private float projectileSpeed = 10f;
    private int poolSize = 10; // Number of projectiles to preallocate in the pool

    private List<GameObject> projectilePool;

    void Start()
    {
        // Initialize the projectile pool
        InitializeProjectilePool();
    }

    void Update()
    {
        // Check for player input to shoot projectiles
        if (Input.GetButtonDown("Fire1"))
        {
            ShootProjectile();
        }
    }

    private void InitializeProjectilePool()
    {
        projectilePool = new List<GameObject>();

        // Create and instantiate projectiles in the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            newProjectile.SetActive(false); // Deactivate the projectile initially
            projectilePool.Add(newProjectile);
        }
    }

    private void ShootProjectile()
    {
        // Find an inactive projectile in the pool
        GameObject newProjectile = projectilePool.Find(p => !p.activeInHierarchy);

        // If no inactive projectile is found, expand the pool by instantiating a new one
        if (newProjectile == null)
        {
            newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectilePool.Add(newProjectile);
        }

        // Set the position and activate the projectile
        newProjectile.SetActive(true);
        newProjectile.transform.position = transform.position;

        // Get the Rigidbody2D component of the projectile
        Rigidbody2D projectileRb = newProjectile.GetComponent<Rigidbody2D>();

        // Apply force to the projectile to make it move upward
        projectileRb.velocity = new Vector2(10f, 0);
        
        //Vector2 target = new Vector2(1,transform.position.y);
        //newProjectile.transform.Translate(target);
    }
}
