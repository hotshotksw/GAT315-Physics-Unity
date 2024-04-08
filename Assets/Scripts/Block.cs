using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] int points = 0;
    [SerializeField] IntVariable score;
    [SerializeField] GameObject self;
    [SerializeField] AudioSource hitSound;
    [SerializeField] AudioSource pointSound;

    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("Trigger enter");
        if (other.gameObject.tag == "Ground")
        {
            hitSound.Play();
            Destroy(self, 1);
        } else if (other.gameObject.tag == "Ammo")
        {
            hitSound.Play();
        }
    }

    private void OnDestroy()
    {
        pointSound.Play();
        score.value += points;
    }
}
