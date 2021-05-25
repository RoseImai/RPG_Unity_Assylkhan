using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float dist;
    NavMeshAgent nav;
    public float Radius = 15f;
    public float HP_Skeleton = 100;
    public GameObject Ragdoll;
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
        if (dist > Radius)
        {
            nav.enabled = false;
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
        }
        if (dist < Radius && dist>1.5f)
        {
            nav.enabled = true;
            nav.SetDestination(player.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger("Run");
        }

        if (dist < 1.5f)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }
        
    }
}
