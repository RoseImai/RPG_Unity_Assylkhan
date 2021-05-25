using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigCrab : MonoBehaviour
{
    public GameObject player;
    public float dist;
    NavMeshAgent nav;
    public float Radius = 40f;

    public float HP_BigCrab = 1500;

    //damage 800
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (HP_BigCrab <= 0)
        {

            Destroy(gameObject);
        }

        dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist > Radius)
        {
            nav.enabled = false;
            gameObject.GetComponent<Animator>().SetTrigger("Idle");
        }

        if (dist < Radius && dist > 3f)
        {
            nav.enabled = true;
            nav.SetDestination(player.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger("Walk");
        }

        if (dist < 3f && HP_BigCrab >= 750)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Attack_1");
        }

        if (dist < 3f && HP_BigCrab < 750)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Attack_2");
        }
    }
}
