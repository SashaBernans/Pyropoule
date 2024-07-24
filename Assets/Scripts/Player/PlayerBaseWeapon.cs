using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseWeapon : Weapon, IUpgradeable
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private AssetRecycler assetRecycler;
    [SerializeField] private float firerate;
    [SerializeField] private int damage;

    private bool canFire = true;

    private const string UPGRADE_BASE_WEAPON = "Upgrade damage by 25%";
    private const string UPGRADE_TITLE = "Spit level ";
    private int level = 1;

    public override float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public override float AttackSpeed { get => firerate; set => firerate = value; }
    public override int Damage { get => damage; set => damage = value; }

    void Start()
    {
        assetRecycler = AssetRecycler.Instance;
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
            pm.Damage = damage;
            pm.ManageRotation();
        }
    }

    public void Upgrade()
    {
        level += 1;
        base.UpgradeDamage(25);
    }

    public string GetUpgradeText()
    {
        return UPGRADE_BASE_WEAPON;
    }

    public string GetUpgradeTitle()
    {
        return UPGRADE_TITLE + level.ToString();
    }

    public bool isActivated()
    {
        return true;
    }

    public override void UpgradeArea(int percent)
    {
        List<GameObject> projectiles = assetRecycler.PlayerProjectilePool;
        foreach (GameObject p in projectiles)
        {
            Vector2 current = p.transform.localScale;
            Vector2 newScale = current + (current * percent / 100);
            p.transform.localScale = newScale;
        }
    }

    public Image GetIcon()
    {
        throw new NotImplementedException();
    }
}
