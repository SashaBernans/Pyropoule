using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroupeauManager : MonoBehaviour
{
    private const float MIN_DISTANCE_FROM_PLAYER = 15f;
    [SerializeField] private GameObject player;
    [SerializeField] private float baseSpeed;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = player.transform.position.y - this.transform.position.y;
        if (distance > MIN_DISTANCE_FROM_PLAYER)
        {
            speed = 5f;
        }
        if (distance < 7f)
        {
            speed = baseSpeed;
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
        else if (collision.tag == "Player")
        {
            GameManager.Instance.GameOver();
        }
        else if (collision.tag == "Exp")
        {
            print("EXPCOLLISION");
        }
        else
        {
            collision.gameObject.SetActive(false);
        }
        /*
        else if (collision.tag == "Pyropoule")
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "Bucket")
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "Laser")
        {
            collision.gameObject.SetActive(false);
        }*/
    }
}
