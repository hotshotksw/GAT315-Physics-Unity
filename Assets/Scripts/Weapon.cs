using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject ammo;
    [SerializeField] Transform emission;
    [SerializeField] float fireRate;
    [SerializeField] AudioSource sound;

    bool fireReady = true;

    void Update()
    {
        if (fireReady && Input.GetMouseButtonDown(0))
        {
            Instantiate(ammo, emission.position, emission.rotation);
            sound.Play();
            fireReady = false;
            StartCoroutine(FireTimer(fireRate));
        }
    }

    IEnumerator FireTimer(float time)
    {
        yield return new WaitForSeconds(time);
        fireReady = true;
    }
}