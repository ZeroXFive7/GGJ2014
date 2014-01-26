using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    private ShipMovement parent;

	void Start ()
    {
        parent = transform.parent.gameObject.GetComponent<ShipMovement>();
        gameObject.layer = LayerMask.NameToLayer("Player" + parent.joystickIndex.ToString());
	}
	
	void Update ()
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, parent.windDirectionWS);
	}
}
