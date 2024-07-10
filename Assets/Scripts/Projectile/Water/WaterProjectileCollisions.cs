using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProjectileCollisions : MonoBehaviour
{
    [SerializeField] private GameObject particalSystemPrefab;
    [SerializeField] private int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isActiveAndEnabled)
        {
            StartCoroutine(Deactivate());
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject newParticalSystem = Instantiate(particalSystemPrefab, transform.position, Quaternion.identity);
        newParticalSystem.transform.position = transform.position;
        gameObject.SetActive(false);

        if (collision.gameObject.tag == "Pyropoule")
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            damageable.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pyropoule")
        {
            GameObject newParticalSystem = Instantiate(particalSystemPrefab, transform.position, Quaternion.identity);
            newParticalSystem.transform.position = transform.position;
            gameObject.SetActive(false);
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            damageable.TakeDamage(damage);
        }
    }
}
