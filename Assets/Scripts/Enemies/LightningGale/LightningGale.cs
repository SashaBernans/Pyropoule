using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningGale : Enemy, IScaleable, IDamageable
{
    [SerializeField] private float baseHealth;
    public override float enemyBaseHealth => baseHealth;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 5f;

    private Vector3 startPos;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBackAndForth();
    }

    private void MoveBackAndForth()
    {
        // Moves the object back and forth along the x-axis
        float xPos = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        if(startPos.x + xPos > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        transform.position = new Vector3(startPos.x + xPos, transform.position.y, transform.position.z);
    }
}
