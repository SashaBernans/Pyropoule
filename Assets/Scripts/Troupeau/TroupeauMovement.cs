using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroupeauManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float speed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float distance = Vector3.Distance(this.transform.position, player.transform.position);
        float distance = player.transform.position.y - this.transform.position.y;
        if (distance > 7f)
        {
            speed = 5f;
        }
        if (distance < 7f)
        {
            speed = 1f;
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HardBlock" | collision.tag == "Platform")
        {
            if (collision.gameObject.transform.parent != null)
            {
                GameObject longPlatform = collision.gameObject.transform.parent.gameObject;
                collision.gameObject.transform.parent.DetachChildren();
                longPlatform.transform.DetachChildren();
                longPlatform.SetActive(false);
            }
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "Pyropoule")
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "Player")
        {
            GameManager.Instance.GameOver();
        }
    }
}
