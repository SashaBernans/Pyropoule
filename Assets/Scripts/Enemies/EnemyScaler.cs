using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScaler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private IScaleable scaleable; 

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        scaleable = GetComponent<IScaleable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (gameManager != null)
        {
            HandleScaling();
        }
    }

    public void HandleScaling()
    {
        if (gameManager.height > gameManager.globalScaler)
        {
            float multiplier = gameManager.height / gameManager.globalScaler;
            scaleable.multiplyMaxHealth(multiplier);
        }

        scaleable.healToMaxHealth();
    }
}
