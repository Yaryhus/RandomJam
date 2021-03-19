using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steerAngle;
    private float m_brakeInput;

    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeForce = 500;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        m_brakeInput = Input.GetAxis("Brake");
        
    }
    void Steer()
    {
        m_steerAngle = maxSteeringAngle * m_horizontalInput;
        frontLeftWheelCollider.steerAngle = m_steerAngle;
        frontRightWheelCollider.steerAngle = m_steerAngle;
    }
    void Accelerate()
    {
        //Acceleration
        frontRightWheelCollider.motorTorque = m_verticalInput * motorForce;
        frontLeftWheelCollider.motorTorque = m_verticalInput * motorForce;

        //Brakes
        frontRightWheelCollider.brakeTorque = m_brakeInput * brakeForce;
        frontLeftWheelCollider.brakeTorque = m_brakeInput * brakeForce;

    }
    void UpdateWheelPoses()
    {
        UpdateWheelPose(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPose(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPose(rearRightWheelCollider, rearRightWheelTransform);
        UpdateWheelPose(rearLeftWheelCollider, rearLeftWheelTransform);
    }
    void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }
    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();

    }
}
