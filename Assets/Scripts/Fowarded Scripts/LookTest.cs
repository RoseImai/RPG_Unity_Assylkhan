using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LookTest : MonoBehaviour
{
    [Range(0, 360)] public float angleView = 90;
    public Transform Target;
    public Transform EnemyEye;
    public float dist;
    NavMeshAgent nav;
    public float detect = 3f;
    public float rangeDetect = 10f;
    public float HP_Skeleton = 100;
    private GameObject point;
    public string tag_point;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        dist = Vector3.Distance(Target.transform.position, transform.position);
        
        if ((dist <= detect || IsInView()) && dist>1f)
        {
            nav.enabled = true;
            nav.SetDestination(Target.transform.position);
            gameObject.GetComponent<Animator>().ResetTrigger("Walk");
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }
        else
        {
            point = GameObject.FindGameObjectWithTag(tag_point);
            nav.enabled = true;
            gameObject.GetComponent<Animator>().SetTrigger("Walk");
            nav.SetDestination(point.transform.position);
        }

        if (dist < 1f)
        {
            nav.SetDestination(Target.transform.position);
            gameObject.GetComponent<Animator>().ResetTrigger("Walk");
            gameObject.GetComponent<Animator>().ResetTrigger("Run");
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }
        DrawViewState();
    }
    
    private bool IsInView() 
    {
        float realAngle = Vector3.Angle(EnemyEye.forward, Target.position - EnemyEye.position);
        RaycastHit hit;
        if (Physics.Raycast(EnemyEye.transform.position, Target.transform.position - EnemyEye.transform.position, out hit, rangeDetect))
        {
            if (realAngle < angleView / 2f && Vector3.Distance(EnemyEye.position, Target.position) <= rangeDetect && hit.transform == Target.transform)
            {
                return true;
            }
        }
        return false;
    }
    
    private void DrawViewState() 
    {       
        Vector3 left = EnemyEye.position + Quaternion.Euler(new Vector3(0, angleView / 2f, 0)) * (EnemyEye.forward * angleView);
        Vector3 right = EnemyEye.position + Quaternion.Euler(-new Vector3(0, angleView / 2f, 0)) * (EnemyEye.forward * angleView);     
        Debug.DrawLine(EnemyEye.position, left, Color.yellow);
        Debug.DrawLine(EnemyEye.position, right, Color.yellow);       
    }
}
