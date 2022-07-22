using System.Collections;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private float xPose;
    private float dir;
    private Vector3 startPosition;

    private float maxDistance = 6f;
    private float currentTime;
    private float maxTime = 2f;
    private IEnumerator coroutine;


    void Start()
    {
        currentTime = 0;
        GameEvents.onPausePressed.AddListener(StopMoving);
    }

	public void Moving(float xNew)
    {
        xPose = transform.position.x;
        startPosition = transform.position;
        if (xPose < xNew)
        {
            dir = 1f;
        }
        else
        {
            dir = -1f;
        }
        StopMoving();
        coroutine = MovingToPosition(startPosition, xNew);
        StartCoroutine(coroutine);
    }

    private IEnumerator MovingToPosition(Vector3 start, float xGoal)
    {
        bool isFinished = false;
        if (Mathf.Abs(xGoal) > maxDistance)
        {
            xGoal = maxDistance * dir;
            Debug.Log("xGoal - " + xGoal);
        }
        Debug.Log("xPose - " + xPose);
        currentTime = 0;
        do
        {
            if (dir > 0)
            {
                if (xPose > xGoal)
                {
                    isFinished = true;
                    break;
                }
            }
            else
            {
                if (xPose < xGoal)
                {
                    isFinished = true;
                    break;
                }
            }
            yield return null; 
            transform.position = Vector3.Lerp(start, new Vector3(xGoal, 1, 0), currentTime / maxTime);
            xPose += Time.deltaTime * dir;
            currentTime += Time.deltaTime; 
        } while (!isFinished);
    }
    public void StopMoving()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}
