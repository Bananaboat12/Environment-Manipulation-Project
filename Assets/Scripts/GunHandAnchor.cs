using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandAnchor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform anchor;


    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(anchor, Vector3.up);
    }
}
