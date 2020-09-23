using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFireVFX : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem particles;
    public float time;

    public void Play()
    {
        animator.gameObject.SetActive(true);

        particles.Play();
        animator.Play("CannonFire");

        Invoke(nameof(Stop), time);
    }

    public void Stop()
    {
        animator.gameObject.SetActive(false);
        particles.Stop();
    }
}
