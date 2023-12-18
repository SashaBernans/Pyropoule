using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContacts : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Flame")
        {
            gameManager.LooseLife();
            Debug.Log("ouch");
        }
    }
}
