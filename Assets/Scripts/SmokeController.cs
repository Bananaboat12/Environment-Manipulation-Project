using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SmokeController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SetAnimationById(animId);
    }



    //visible in editor
    [SerializeField]
    int animId = 0;
    [SerializeField]
    Transform camPivot;
    

    //not visible in editor
    int prevId;
    Animator anim;
    float moveX, moveZ;



    // Update is called once per frame
    void Update()
    {
        if (animId == 0)
        {
            SetInput();
        }

        if (prevId != animId) {
            SetAnimationById(animId);
        }
    }

    void SetInput()
    {
        float yRot = Input.GetAxis("Mouse X");
        float xRot = -Input.GetAxis("Mouse Y");

        RotateCam(xRot, yRot);

        moveX = Input.GetAxisRaw("Vertical");
        moveZ = Input.GetAxisRaw("Horizontal");


        if (Input.GetKey(KeyCode.C))
        {
            moveX /= 2;
            moveZ /= 2;

        }


        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("Jump");

        }
        else
        {
            SetAnimationWalkAnim(moveX, moveZ);
        }
    }

    void RotateCam(float x, float y)
    {
        camPivot.localEulerAngles += new Vector3(x,0,0);
        transform.localEulerAngles += new Vector3(0, y, 0);
    }

    float xCurrent;
    float zCurrent;
    [SerializeField]
    float transitionSpeed = 2;

    [SerializeField]
    float zeroZoneAnim = 0.1f;

    void SetAnimationWalkAnim(float x, float z)
    {
        //try reusing to set walking animations smoothly
        if(isBetween(xCurrent,-zeroZoneAnim,zeroZoneAnim) && x == 0)
        {
            xCurrent = 0;
        }
        else
        {
            xCurrent += (2 * transitionSpeed * x * Time.deltaTime);
            xCurrent -= Time.deltaTime * transitionSpeed * (xCurrent / Mathf.Abs(xCurrent));
            xCurrent = Mathf.Clamp(xCurrent, -1, 1);
        }
        if (isBetween(zCurrent,-zeroZoneAnim,zeroZoneAnim) && z == 0)
        {
            zCurrent = 0;

        }
        else
        {
            zCurrent += (2 * transitionSpeed * z * Time.deltaTime);
            zCurrent -= Time.deltaTime * transitionSpeed * (zCurrent / Mathf.Abs(zCurrent));
            zCurrent = Mathf.Clamp(zCurrent, -1, 1);

        }


        anim.SetFloat("Input X", xCurrent);
        anim.SetFloat("Input Z", zCurrent);
    }

    bool isBetween(float value, float min, float max)
    {
        return (max >= value && value >= min);
    }

    void SetAnimationById(int id)
    {
        anim.SetInteger("Id", id);
        prevId = id;

        anim.SetTrigger("Any State Trigger");
    }
}
