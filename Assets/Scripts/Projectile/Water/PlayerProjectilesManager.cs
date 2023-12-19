using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectilesManager : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private AssetRecycler assetRecycler;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        assetRecycler = AssetRecycler.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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

    private void ShootProjectile()
    {
        // Trouver projectile inactif dans la liste
        GameObject newProjectile = assetRecycler.PlayerProjectilePool.Find(p => !p.activeInHierarchy);

        if (newProjectile != null)
        {
            newProjectile.SetActive(true);

            // Le sprite de base est pas dans le bon sens donc on fait une rotation
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, 90f);

            Rigidbody2D projectileRb = newProjectile.GetComponent<Rigidbody2D>();
            SpriteRenderer newProjectileSR = newProjectile.GetComponent<SpriteRenderer>();

            // Vérifie dans quel direction le projectile dois aller
            if (spriteRenderer.flipX)
            {
                newProjectile.transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.4f, transform.position.z);
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
}
