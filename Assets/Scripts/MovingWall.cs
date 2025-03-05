using UnityEngine;
using System;
using System.Collections;

public class MovingWall : MonoBehaviour
{

    public static event Action OnWallMidpointReached;
    public static event Action OnWallSlamReached;

    [SerializeField] private float returnSpeed = 2f;
    [SerializeField] private float expandDistance = 0.5f;
    [SerializeField] private float slamSpeed = 10f;
    [SerializeField] private float expandDelay = 0.5f;
    [SerializeField] private float slamDelay = 0.3f;
    private float startX;
    private bool hasBroadcasted = false;
    private Coroutine delayCoroutine;
    private MovementState state = MovementState.MovingToMidpoint;

    private enum MovementState
    {
        MovingToMidpoint,
        WaitingToExpand,
        ExpandingOutward,
        WaitingToSlam,
        SlammingInward,
        Stopped
    }

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        if (state == MovementState.Stopped) return; // If stopped, do nothing

        float currentX = transform.position.x;

        switch (state)
        {
            case MovementState.MovingToMidpoint:
                MoveToMidpoint(currentX);
                break;
            case MovementState.ExpandingOutward:
                ExpandOutward(currentX);
                break;
            case MovementState.SlammingInward:
                SlamInward(currentX);
                break;
        }
    }

    private void MoveToMidpoint(float currentX)
    {
        float distance = Mathf.Abs(currentX);
        float time = distance / Mathf.Abs(startX);
        float speedMultiplier = time > 0.5f ? 1f : Mathf.SmoothStep(0.1f, 1f, time * 2);

        float newX = Mathf.MoveTowards(currentX, 0.5f * Mathf.Sign(startX), returnSpeed * speedMultiplier * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (!hasBroadcasted && Mathf.Abs(newX) <= 0.5f)
        {
            hasBroadcasted = true;
            OnWallMidpointReached?.Invoke();
            ChangeStateWithDelay(MovementState.ExpandingOutward, expandDelay);
        }
    }

    private void ExpandOutward(float currentX)
    {
        float targetX = 0.5f * Mathf.Sign(startX) + expandDistance * Mathf.Sign(startX);
        float newX = Mathf.MoveTowards(currentX, targetX, returnSpeed * 0.5f * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (Mathf.Abs(currentX - targetX) <= 0.05f) // Use a slightly higher threshold
        {
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z); // Snap to exact position
            state = MovementState.WaitingToSlam;
            ChangeStateWithDelay(MovementState.SlammingInward, slamDelay);
        }
    }

    private void SlamInward(float currentX)
    {
        float newX = Mathf.MoveTowards(currentX, 0.1f, slamSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (Mathf.Abs(newX) < 0.1f)
        {
            state = MovementState.Stopped;
            OnWallSlamReached?.Invoke();
        }
    }

    private void ChangeStateWithDelay(MovementState newState, float delay)
    {
        if (delayCoroutine != null)
            StopCoroutine(delayCoroutine);

        delayCoroutine = StartCoroutine(DelayStateChange(newState, delay));
    }

    private IEnumerator DelayStateChange(MovementState newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (state != MovementState.Stopped)
        {
            state = newState;
        }
    }

    public void StopWall()
    {
        state = MovementState.Stopped;
        if (delayCoroutine != null)
        {

            StopCoroutine(delayCoroutine);
            delayCoroutine = null;
        }
    }

}

