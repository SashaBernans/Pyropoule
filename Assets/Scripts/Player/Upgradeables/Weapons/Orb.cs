using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : Weapon, IUpgradeable
{
    private SpriteRenderer spriteRenderer;
    private PlayerUpgradesManager playerUpgradesManager;

    [SerializeField] private float speed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int damage;

    private Vector3 playerPosition;
    private int level = 1;

    private string upgradeText = "Upgrade orb speed by 10%";
    private string upgradeTite = "Orb level ";

    public override float ProjectileSpeed { get => speed; set => speed = value; }
    public override float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public override int Damage { get => damage; set => damage = value; }

    public string GetUpgradeText()
    {
        return upgradeText;
    }

    public void Upgrade()
    {
        print("ORB UPGRADEDDDDDDDD");
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //playerUpgradesManager = GetComponentInParent<PlayerUpgradesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the rotation for this frame
        float rotationThisFrame = speed * Time.deltaTime;

        //Calculate the pivot point
        //Vector3 pivotPoint = spriteRenderer.bounds.max;

        // Apply the rotation around the Z axis
        transform.RotateAround(transform.parent.transform.position, Vector3.forward, rotationThisFrame);
    }

    public string GetUpgradeTitle()
    {
        return upgradeTite + level.ToString();
    }
    public bool isActivated()
    {
        return gameObject.activeSelf;
    }
}
