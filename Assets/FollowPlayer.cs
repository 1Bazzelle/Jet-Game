using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private new Camera camera;
    private Transform player;
    private PlayerMovement playerMov;
    private Rigidbody rigidPlayer;
    public float moveSpeed;
    public Vector3 offset;
    public float slerpyderp;
    public float rotationAmp;
    private bool stalling;
    private float prevFOV;
    void Start()
    {
        player = GameObject.Find("JetPlayer").GetComponent<Transform>();
        playerMov = GameObject.Find("JetPlayer").GetComponent<PlayerMovement>();
        rigidPlayer = GameObject.Find("JetPlayer").GetComponent<Rigidbody>();
        camera = Camera.main;
        moveSpeed = 50;
        offset = new Vector3(0f, 5f, -12f);
    }


    void Update()
    {
        if (playerMov.isAlive && !playerMov.isStalling && !Input.GetKey(KeyCode.Space))
        {
            if(stalling)
            {
                stalling = false;
            }
            camera.fieldOfView = 45f + Mathf.Sqrt(rigidPlayer.velocity.x * rigidPlayer.velocity.x + rigidPlayer.velocity.y * rigidPlayer.velocity.y + rigidPlayer.velocity.z * rigidPlayer.velocity.z)/Mathf.Lerp(3f,1f,0.5f);
            Vector3 worldOffset = player.transform.TransformPoint(offset);
            
            transform.position = Vector3.Slerp(transform.position, worldOffset, slerpyderp * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation * Quaternion.Euler(5f, 0f, 0f), rotationAmp * Time.deltaTime);

        }
        else if(playerMov.isAlive && playerMov.isStalling && !Input.GetKey(KeyCode.Space))
        {   
            if(!stalling)
            {
                stalling= true;
                prevFOV = camera.fieldOfView;
            }
            Vector3 worldOffset = player.transform.TransformPoint(offset);
            transform.position = Vector3.Slerp(transform.position, worldOffset, slerpyderp * Time.deltaTime);
            // TODO figure out a smooth way of transitioning between non-stalling rotation and stalling-rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation * Quaternion.Euler(5f, 0f, 0f), rotationAmp * Time.deltaTime);
            camera.fieldOfView = Mathf.Lerp(prevFOV, 75f, 0.2f * Time.deltaTime);
        }
        else if(!playerMov.isAlive)
        {
            float i = camera.fieldOfView;
            camera.fieldOfView = Mathf.Lerp(i, 90f, 0.01f);
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            //Cinematic Camera
            transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        }
    }
}