using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private Orbs parent;
    private Transform playerTransform;
    private Orb previousOrb;

    public Orb PreviousOrb { get => previousOrb; set => previousOrb = value; }
    public Transform PlayerTransform { get => playerTransform; set => playerTransform = value; }

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Orbs>();
        PlayerTransform = transform.parent.parent.transform;
    }
    private void OnEnable()
    {
        parent = GetComponentInParent<Orbs>();
        /*if(transform.parent != null)
        {
            PlayerTransform = transform.parent.parent.transform;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the rotation for this frame
        float rotationThisFrame = parent.ProjectileSpeed * Time.deltaTime;

        //Calculate the pivot point
        //Vector3 pivotPoint = spriteRenderer.bounds.max;

        // Apply the rotation around the Z axis
        transform.RotateAround(PlayerTransform.position, Vector3.forward, rotationThisFrame);
    }

    /*public void IncreaseDistanceFromPlayer(int percent)
    {

        float currentDistanceFromPlayer = Vector2.Distance(transform.position,PlayerTransform.position);

        float newDistance = currentDistanceFromPlayer + currentDistanceFromPlayer * percent/100;

        float ratio = newDistance / currentDistanceFromPlayer;

        if (ratio < 0)
        {
            ratio = ratio * -1;
        }

        float newX = transform.position.x * ratio;

        float newY = transform.position.y * ratio;

        transform.position = new Vector2(newX, newY);
    }*/

    public void IncreaseDistanceFromPlayer(int percent)
    {
        Vector2 directionFromPlayer = (Vector2)transform.position - (Vector2)PlayerTransform.position;
        float currentDistanceFromPlayer = directionFromPlayer.magnitude;
        float distanceIncrease = currentDistanceFromPlayer * percent / 100f;
        Vector2 newPosition = (Vector2)PlayerTransform.position + directionFromPlayer.normalized * (currentDistanceFromPlayer + distanceIncrease);
        transform.position = newPosition;
    }
}
