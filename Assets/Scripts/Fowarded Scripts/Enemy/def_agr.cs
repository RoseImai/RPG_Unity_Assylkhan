using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class def_agr : MonoBehaviour
{
    public GameObject player;
    public float dist;
    NavMeshAgent nav;
    public float Radius = 5f;
    public float HP_Skeleton = 100;
    public GameObject Ragdoll;
    private GameObject point;
    public string tag_point;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        if (HP_Skeleton <= 0)
        {
            Instantiate(Ragdoll, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        dist = Vector3.Distance(player.transform.position, transform.position);
        if(dist > Radius)
        {
            point = GameObject.FindGameObjectWithTag(tag_point);
            nav.enabled = true;
            gameObject.GetComponent<Animator>().SetTrigger("Walk");
            nav.SetDestination(point.transform.position);
        }
        if (dist < Radius && dist>1f)
        {
            nav.enabled = true;
            nav.SetDestination(player.transform.position);
            gameObject.GetComponent<Animator>().ResetTrigger("Walk");
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }

        if (dist < 1f)
        {
            nav.SetDestination(player.transform.position);
            gameObject.GetComponent<Animator>().ResetTrigger("Walk");
            gameObject.GetComponent<Animator>().ResetTrigger("Run");
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }
    }
}
