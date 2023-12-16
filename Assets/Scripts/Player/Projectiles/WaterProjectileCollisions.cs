using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProjectileCollisions : MonoBehaviour
{
    [SerializeField] private GameObject particalSystemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject newParticalSystem = Instantiate(particalSystemPrefab, transform.position, Quaternion.identity);
        newParticalSystem.transform.position = transform.position;
        gameObject.SetActive(false);
        Debug.Log("collisionDetected");
    }
}
