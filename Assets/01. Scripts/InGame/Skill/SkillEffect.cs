
using System.Collections;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{

    ParticleSystem Particle;

    private void Awake()
    {
        Particle = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        Particle.Stop();
    }


    public void Play()
    {
        StartCoroutine(StartAndDestroy());
    }

    public IEnumerator StartAndDestroy()
    {
        yield return null;
        Particle.Play();
        Debug.Log($"파티클 타임: {Particle.main.duration + Particle.main.startLifetime.constantMax}");
        yield return new WaitForSeconds(Particle.main.duration+ Particle.main.startLifetime.constantMax);
        Destroy(Particle.gameObject);
    }

    public void Pause()
    {
        Particle.Stop();
    }
}
