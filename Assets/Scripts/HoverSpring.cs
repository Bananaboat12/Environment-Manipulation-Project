using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoverSpring : MonoBehaviour
{
    float previousVelocity = 0;
    [SerializeField]
    float velocity = 0;
    float y = 0;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //velocity = GetComponentInParent<NavMeshAgent>().velocity.y;
        if (velocity != previousVelocity)
        {
            previousVelocity = velocity;
            y = velocity / 2;
        }
        else
        {
            previousVelocity = velocity;
            y = y / 50;
        }
        transform.position = new Vector3(transform.position.x,transform.position.y - y, transform.position.z + 0);
    }
}
