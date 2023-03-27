using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip shoutingClip;
    public float speedDampTime = 0.01f;
    public float sensitivityX = 1.0f;
    public float animationSpeed = 1.5f;
    public float pitchValue;
    private float elapsedTime = 0;
    private bool noBackMov = true;
    private float desiredDuration = 0.5f;
    public float speed = 5.0f;


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
        elapsedTime += Time.deltaTime;
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        bool sneak = Input.GetButton("Sneak");
        float turn = Input.GetAxis("Turn");
        Rotating(turn);
        MovementManagment(v,h, sneak);

        transform.Translate(Vector3.right * h * Time.deltaTime * speed);
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

        if (mouseXInput != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, mouseXInput * sensitivityX, 0f);
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }

    }

    void MovementManagment(float vertical, float horizontal, bool sneaking)
    {
        Rigidbody ourBody = this.GetComponent<Rigidbody>();
        anim.SetBool(hash.sneakingBool, sneaking);
        if (vertical > 0)
        {
            anim.SetFloat(hash.speedFloat, 1.5f, speedDampTime, Time.deltaTime);
            anim.SetBool("Backwards", false);
            noBackMov = true;
        }
        if (vertical < 0)
        {
            if (noBackMov == true)
            {
                elapsedTime = 0;
                noBackMov = false;
            }
            float percentageComplete = elapsedTime / desiredDuration;
            anim.SetFloat(hash.speedFloat, -1.5f, speedDampTime, Time.deltaTime);
            anim.SetBool("Backwards", true);

           

            float movement = Mathf.Lerp(0f, 0.025f, percentageComplete);
            Vector3 moveBack = new Vector3(0, 0, -0.025f);
            moveBack = ourBody.transform.TransformDirection(moveBack);
            ourBody.transform.position += moveBack;
        }
        if (vertical == 0)
        {
            anim.SetFloat(hash.speedFloat, 0);
            anim.SetBool("Backwards", false);
            noBackMov = true;
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
            GameObject thisAudio = GameObject.Find("One shot audio");

            if (thisAudio.name == "Z2 - Vector2 - Angry - Free - 1")
            {
                thisAudio.GetComponent<AudioSource>().pitch = pitchValue;
            }
        }

    }


}
