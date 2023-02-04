using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlayParticleSystem : GenericActionCaller
{
    public ParticleSystem myParticleSystem;

    public override void MethodToCall()
    {
        base.MethodToCall();
        PlayParticleSystem();
    }

    public void PlayParticleSystem() => myParticleSystem.Play();

}
