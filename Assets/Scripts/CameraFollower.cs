using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] GameObject obj;

    // Update is called once per frame
    void Update()
    {
        transform.position = obj.transform.position;
    }
}
