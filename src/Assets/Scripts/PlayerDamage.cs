using UnityEngine;
using System.Collections;

public class PlayerDamage : Damagable {

	private Vector3 StartPosition;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		StartPosition = transform.position;
	}

    public override void Kill(GameObject killer)
	{
		health = StartHealth;
		transform.position = StartPosition;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}
}
