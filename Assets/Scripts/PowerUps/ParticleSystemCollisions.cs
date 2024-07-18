using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    private HealthSystem playerExp;
    private ParticleSystem ps;
    private List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
    private WorldGenerator platformGenerator;
    private SoundManager soundManager;
    private AudioSource audioSource;
    [SerializeField] private float expValue;

    // Start is called before the first frame update
    void Start()
    {
        playerExp = HealthSystem.Instance;
        ps = GetComponent<ParticleSystem>();
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
            particles[i] = p;
            playerExp.GainExp(expValue);
        }
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
        ps.Stop();
    }
}
