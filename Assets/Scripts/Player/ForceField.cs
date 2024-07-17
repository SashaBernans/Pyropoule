using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    private ParticleSystem ps;
    private List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
    private GameManager gameManager;
    private WorldGenerator platformGenerator;
    private SoundManager soundManager;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        gameManager = GameManager.Instance;
        soundManager = SoundManager.Instance;
        platformGenerator = WorldGenerator.Instance;
        ps.trigger.AddCollider(platformGenerator.PlayerForceField);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleTrigger()
    {
        int particlesAlive = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        for (int i =0; i<particlesAlive;i++)
        {
            ParticleSystem.Particle p = particles[i];
            audioSource.PlayOneShot(soundManager.ExpPickUp);
            p.remainingLifetime = 0;
            Debug.Log("We colled");
            particles[i] = p;
        }
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }
}
