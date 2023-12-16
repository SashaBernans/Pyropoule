using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyroPouleTroupeauManager : MonoBehaviour
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
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance > 10f)
        {
            speed = 5f;
        }
        if (distance < 10f)
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
                Destroy(longPlatform);
            }
            collision.gameObject.SetActive(false);
        }
    }
}
