using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EyeBotController : MonoBehaviour
{
    NavMeshAgent nav;

    [SerializeField]
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.position);
        //transform.LookAt(transform.position + nav.velocity);

    }


}
