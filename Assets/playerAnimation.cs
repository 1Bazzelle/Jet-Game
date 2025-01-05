using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    private PlayerMovement player;
    private ParticleSystem fireParticles;
    private ParticleSystem wingParticlesR;
    private ParticleSystem wingParticlesL;

    void Start()
    {
        player = GameObject.Find("JetPlayer").GetComponent<PlayerMovement>();
        fireParticles = GameObject.Find("Fire Particles").GetComponent<ParticleSystem>();
        wingParticlesR = GameObject.Find("Wing Particles Right").GetComponent<ParticleSystem>();
        wingParticlesL = GameObject.Find("Wing Particles Left").GetComponent<ParticleSystem>();
        fireParticles.Stop();
        wingParticlesR.Stop();
        wingParticlesL.Stop();
    }


    void Update()
    {
        if(player.isAlive)
        {
            if(!fireParticles.isPlaying && player.moveSpeed >= 100 )
            {
                fireParticles.Play();
                wingParticlesR.Play();
                wingParticlesL.Play();
            }

            if(fireParticles.isPlaying)
            {
                if(player.accelerate && player.moveSpeed < player.maxSpeed)
                {
                    var emission = fireParticles.emission;
                    var rate = emission.rateOverTime;
                    rate = Mathf.Lerp(150f, 300f, 0.5f);
                    emission.rateOverTime = rate;
                }
                else if(player.decelerate)
                {     
                    var emission = fireParticles.emission;
                    var rate = emission.rateOverTime;
                    rate = Mathf.Lerp(150f, 50f, 0.5f);
                    emission.rateOverTime = rate;
                }
                else
                {
                    var emission = fireParticles.emission;
                    var rate = emission.rateOverTime;
                    rate = 150f;
                    emission.rateOverTime = rate;
                }

            }
        }
        if(!player.isAlive)
        {
            fireParticles.Stop();
            wingParticlesR.Stop();
            wingParticlesL.Stop();
        }
    }
}
