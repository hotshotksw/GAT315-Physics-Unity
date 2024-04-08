using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
	[SerializeField] private PathFollower pathFollower;
	[SerializeField] private float healthValue;

	[SerializeField] private GameObject hitPrefab;
	[SerializeField] private GameObject destroyPrefab;

	private void Start()
	{
	}

	void Update()
    {
    }
}
