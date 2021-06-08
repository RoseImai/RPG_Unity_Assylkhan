using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;
using UnityEngine.Serialization;


public class EnemyManager : MonoBehaviour
{
    EnemyLocalMotionManager enemyLocalMotionManager;
    EnemyAnimatorManager enemyAnimationManager;
    EnemyStats enemyStats;
    public NavMeshAgent navMeshAgent;    
    public Rigidbody enemyRigitBody;

    public State currentState;
    public CharacterStats currentTarget;

    public bool isPerformingAction;
    public bool isInteracting;
    public float rotationSpeed = 15;
    public float maximumAttackRange = 1.5f;


    [Header("A.I. Settings")]
    public float detectionRadius = 20f;
        
    public float maximumDetectionAngle = 50f;
    public float minimumDetectionAngle = -50f;

    public float currentRecoveryTime = 0;

    private void Awake()
    {
        enemyLocalMotionManager = GetComponent<EnemyLocalMotionManager>();
        enemyAnimationManager = GetComponentInChildren<EnemyAnimatorManager>();
        enemyStats = GetComponent<EnemyStats>();
        enemyRigitBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.enabled = false;
    }
    
    private void Start()
    {
        enemyRigitBody.isKinematic = false;
    }

    private void Update()
    {
        HandleRecoveryTimer();
        isInteracting = enemyAnimationManager.anim.GetBool("isInteracting");
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void HandleStateMachine()
    {
        if (currentState != null)
        {
            State nextState = currentState.Tick(this, enemyStats, enemyAnimationManager);

            if (nextState!= null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(State state)
    {
        currentState = state;
    }

    private void HandleRecoveryTimer()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (isInteracting)
        {
            if (currentRecoveryTime <= 0)
            {
                isInteracting = false;
            }
        }
    } 
}