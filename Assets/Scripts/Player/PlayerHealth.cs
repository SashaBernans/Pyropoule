using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool takeDamage(float damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            return true;
        }
        return false;
    }
}
