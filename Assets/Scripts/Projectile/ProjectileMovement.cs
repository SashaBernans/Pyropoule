using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 direction;
    private Vector2 target;
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private SpriteRenderer sr;

    /*public Vector2 Direction
    {
        get => direction; set
        {
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            this.direction = (value - position).normalized;
        }
    }*/

    public Vector2 Target 
    {
        get => target; set
        {
            target = value;
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            this.direction = (value - position).normalized;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        cc = this.GetComponent<CapsuleCollider2D>();
        sr = this.GetComponent<SpriteRenderer>();
        //transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        rb.velocity = direction * speed;
    }

    private void OnDisable()
    {
        transform.rotation = new Quaternion(0,0,0,0);
    }

    public void ManageRotation()
    {
        if (cc != null)
        {
            //print(cc.transform.position);
            //print(cc.size.x);
            //print(cc.size.y);

            float minX = transform.position.x - cc.size.x /2;
            float minY = transform.position.y - cc.size.y / 2;
            float maxX = transform.position.x + cc.size.x/2;
            float maxY = transform.position.y + cc.size.y/2;

            Vector2 tailPosition = new Vector2(transform.position.x, maxY);
            Vector2 headPosition = new Vector2(transform.position.x, minY);

            float distanceTailTarget = Vector2.Distance(tailPosition, target);
            float distanceTailHead = Vector2.Distance(tailPosition, headPosition);
            float distanceHeadTarget = Vector2.Distance(headPosition, target);

            double radRotation = System.Math.Acos(
                (distanceTailTarget * distanceTailTarget + distanceTailHead * distanceTailHead - distanceHeadTarget * distanceHeadTarget) /
                (2 * distanceHeadTarget * distanceTailHead));

            float rotation = (float)((180 / System.Math.PI) * radRotation);
            //print(rotation);
            if (rotation >1 | rotation <-1)
            {
                if (target.x > transform.position.x)
                {
                    sr.flipX = false;
                    transform.Rotate(0, 0,rotation);
                }
                else
                {
                    sr.flipX = true;
                    transform.Rotate(0, 0, 360 - rotation);
                }
            }
        }
    }
}
