using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyroPouleTroupeauManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * 1 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HardBlock" | collision.tag == "Platform")
        {
            collision.gameObject.SetActive(false);
            Debug.Log("HardBLock in trigger");
        }
    }
}
