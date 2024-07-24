using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCollisions : MonoBehaviour
{
    private Orbs parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Orbs>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pyropoule")
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(parent.Damage);
        }
    }
}
