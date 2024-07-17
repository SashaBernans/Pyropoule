using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCollisions : MonoBehaviour
{
    [SerializeField] private int damage;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        if(collision.gameObject.tag == "Player")
        {
            
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(this.damage);
            gameObject.SetActive(false);
        }
    }
}
