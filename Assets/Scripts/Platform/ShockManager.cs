using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockManager : MonoBehaviour
{
    [SerializeField] private int damage;
    private bool isShocking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShockLeft()
    {
        transform.GetChild(3).gameObject.SetActive(true);
    }

    public void ShockRight()
    {
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void ShockUp()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ShockDown()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void ResetShocks()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        ResetShocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && isShocking)
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}
