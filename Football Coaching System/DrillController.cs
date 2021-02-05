using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillController : MonoBehaviour
{
    [Header("Drill Buttons")]
    public GameObject drillButton1;
    public GameObject drillButton2;
    public GameObject drillButton3;
    public GameObject drillButton4;

    [Header("Drill Containers")]
    public GameObject passingDrill;
    public GameObject dribblingDrill;
    public GameObject shootingDrill;
    public GameObject crossingDrill;

    [Header("Dribbling Drill Players")]
    public AIController dribblingPlayer;
    public Vector3 DribblingStartingPos;


    [Header("Shooting Drill Players")]
    public AIController shootingPlayer1;
    public AIController shootingPlayer2;
    public AIController shootingPlayer3;

    public Vector3 shootingP1StartingPos;
    public Vector3 shootingP2StartingPos;
    public Vector3 shootingP3StartingPos;

    [Header("CrossingDrill Players")]
    public AIController crossingPlayer;
    public AIController scoringPlayer;
    public Vector3 crossingStartingPos;
    public Vector3 headingStartingPos;

    private AudioSource audio;
    
    // Start is called before the first frame update
    void Start()
    {
        //passingDrill = GameObject.FindGameObjectWithTag("PassingDrill");
        //dribblingDrill = GameObject.FindGameObjectWithTag("DribblingDrill");
        //shootingDrill = GameObject.FindGameObjectWithTag("ShootingDrill");
        //crossingDrill = GameObject.FindGameObjectWithTag("CrossingDrill");

        passingDrill.SetActive(false);
        dribblingDrill.SetActive(false);
        shootingDrill.SetActive(false);
        crossingDrill.SetActive(false);
        audio = GetComponent<AudioSource>();
    }

     private void Update() 
    {
        if(OVRInput.GetDown(OVRInput.Button.One))
        {
            PlayPassingDrill();
            audio.Play();
        }
        else if(OVRInput.GetDown(OVRInput.Button.Two))
        {
            PlayDibblingDrill();
            audio.Play();
        }
        else if(OVRInput.GetDown(OVRInput.Button.Three))
        {
            PlayShootingDrill();
            audio.Play();
        }
        else if(OVRInput.GetDown(OVRInput.Button.Four))
        {
            PlayCrossingDrill();
            audio.Play();
        }
    }

    public void PlayPassingDrill()
    {
        if(passingDrill.activeSelf == false)
        {
            passingDrill.SetActive(true);
            dribblingDrill.SetActive(false);
            shootingDrill.SetActive(false);
            crossingDrill.SetActive(false);
        }
    }

    public void PlayDibblingDrill()
    {
        dribblingPlayer.gameObject.transform.position = DribblingStartingPos;
        dribblingPlayer.destPoint = 0;
        if (dribblingDrill.activeSelf == false)
        {
            passingDrill.SetActive(false);
            dribblingDrill.SetActive(true);
            shootingDrill.SetActive(false);
            crossingDrill.SetActive(false);
        }
    }

    public void PlayShootingDrill()
    {
        shootingPlayer1.destPoint = 0;
        shootingPlayer2.destPoint = 0;
        shootingPlayer3.destPoint = 0;

        shootingPlayer1.gameObject.transform.position = shootingP1StartingPos;
        shootingPlayer2.gameObject.transform.position = shootingP2StartingPos;
        shootingPlayer3.gameObject.transform.position = shootingP3StartingPos;

        if (shootingDrill.activeSelf == false)
        {
            passingDrill.SetActive(false);
            dribblingDrill.SetActive(false);
            shootingDrill.SetActive(true);
            crossingDrill.SetActive(false);
        }
    }

    public void PlayCrossingDrill()
    {
        crossingPlayer.destPoint = 0;
        scoringPlayer.destPoint = 0;
        crossingPlayer.gameObject.transform.position = crossingStartingPos;
        scoringPlayer.gameObject.transform.position = headingStartingPos;

        if (crossingDrill.activeSelf == false)
        {
            passingDrill.SetActive(false);
            dribblingDrill.SetActive(false);
            shootingDrill.SetActive(false);
            crossingDrill.SetActive(true);
        }
    }
}
