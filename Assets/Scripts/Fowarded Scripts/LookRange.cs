using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class LookRange : MonoBehaviour
{
    [Range(0, 360)] public float ViewAngle = 90f;
    public float ViewDistance = 15f;
    public float DetectionDistance = 3f;
    public Transform EnemyEye;
    public Transform Target;

    private NavMeshAgent agent;
    private float rotationSpeed;
    private Transform agentTransform;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        rotationSpeed = agent.angularSpeed;
        agentTransform = agent.transform;
    }
    private void Update()
    {       
        float distanceToPlayer = Vector3.Distance(Target.transform.position, agent.transform.position);
        if (distanceToPlayer <= DetectionDistance || IsInView())
        {
            RotateToTarget();
            MoveToTarget();
        }

        if (distanceToPlayer < 1.5f)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }

        DrawViewState();
    }

    private bool IsInView() 
    {
        float realAngle = Vector3.Angle(EnemyEye.forward, Target.position - EnemyEye.position);
        RaycastHit hit;
        if (Physics.Raycast(EnemyEye.transform.position, Target.position - EnemyEye.position, out hit, ViewDistance))
        {
            if (realAngle < ViewAngle / 2f && Vector3.Distance(EnemyEye.position, Target.position) <= ViewDistance && hit.transform == Target.transform)
            {
                return true;
            }
        }
        return false;
    }
    private void RotateToTarget() 
    {
        Vector3 lookVector = Target.position - agentTransform.position;
        lookVector.y = 0;
        if (lookVector == Vector3.zero) return;
        agentTransform.rotation = Quaternion.RotateTowards
            (
                agentTransform.rotation,
                Quaternion.LookRotation(lookVector , Vector3.up),
                rotationSpeed * Time.deltaTime
            );
        
    }
    private void MoveToTarget() 
    {
        agent.SetDestination(Target.transform.position);
    }
    
    
    private void DrawViewState() 
    {       
        Vector3 left = EnemyEye.position + Quaternion.Euler(new Vector3(0, ViewAngle / 2f, 0)) * (EnemyEye.forward * ViewDistance);
        Vector3 right = EnemyEye.position + Quaternion.Euler(-new Vector3(0, ViewAngle / 2f, 0)) * (EnemyEye.forward * ViewDistance);     
        Debug.DrawLine(EnemyEye.position, left, Color.yellow);
        Debug.DrawLine(EnemyEye.position, right, Color.yellow);       
    }

    
}