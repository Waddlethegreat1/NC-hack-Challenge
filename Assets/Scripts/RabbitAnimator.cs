using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitAnimator : MonoBehaviour
{
    [Header("Public Input Parameters (I.E. things we slot in)")]
    public CharacterController froggy;
    public float betterGroundDistance;
    public LayerMask betterGroundLayerMask;
    public GameObject betterGroundChecker;
    public bool betterIsGrounded;



    [Header("Velocity Controls")]
    [Tooltip("This parameter is the velocity vector that the frog will approach")]
    public Vector3 targetVelocity;
    [Tooltip("This parameter is the velocity vector that the frog will use to move")]
    public Vector3 netVelocity;
    [Tooltip("")]
    public float gravModifier;
    [Tooltip("This parameter is the forward vector that the frog will use to do something")]
    public Vector3 forwardVector;
    [Tooltip("This Affects how fast the frog moves forward")]
    public float forwardSpeed;


    [Header("Animation Variables")]
    public Animator animationIguana;
    public float currentSpeed;
    public bool startDelay;
    public float betterDelayTimer = 0;

    [Header("Randomization Variables")]
    public int randomNumber;
    public float movementTimer = 0;

    [Header("Turning Code")]
    public float targetDegrees;
    public float degreesRightNow;
    public Quaternion RotationVector;


    [Header("NavMesh Variables")]
    public NavMeshAgent IguanaAgent;
    public Vector3 LocationToGoTo;
    public GameObject playerCamera;
    public LayerMask layermaskCool;
    public float DistanceBetween;
    public float rangeOfMovement;
    public float maxDistance;

    private void Start()
    {
        FindLocation();
        animationIguana = GetComponent<Animator>();
    }
    private void Update()
    {
        if (movementTimer == 0)
        {
            RandomGenerator();
        }

        if (randomNumber == 1 || randomNumber == 2)
        {
            AnimationWalk();
            SetWhereToGo();
        }
        if (randomNumber == 3)
        {
            movementTimer++;
            if (movementTimer >= 600)
            {
                RandomGenerator();
                movementTimer = 0;
            }
        }
        CloseToDestination();
    }
    private void SetWhereToGo()
    {
        IguanaAgent.SetDestination(LocationToGoTo);
    }
    private void AnimationWalk()
    {
        currentSpeed = IguanaAgent.velocity.magnitude;
        animationIguana.SetFloat("Forward", currentSpeed);
    }
    private void CloseToDestination()
    {
        DistanceBetween = Vector3.Distance(LocationToGoTo, transform.position);
        if (DistanceBetween < 1)
        {
            FindLocation();
        }
    }
    private void FindLocation()
    {
        NavMeshHit hit;

        Vector3 randomLocation = transform.position + Random.insideUnitSphere * rangeOfMovement;

        if (NavMesh.SamplePosition(randomLocation, out hit, maxDistance, NavMesh.AllAreas))
        {
            LocationToGoTo = hit.position;
        }
    }
    private void RandomGenerator()
    {
        randomNumber = Random.Range(1, 3);
    }
}
