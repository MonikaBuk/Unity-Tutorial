using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip shoutingClip;
    public float speedDampTime = 0.01f;
    public float sensitivityX = 1.0f;
    public float animationSpeed = 1.5f;

    private Animator anim;
    private HashIDs hash;
  

    void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        anim.SetLayerWeight(1, 1f);

    }
    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");
        float turn = Input.GetAxis("Turn");
        Rotating(turn);
        MovementManagment(v, sneak);

    }
    private void Update()
    {
        bool shout = Input.GetButtonDown("Attract");
        anim.SetBool(hash.shoutingBool, shout);
        AudioManagment(shout);
    }

    void Rotating(float mouseXInput)
    {
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        if(mouseXInput !=0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, mouseXInput * sensitivityX, 0f);
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }

    }

    void MovementManagment(float vertical, bool sneaking)
    {
        anim.SetBool(hash.sneakingBool, sneaking);
        if(vertical>0)
        {
            anim.SetFloat(hash.speedFloat, animationSpeed, speedDampTime, Time.deltaTime);
        }
        else 
        {
            anim.SetFloat(hash.speedFloat,0);
        }
    }

    void AudioManagment (bool shout)
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            if(!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().pitch = 0.27f;
                GetComponent<AudioSource>().Play(); 
            }
        }
        else 
        {
            GetComponent<AudioSource>().Stop();
        }
        if(shout)
        {
            AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
        }

    }


}
