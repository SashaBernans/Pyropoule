using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketMover : MonoBehaviour
{
    private const float MIN_OFFSET = 0f;
    private const float MAX_OFFSET = 20f;
    private const float BUCKET_SPEED = 25f;

    private float offSet;
    private float direction;
    private Vector3 basePosition;

    private void OnEnable()
    {
        basePosition = transform.position;

        offSet = Random.Range(MIN_OFFSET + 1f, MAX_OFFSET - 1f);

        direction = Random.Range(-1, 1);
        if (direction == 0) direction = 1;
    }

    void Update()
    {
        MoveBucket();
    }

    private void MoveBucket()
    {
        offSet += (Time.deltaTime * direction * BUCKET_SPEED);

        if (offSet > MAX_OFFSET)
        {
            offSet = MAX_OFFSET;
            direction = -1f;
        }
        else if (offSet < MIN_OFFSET)
        {
            offSet = MIN_OFFSET;
            direction = 1f;
        }

        transform.position = basePosition + (Vector3.up * (offSet / 100f));
    }
}
