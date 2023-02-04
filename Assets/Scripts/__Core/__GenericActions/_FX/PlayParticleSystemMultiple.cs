using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleSystemMultiple : MonoBehaviour
{
    public ParticleSystem myParticleSystem1;
    public ParticleSystem myParticleSystem2;
    public ParticleSystem myParticleSystem3;
    public ParticleSystem myParticleSystem4;
    public ParticleSystem myParticleSystem5;

    public void PlayParticleSystem1() => myParticleSystem1.Play();
    public void PlayParticleSystem2() => myParticleSystem2.Play();
    public void PlayParticleSystem3() => myParticleSystem3.Play();
    public void PlayParticleSystem4() => myParticleSystem4.Play();
    public void PlayParticleSystem5() => myParticleSystem5.Play();
}
