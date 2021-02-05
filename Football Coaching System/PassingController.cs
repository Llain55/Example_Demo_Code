using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingController : MonoBehaviour
{
    public Transform[] passPoints;
    private Ball ball;
    private int destPoint = 0;
    private bool isPassing = false;
    private bool canPass;
    private Vector3 startPos;
    private Vector3 endPos;
    private float timeElapsed = 0;
    private float lerpDuration = 1.5f;
    private float percent = 0;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        startPos =passPoints[destPoint].position;
        GoToNextPoint();

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPassing)
        {
            Vector3 targetDirection = endPos - ball.transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, endPos, 100 * Time.deltaTime,0.0f);
            Debug.DrawRay(ball.transform.position, newDirection, Color.red);

            ball.transform.Rotate(360 * Time.deltaTime, 0, 0);
            percent = timeElapsed / lerpDuration;
            ball.transform.position = Vector3.Lerp(startPos,endPos,percent);
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= lerpDuration)
            {
                timeElapsed = 0;
                startPos = endPos;
                GoToNextPoint();
            }
        }
    }

    void GoToNextPoint()
    {
        audio.Play();
        if (passPoints.Length == 0)
        {
            return;
        }
        isPassing = true;
        destPoint = (destPoint + 1) % passPoints.Length;
        endPos = passPoints[destPoint].position;
        ball.transform.LookAt(endPos);
    }
}
