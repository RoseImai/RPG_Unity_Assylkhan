using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointEnemy : MonoBehaviour
{
    private GameObject point;
    public string tag_point;
    NavMeshAgent nav;

    // Update is called once per frame
    void Update()
    {
        nav = GetComponent<NavMeshAgent>();
        point = GameObject.FindGameObjectWithTag(tag_point);
        nav.enabled = true;
        gameObject.GetComponent<Animator>().SetBool("Walk", true);
        nav.SetDestination(point.transform.position);
        
    }
}
