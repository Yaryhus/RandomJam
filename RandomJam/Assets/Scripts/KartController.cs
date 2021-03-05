using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour
{
    [Header("Rigidbody")]
    public Rigidbody rb;

    [Header("Variables")]
    public float forwardAccel = 8f;
    public float reverseAccel = 4f;
    public float maxSpeed = 50f;
    public float turnStrengh = 180f;
    public float gravityForce = 10f;
    public float dragOnGround = 3.0f;

    [Header("Suelo")]
    public LayerMask WhatIsGround;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;

    [Header("Wheels")]
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public float maxWheelRot;

    private float speedInput, turnInput;
    bool grounded;
    Quaternion rot;

    void Start()
    {
        rb.transform.parent = null;
    }

    private void Update()
    {
        speedInput = 0f;

        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }

        //Multiplied for vertical, to have the rotation flipped when inverse, or not be able to turn if stopped.
        turnInput = Input.GetAxis("Horizontal");

        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrengh * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        frontLeftWheelTransform.localRotation = Quaternion.Euler(frontLeftWheelTransform.localRotation.eulerAngles.x, turnInput * maxWheelRot, frontLeftWheelTransform.localRotation.eulerAngles.z);
        frontRightWheelTransform.localRotation = Quaternion.Euler(frontRightWheelTransform.localRotation.eulerAngles.x, turnInput * maxWheelRot, frontRightWheelTransform.localRotation.eulerAngles.z);

        transform.position = rb.transform.position;
    }

    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;

        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, WhatIsGround))
        {
            //Debug.Log("Hitting ground");
            grounded = true;
            rot = Quaternion.FromToRotation(transform.up, hit.normal);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot * transform.rotation, 10 * Time.deltaTime);
        }

        /*
        //Car smooth orientation, needs larger ray for jumps
        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength + 100, WhatIsGround))
        {
        }
        */

        if (grounded)
        {
            rb.drag = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                rb.AddForce(transform.forward * speedInput);
            }
        }
        else
        {
            rb.drag = 0.1f;
            rb.AddForce(Vector3.up * -gravityForce * 100f);
        }
    }

}
