using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectilesManager : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private AssetRecycler assetRecycler;
    [SerializeField] private float firerate;

    private SpriteRenderer spriteRenderer;
    private bool canFire = true;

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
            if (Input.GetMouseButton(0))
            {
                if (canFire == true)
                {
                    StartCoroutine(Firerate());
                    Vector2 mouseScreenPosition = Input.mousePosition;
                    Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
                    ShootProjectile(mouseWorldPosition);
                    canFire = false;
                }
            }
        }
    }

    IEnumerator Firerate()
    {
        yield return new WaitForSeconds(firerate);
        this.canFire = true;
    }

    private void ShootProjectile(Vector2 target)
    {
        // Trouver projectile inactif dans la liste
        GameObject newProjectile = assetRecycler.PlayerProjectilePool.Find(p => !p.activeInHierarchy);

        if (newProjectile != null)
        {
            newProjectile.SetActive(true);
            newProjectile.transform.position = this.transform.position;
            ProjectileMovement pm = newProjectile.GetComponent<ProjectileMovement>();
            pm.Target = target;
            pm.ManageRotation();

            /*// Le sprite de base est pas dans le bon sens donc on fait une rotation
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
            }*/
        }
    }
}
