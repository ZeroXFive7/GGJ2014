using UnityEngine;
using System.Collections;

public class CannonballMovement : MonoBehaviour
{
    [SerializeField]
    private float Lifetime;

    private float spawnTime;

	public GameObject Owner;

    void Awake()
    {
        spawnTime = Time.time;
    }

    void OnCollisionEnter(Collision collision)
    {
		var otherHealth = collision.gameObject.GetComponent<Damagable>();
		if (otherHealth != null)
		{
			otherHealth.Damage(1, Owner);
		}

        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if ((Time.time - spawnTime) > Lifetime)
        {
            Destroy(gameObject);
        }
    }
}
