using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectilesManager : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    //[SerializeField] private GameObject wParticleSystemPrefab;
    [SerializeField] private float projectileSpeed;
    private int poolSize = 10; // Nombre de projectile instancié dès le début

    private SpriteRenderer spriteRenderer;

    private List<GameObject> projectilePool;
    //private List<GameObject> particalSystemPool;

    //public List<GameObject> ParticalSystemPool { get => particalSystemPool; set => particalSystemPool = value; }

    void Start()
    {
        InitializeProjectilePool();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /*
    private void InitializeParticleSystemPool()
    {
        ParticalSystemPool = new List<GameObject>();

        // Instancie une liste de particalSystem inactif
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newParticalSystem = Instantiate(wParticleSystemPrefab, transform.position, Quaternion.identity);
            newParticalSystem.SetActive(false);
            ParticalSystemPool.Add(newParticalSystem);
        }
    }*/

    void Update()
    {
        if (!PauseManager.GameIsPaused)
        {
            // Devrait peut-être etre dans PlayerControls
            if (Input.GetButtonDown("Fire1"))
            {
                ShootProjectile();
            }
        }
    }

    private void InitializeProjectilePool()
    {
        projectilePool = new List<GameObject>();

        // Instancie une liste de projectile inactif
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            //newProjectile.GetComponent<WaterProjectileCollisions>().ParticalSystemGameObject = particalSystemPool[i];
            newProjectile.SetActive(false);
            projectilePool.Add(newProjectile);
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

        // Le sprite de base est pas dans le bon sens donc on fait une rotation
        newProjectile.transform.rotation = Quaternion.Euler(0,0,90f);

        Rigidbody2D projectileRb = newProjectile.GetComponent<Rigidbody2D>();
        SpriteRenderer newProjectileSR = newProjectile.GetComponent<SpriteRenderer>();

        // Vérifie dans quel direction le projectile dois aller
        if (spriteRenderer.flipX)
        {
            newProjectile.transform.position = new Vector3(transform.position.x -0.5f, transform.position.y + 0.4f, transform.position.z);
            newProjectileSR.flipY = true;
            projectileRb.velocity = new Vector2(-projectileSpeed, 0);
        }
        else
        {
            newProjectile.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.4f, transform.position.z);
            newProjectileSR.flipY = false;
            projectileRb.velocity = new Vector2(projectileSpeed, 0);
        }
    }
}
