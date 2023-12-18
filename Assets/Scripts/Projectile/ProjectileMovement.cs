using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 target;

    public Vector2 Target { get => target; set => target = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * 3f * Time.deltaTime;
    }
}
