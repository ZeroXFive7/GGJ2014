using UnityEngine;
using System.Collections;
using System;

public class ShipMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    [SerializeField]
    private float MaxRudderAngle;

    private Vector3 windDirection = new Vector3(-1.0f, 0.0f, 0.0f);

    private Vector3 sailDirection = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 rudderDirection = new Vector3(0.0f, 0.0f, -1.0f);

    private enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }

    private float slack;
    private float rudder;
    private bool[] isFiring;

	void Start () 
    {
        isFiring = new bool[4];
	}
	
    void Update ()
    {
        ReadInput();
        rigidbody.velocity = ForwardDirection() * ForwardSpeed();
	}

    void OnGui()
    {

    }

    private void ReadInput()
    {
        slack = -Input.GetAxis("Slack");
        rudder = Input.GetAxis("Rudder");

        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            isFiring[(int)dir] = Input.GetButton("Fire" + dir.ToString());
        }
    }

    private Vector3 ForwardDirection()
    {
        Vector3 targetDirection = Quaternion.AngleAxis(rudder * MaxRudderAngle, Vector3.up) * rigidbody.velocity.normalized;
        rudderDirection = Vector3.Lerp(rudderDirection, targetDirection, Time.deltaTime).normalized;

        Debug.DrawRay(transform.position, rudderDirection, Color.yellow);
        return rudderDirection;
    }

    private float ForwardSpeed()
    {
        float magnitude = Vector2.Dot(sailDirection, windDirection) * Speed;
        return magnitude;
        return 0.0f;
    }
}
