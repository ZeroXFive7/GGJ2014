using UnityEngine;
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
		var player = other as ShipMovement;
		if (player)
		{
			player.windDirectionWS = transform.forward;
		}
	}
}
