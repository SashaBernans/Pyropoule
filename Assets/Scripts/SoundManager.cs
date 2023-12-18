using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;

    [SerializeField] private AudioClip playerJump;
    [SerializeField] private AudioClip playerKilled;
    [SerializeField] private AudioClip shootFlame;
    [SerializeField] private AudioClip chickenHurt;

    public static SoundManager Instance { get { return instance; } }
    public AudioClip PlayerJump { get { return playerJump; } }
    public AudioClip PlayerKilled { get { return playerKilled; } }

    public AudioClip ShootFlame { get => shootFlame; set => shootFlame = value; }
    public AudioClip ChickenHurt { get => chickenHurt; set => chickenHurt = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }
}
