﻿using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other)
	{
		ShipMovement player = other.gameObject.GetComponent<ShipMovement>();
		if (player != null)
		{
			player.windDirectionWS = transform.forward;
		}
	}
}
