using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpeed : MonoBehaviour
{
    private PlayerMovement player;
    public float effectDuration;
    public float boostSpeed;
    private bool hasEffect;

    void Start()
    {
        player = GameObject.Find("JetPlayer").GetComponent<PlayerMovement>();
        hasEffect = false;
    }

    
    void Update()
    {
        if(!hasEffect && player.isAlive && !player.rb.isKinematic)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                hasEffect = true;

                StartCoroutine(GiveSpeedEffect());
            }
        }
    }

    private IEnumerator GiveSpeedEffect()
    {
        while(player.moveSpeed != boostSpeed)
        {
            //player.moveSpeed = Mathf.Lerp(prevSpeed, boostSpeed, 0.01f);
            player.moveSpeed = boostSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(effectDuration);
        player.moveSpeed = player.maxSpeed;
        hasEffect = false; 
    }
}
