﻿using UnityEngine;
using System.Collections;
using System;

public class ShipMovement : MonoBehaviour
{
    [SerializeField]
    private Transform cannonball;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float maxRudderAngle;

    [SerializeField]
    private float maxSailAngle;

    private Quaternion sailRotation = Quaternion.identity;
    private Vector3 rudderTangentLS = new Vector3(0.0f, 0.0f, -1.0f);
    private Vector3 windDirectionWS = new Vector3(-1.0f, 0.0f, 0.0f);

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
        ReadInput();
        UpdateSail();
        UpdateRudder();

        rigidbody.velocity = transform.forward * maxSpeed * relativeSpeed;
    }

    void OnGUI()
    {

    }

    private void ReadInput()
    {
        slack = Mathf.Max(Input.GetAxis("Slack"), 0.0f);
        rudder = Input.GetAxis("Rudder");
    }

    private void UpdateRudder()
    {
        Quaternion rudderRotationLS = Quaternion.Euler(0.0f, -rudder * maxRudderAngle, 0.0f);
        Vector3 targetLocal = (rudderRotationLS * -Vector3.forward).normalized;
        rudderTangentLS = Vector3.Lerp(rudderTangentLS, targetLocal, Time.deltaTime);

        Vector3 targetRudderDirection = (Quaternion.FromToRotation(-Vector3.forward, rudderTangentLS) * -transform.forward).normalized;
        Vector3 newForward = Vector3.Lerp(transform.forward, -targetRudderDirection, Time.deltaTime);

        transform.rotation = Quaternion.FromToRotation(Vector3.forward, newForward);

        Debug.DrawRay(transform.position - transform.forward * 2.0f, transform.rotation * rudderTangentLS, Color.red);
    }

    private void UpdateSail()
    {
        UpdateSailRotation();

        Transform sail = transform.GetChild(0);
        sail.transform.localRotation = sailRotation;

        Vector3 sailNormalWS = sailRotation * transform.right;
        if (Vector3.Cross(windDirectionWS, sailRotation * transform.forward).y < 0.0f)
        {
            sailNormalWS *= -1.0f;
        }
        Debug.DrawRay(transform.position, sailNormalWS, Color.blue);

        relativeSpeed = Vector2.Dot(sailNormalWS, -windDirectionWS);
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

        Debug.DrawRay(transform.position, 2.5f * (sailRotation * fullTaughtTangentWS), Color.green);
    }
}
