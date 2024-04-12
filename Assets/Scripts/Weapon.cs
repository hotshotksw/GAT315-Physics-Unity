using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject ammo;
    [SerializeField] Transform emission;
    [SerializeField] float fireRate;
    [SerializeField] AudioSource audioSource;

    bool fireReady = true;

    public bool equipped = false;
    void Update()
    {
        Debug.DrawRay(emission.position, emission.forward * 10, Color.red);

        if (equipped && Input.GetMouseButtonDown(0))
        {
            if (audioSource != null) audioSource.Play();
            Instantiate(ammo, emission.position, emission.rotation);
        }
    }

    IEnumerator FireTimer(float time)
    {
        yield return new WaitForSeconds(time);
        fireReady = true;
    }
}