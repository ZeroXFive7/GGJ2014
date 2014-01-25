using UnityEngine;
using System.Collections;
using System;

public class ShipMovement : MonoBehaviour
{
    [SerializeField]
    private float MaxSpeed;

    [SerializeField]
    private float MaxRudderAngle;
    
    private Vector3 windDirection = new Vector3(-1.0f, 0.0f, 0.0f);

    private Vector3 sailDirection = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 rudderDirection = new Vector3(0.0f, 0.0f, -1.0f);

    private Vector3 localRudder = new Vector3(0.0f, 0.0f, -1.0f);

    private float speed = 0.0f;

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

        UpdateForward();
        UpdateSpeed();

        rigidbody.velocity = -rudderDirection * speed;
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

    private void UpdateForward()
    {
        Quaternion rotation = Quaternion.Euler(0.0f, -rudder * MaxRudderAngle, 0.0f);
        Vector3 targetLocal = (rotation * -Vector3.forward).normalized;
        localRudder = Vector3.Lerp(localRudder, targetLocal, Time.deltaTime);

        Vector3 targetRudderDirection = (Quaternion.FromToRotation(-Vector3.forward, localRudder) * rudderDirection).normalized;
        rudderDirection = Vector3.Lerp(rudderDirection, targetRudderDirection, Time.deltaTime);

        float angle = Vector3.Angle(Vector3.forward, -rudderDirection);
        if (Vector3.Cross(Vector3.forward, -rudderDirection).y <= 0.0f)
        {
            angle *= -1.0f;
        }

        transform.rotation = Quaternion.Euler(90.0f, angle, 0.0f);

        Debug.DrawRay(transform.position, rudderDirection, Color.yellow);
        Debug.DrawRay(transform.position, localRudder, Color.green);
    }

    private void UpdateSpeed()
    {
        speed = (1.0f - Vector2.Dot(sailDirection, -windDirection)) * MaxSpeed;
    }
}
