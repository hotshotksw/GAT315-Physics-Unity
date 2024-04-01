using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidBodyMovement : MonoBehaviour
{
    [SerializeField] Vector3 force;
    [SerializeField] ForceMode mode;
    [SerializeField] Vector3 torque;
    [SerializeField] ForceMode torqueMode;
    [SerializeField] KeyCode jumpKey;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        if (Input.GetKey(jumpKey))
        {
            rb.AddForce(force, mode);
            rb.AddTorque(torque, torqueMode);
        }
    }
}
