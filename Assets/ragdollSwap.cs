using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdollSwap : MonoBehaviour
{
    [SerializeField] GameObject ragdoll;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(ragdoll, transform.position, transform.rotation, transform);
        
    }
}
