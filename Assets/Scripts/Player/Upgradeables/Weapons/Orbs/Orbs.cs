using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orbs : Weapon, IUpgradeable
{
    private SpriteRenderer spriteRenderer;
    private PlayerUpgradesManager playerUpgradesManager;

    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private GameObject orbPrefab;
    [SerializeField] private GameObject orbIcon;

    private Orb[] childrenOrbs;
    private int level = 1;
    private Orb lastestAddedOrb;
    private Transform playerTransform;

    private string upgradeText = "Upgrade orb speed, damage, size and distance by 10%";
    private string upgradeTite = "Ponderer level ";

    public override float ProjectileSpeed { get => speed; set => speed = value; }
    public override float AttackSpeed { get => speed; set => speed = value; }
    public override int Damage { get => damage; set => damage = value; }

    public string GetUpgradeText()
    {
        return upgradeText;
    }

    public void Upgrade()
    {
        /*
        UpgradeProjectileSpeed(10);
        UpgradeDamage(10);
        UpgradeArea(10);
        UpgradeDistance(3);
        */
        SpawnOrb();
    }

    private void SpawnOrb()
    {
        GameObject orb = Instantiate(orbPrefab, CalculatePosition(), Quaternion.identity, this.transform);
        childrenOrbs = GetComponentsInChildren<Orb>();
        if (childrenOrbs.Length % 4 == 0)
        {
            UpgradeDistance(10);
        }
        Orb temp = lastestAddedOrb;
        lastestAddedOrb = orb.GetComponent<Orb>();
        lastestAddedOrb.PreviousOrb = temp;
        //orb.SetActive(true);
    }

    private Vector3 CalculatePosition()
    {
        if (childrenOrbs.Length % 2 != 0)
        {
            return CalculateMirrorPosition();
        }
        else
        {
            Vector3 newPosition = lastestAddedOrb.transform.position;
            RotateLastestPairOfOrbs();
            return newPosition;
        }

    }

    private void RotateLastestPairOfOrbs()
    {
        float degreesToRotate = 180/ childrenOrbs.Length;

        lastestAddedOrb.transform.RotateAround(playerTransform.position, Vector3.forward, degreesToRotate);
        lastestAddedOrb.PreviousOrb.transform.RotateAround(playerTransform.position, Vector3.forward, degreesToRotate);
    }

    private Vector3 CalculateMirrorPosition()
    {
        float xOffset = lastestAddedOrb.transform.position.x - playerTransform.position.x;
        float yOffset = lastestAddedOrb.transform.position.y - playerTransform.position.y;

        Vector3 newPosition = new Vector3(playerTransform.position.x - xOffset, playerTransform.position.y - yOffset, playerTransform.position.z);

        return newPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        childrenOrbs = GetComponentsInChildren<Orb>();
        lastestAddedOrb = childrenOrbs[childrenOrbs.Length-1];
        playerTransform = transform.parent.transform;
    }

    public string GetUpgradeTitle()
    {
        return upgradeTite + level.ToString();
    }
    public bool isActivated()
    {
        return gameObject.activeSelf;
    }

    private void UpgradeDistance(int percent)
    {
        foreach(Orb orb in childrenOrbs)
        {
            orb.PlayerTransform = playerTransform;
            orb.IncreaseDistanceFromPlayer(percent);
        }
    }

    public Image GetIcon()
    {
        return null;
    }
}
