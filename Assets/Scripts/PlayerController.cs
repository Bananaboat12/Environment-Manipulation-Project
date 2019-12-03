using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Range(1,100)]
    float sensitivity;

    [SerializeField]
    float movementSpeed;

    Camera camFPS;



    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        camFPS = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerRotate();
    }

    void PlayerMove()
    {
        Vector3 movementX = Input.GetAxisRaw("Horizontal") * movementSpeed * transform.right;
        Vector3 movementY = Input.GetAxisRaw("Vertical") * movementSpeed * transform.forward;
        Vector3 movement = movementX + movementY + new Vector3(0,rb.velocity.y,0);
        rb.velocity = movement;
    }

    void PlayerRotate()
    {
        float mPosX = Input.GetAxisRaw("Mouse X") * (sensitivity / 50);
        Vector3 rotation = new Vector3(0, mPosX, 0);

        transform.eulerAngles = transform.eulerAngles + rotation;

        //the negative is to uninvert the rotation for Mouse Y's return value
        float mPosY = -(Input.GetAxisRaw("Mouse Y") * (sensitivity / 100));
        Vector3 camRotation = new Vector3(mPosY, 0, 0);

        camFPS.transform.eulerAngles = camFPS.transform.eulerAngles + camRotation;
    }
}
