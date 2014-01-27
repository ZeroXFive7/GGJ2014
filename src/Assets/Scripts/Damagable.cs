using UnityEngine;
using System.Collections;

public class Damagable : MonoBehaviour {
	[SerializeField]
	public float StartHealth = 10;

	[SerializeField]
	protected float health;

	// Use this for initialization
	protected virtual void Start () {
		health = StartHealth;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}

	public bool Damage(float damage)
	{
	    return Damage (damage, null);
	}

	// Did the damage kill the thing
	public bool Damage(float damage, GameObject attacker)
	{
		health -= damage;
		if (health <= 0)
		{
			Kill (attacker);
			return true;
		}
		return false;
	}

	// Kill it dead
	public virtual void Kill(GameObject killer)
	{

	}
}
