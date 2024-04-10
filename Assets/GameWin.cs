using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    [SerializeField] GameObject winScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") winScreen.SetActive(true);
    }
}
