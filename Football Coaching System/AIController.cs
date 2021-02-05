using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public float aiMoveSpeed;
    public float damping = 6.0f;
    public Transform[] navPoint;
    public NavMeshAgent agent;
    public int destPoint = 0;
    public GameObject feetCollider; 
    private Animator animator;
    public GameObject ball;
    public GameObject placeholderBall;
    public GameObject goals;
    public GameObject drill1;
    public GameObject drill2;
    public GameObject drill3;
    public GameObject drill4;
    public GameObject CrossingBall;
    public Animator crossingTarget;
    private AudioSource audio;

    private void Awake() 
    {
        audio = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if(this.transform.parent == drill4.transform)
        {
          CrossingBall.SetActive(false);  
        }
        
        if(navPoint.Length > 0)
        {
            agent.destination = navPoint[destPoint].position;
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning",false);
        }

        agent.speed = aiMoveSpeed;
        agent.autoBraking = false;
    }

    public void PlaySound()
    {
        audio.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
            //agent.speed = 0;
            //agent.isStopped = true;
        }
    }

    void GoToNextPoint()
    {
        if (navPoint.Length == 0)
        {
            return;
        }

        if (destPoint == (navPoint.Length ))
        {
            agent.speed = 0;
            agent.isStopped = true;
            if(this.gameObject.tag =="Dribbling")
            {
                animator.SetBool("isRunning",false);
            }
            else if(this.gameObject.tag=="Shooting")
            {
                Shooting();
            }
            else if(this.transform.parent == drill4.transform)
            {
                print("TEST");
                Crossing();
            }

        }
        else
        {
            agent.destination = navPoint[destPoint].position;
            destPoint = (destPoint + 1);
        }

    }
    public void Dribbling()
    {
        animator.SetBool("isRunning",false);
    }
    public void Crossing()
    {
        animator.SetBool("isCrossing", true);
    }

    public void PlayCrossAnim()
    {
        ball.SetActive(false);
        CrossingBall.SetActive(true);
        CrossingBall.GetComponent<Animation>().Play();
    }
    public void PlayJumpAnim()
    {
        if(crossingTarget.GetBool("isJumping") == true)
        {
            crossingTarget.SetBool("isJumping",false);
        }
        else if(crossingTarget.GetBool("isJumping") == false)
        {
            crossingTarget.SetBool("isJumping",true);
        }
    }
    public void ResetJump()
    {
        animator.SetBool("isJumping",false);
    }
    public void Shooting()
    {
            animator.SetBool("isShooting",true);
    }
    public void ResetAnim()
    {
        ball.SetActive(false);
        placeholderBall.SetActive(true);
        animator.SetBool("isRunning", false);
         if(this.gameObject.tag =="Shooting")
        {
            animator.SetBool("isShooting", false);

        }
        else if(this.gameObject.tag == "Crossing")
        {
            animator.SetBool("isCrossing",false);
        }
    }
}
