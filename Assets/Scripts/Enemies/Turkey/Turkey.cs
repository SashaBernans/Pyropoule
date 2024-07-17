using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turkey : Enemy, IDamageable, IScaleable
{
    //[SerializeField] private AssetRecycler assetRecycler;
    [SerializeField] private float baseHealth;

    private GameObject laser;

    public override float enemyBaseHealth => baseHealth;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //assetRecycler = AssetRecycler.Instance;
        laser = base.assetRecycler.LaserPool.Find(p => !p.activeInHierarchy);
        laser.transform.position = transform.position;
        laser.SetActive(true);
    }
    private void OnEnable()
    {
        if (laser!=null)
        {
            laser.transform.position = transform.position;
            laser.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Disable laser when turkey dies
    private void OnDisable()
    {
        if (laser != null)
        {
            laser.SetActive(false);
        }
    }
}
