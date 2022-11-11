using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerScript : MonoBehaviour
{
    public static bool isActive = false;
    public AudioSource audioSource;
    public float minPitch;
    public float maxPitch;
    public WheelColliders wheelColliders;
    public WheelMeshes wheelMeshes;
    Rigidbody carRb;
    public float speed;
    public float verticalInput;
    public float horizontalInput;
    public float slipAngle;
    public float motorPower;
    public float steeringAngle;
    public float brakeInput;
    public float brakePower;
    public AnimationCurve steerCurve;
    // Start is called before the first frame update
    void Start()
    {
        carRb = this.GetComponent<Rigidbody>();
        isActive = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        verticalInput = Input.GetAxis("Vertical");
        speed = carRb.velocity.magnitude;
        EngineSound();
        UpdateWheels();
        if(isActive)
        {
            GetInput();
            Accelerate();
            Steer();
            Brake();
        }
        
    }

    void Accelerate()
    {
        wheelColliders.wheelFL.motorTorque = motorPower*verticalInput;
        wheelColliders.wheelFR.motorTorque = motorPower*verticalInput;
        wheelColliders.wheelRL.motorTorque = motorPower*verticalInput;
        wheelColliders.wheelRR.motorTorque = motorPower*verticalInput;
    }

    void Steer()
    {
        steeringAngle = horizontalInput * steerCurve.Evaluate(speed);
        wheelColliders.wheelFL.steerAngle = steeringAngle;
        wheelColliders.wheelFR.steerAngle = steeringAngle;
    }

    void Brake()
    {
        wheelColliders.wheelFR.brakeTorque = brakeInput * brakePower * 0.7f;
        wheelColliders.wheelFL.brakeTorque = brakeInput * brakePower * 0.7f;
        wheelColliders.wheelRL.brakeTorque = brakeInput * brakePower * 0.3f;
        wheelColliders.wheelRR.brakeTorque = brakeInput * brakePower * 0.3f;
    }

    void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        slipAngle = Vector3.Angle(transform.forward,carRb.velocity-transform.forward);
        if(slipAngle < 120 && Input.GetKey(KeyCode.DownArrow))
        {
            if(verticalInput < 0)
            {
                brakeInput = Mathf.Abs(verticalInput);
                verticalInput = 0;
            }

        }
        else
        {
            brakeInput = 0;
        }
    }

    void UpdateWheels()
    {
        UpdateWheel(wheelColliders.wheelFL,wheelMeshes.wheelFL_Mesh);
        UpdateWheel(wheelColliders.wheelFR,wheelMeshes.wheelFR_Mesh);
        UpdateWheel(wheelColliders.wheelRL,wheelMeshes.wheelRL_Mesh);
        UpdateWheel(wheelColliders.wheelRR,wheelMeshes.wheelRR_Mesh);
    }

    void UpdateWheel(WheelCollider coll, MeshRenderer mesh)
    {
        Vector3 position;
        Quaternion quat;
        coll.GetWorldPose(out position,out quat);
        mesh.transform.position = position;
        mesh.transform.rotation = quat;
    }

    void EngineSound()
    {
        if(verticalInput==0)
        {
            audioSource.pitch = 0.85f;
        }
        if(speed < minPitch)
        {
            audioSource.pitch = 0.85f;
        }
        else if(speed > maxPitch)
        {
            audioSource.pitch = maxPitch/7;
        }
        else
        {
            audioSource.pitch = speed/7f;
        }
    }
}


[System.Serializable]
public class WheelColliders
{
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
}
[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer wheelFL_Mesh;
    public MeshRenderer wheelFR_Mesh;
    public MeshRenderer wheelRL_Mesh;
    public MeshRenderer wheelRR_Mesh;
}
