using UnityEngine;
using System.Collections;
using System;

public class ShipMovement : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float maxRudderAngle;

    [SerializeField]
    private float maxSailAngle;

    [SerializeField]
    private float cannonKickback;

    public int joystickIndex;

    private Quaternion sailRotation = Quaternion.identity;
    private Vector3 rudderTangentLS = new Vector3(0.0f, 0.0f, -1.0f);
    public Vector3 windDirectionWS = new Vector3(-1.0f, 0.0f, 0.0f);

    private float relativeSpeed = 0.0f;

    private enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }

    private float slack;
    private float rudder;

    void FixedUpdate()
    {
        windDirectionWS = new Vector3(1.0f, 0.0f, 1.0f).normalized;

        ReadInput();
        UpdateSail();
        UpdateRudder();

        rigidbody.AddRelativeForce(Vector3.forward * maxSpeed * relativeSpeed);
    }

    private void ReadInput()
    {
        slack = Mathf.Max(Input.GetAxis("SlackJoystick" + joystickIndex), 0.0f);
        rudder = Input.GetAxis("RudderJoystick" + joystickIndex);
    }

    private void UpdateRudder()
    {
        Quaternion rudderRotationLS = Quaternion.Euler(0.0f, -rudder * maxRudderAngle, 0.0f);
        Vector3 targetLocal = (rudderRotationLS * -Vector3.forward).normalized;
        rudderTangentLS = Vector3.Lerp(rudderTangentLS, targetLocal, Time.deltaTime);

        Transform rudderObject = transform.FindChild("Rudder");
        rudderObject.transform.localRotation = Quaternion.FromToRotation(-Vector3.forward, rudderTangentLS);

        Vector3 targetRudderDirection = (Quaternion.FromToRotation(-Vector3.forward, rudderTangentLS) * -transform.forward).normalized;
        Vector3 newForward = Vector3.Lerp(transform.forward, -targetRudderDirection, Time.deltaTime);

        transform.rotation = Quaternion.FromToRotation(Vector3.forward, newForward);
    }

    private void UpdateSail()
    {
        UpdateSailRotation();

        Transform sail = transform.FindChild("Sail");
        sail.transform.localRotation = sailRotation;

        Vector3 sailNormalWS = sail.right;
        relativeSpeed = Mathf.Abs(Vector3.Dot(sailNormalWS, windDirectionWS));
    }

    private void UpdateSailRotation()
    {
        Vector3 fullTaughtTangentWS = -transform.forward;
        Vector3 fullSlackTangentWS = windDirectionWS;

        Quaternion oldRotation = sailRotation;

        Quaternion fullSlackRotation = Quaternion.FromToRotation(fullTaughtTangentWS, fullSlackTangentWS);
        fullSlackRotation = Quaternion.Slerp(Quaternion.identity, fullSlackRotation, (1.0f - slack));

        sailRotation = Quaternion.Lerp(sailRotation, fullSlackRotation, Time.deltaTime);
        if (Vector3.Angle((sailRotation * fullTaughtTangentWS), fullTaughtTangentWS) > maxSailAngle)
        {
            sailRotation = oldRotation;
        }
    }

    public void CannonRecoil(Vector3 position, Vector3 direction)
    {
        rigidbody.AddForceAtPosition(-direction * 100.0f * cannonKickback, position);
    }
}
