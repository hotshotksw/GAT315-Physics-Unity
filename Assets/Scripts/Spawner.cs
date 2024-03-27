using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] KeyCode trigger;

    void Start()
    {
        
    }
    
    void Update()
    {
        if(Input.GetKeyDown(trigger))
        {
            Instantiate(obj, gameObject.transform);
        }
    }
}
