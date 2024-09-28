using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningGale : Enemy,IScaleable,IDamageable
{
    [SerializeField] private float baseHealth;
    public override float enemyBaseHealth => baseHealth;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
