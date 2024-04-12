using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenImageTarget : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] Image image;
    [SerializeField] Camera view;

    private void LateUpdate()
    {
        Vector3 screen = image.transform.position;
        screen.z = distance;

        Vector3 world = view.ScreenToWorldPoint(screen);
        transform.position = world;
    }
}
