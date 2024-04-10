using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRagdoll : MonoBehaviour
{
    int count = 0;
    [SerializeField] GameObject ragdoll;
    [SerializeField] CapsuleCollider capsule;
    [SerializeField] GameObject mesh;

    private void OnCollisionEnter(Collision collision)
    {
        if (count < 1 && collision.gameObject.tag == "Player")
        {
            count++;
            Instantiate(ragdoll, transform.position, transform.rotation);
            capsule.gameObject.SetActive(false);
            mesh.SetActive(false);
        }
    }
}
