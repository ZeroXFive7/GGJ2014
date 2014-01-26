using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

    public string FireInput = "Input Name";
    public float FireCooldown = 2.0f;
    public float FireVelocity = 2.0f;
    public GameObject Amunition = null;

    private float lastFireTime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetButton(FireInput) && Time.time - lastFireTime > FireCooldown)
        {
            lastFireTime = Time.time;
            GameObject bullet = GameObject.Instantiate(Amunition, transform.position, Quaternion.identity) as GameObject;
            bullet.rigidbody.velocity = transform.forward * FireVelocity + transform.parent.rigidbody.velocity;
			Physics.IgnoreCollision(transform.parent.collider, bullet.collider);
        }
    }
}
