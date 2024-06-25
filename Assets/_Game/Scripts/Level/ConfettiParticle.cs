using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        particle.Pause();
    }

    private void OnEnable()
    {
        Player.winGameEvent += OnPlayParticle;
    }

    private void OnDisable()
    {
        Player.winGameEvent += OnPlayParticle;
    }

    public void OnPlayParticle()
    {
        if (particle != null)
        {
            particle.Play();
        }
    }
}
