using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    private ParticleSystem destructionInner;
    private ParticleSystem destructionOuter;
    private PlayerMovement player;

    void Start()
    {
        destructionInner = GameObject.Find("DestructionInner").GetComponent<ParticleSystem>();
        destructionOuter = GameObject.Find("DestructionOuter").GetComponent<ParticleSystem>();
        player = GameObject.Find("JetPlayer").GetComponent<PlayerMovement>();
        destructionInner.Stop();
        destructionOuter.Stop();
    }

    
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            StartCoroutine(Destruct());
        }
    }
    
    private IEnumerator Destruct()
    {
        player.isAlive = false;
        destructionInner.Play();
        destructionOuter.Play();
        yield return new WaitForSeconds(3);
        player.isAlive = true;
        player.stillDead = false;
    }
}