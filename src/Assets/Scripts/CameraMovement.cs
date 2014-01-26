using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private int joystickIndex;

    [SerializeField]
    private Transform ship;

    [SerializeField]
    private float distanceAbove;

    [SerializeField]
    private float distanceBehind;

    [SerializeField]
    private float rate;

    private enum FocusDirection
    {
        CENTER,
        LEFT,
        RIGHT
    }

    private FocusDirection focusDirection = FocusDirection.CENTER;

    private Vector3 directionBehind;
    private Vector3 directionLeft;
    private Vector3 directionRight;

    private Quaternion orbitRotation = Quaternion.identity;

    void Start()
    {
        directionBehind = distanceAbove * Vector3.up - distanceBehind * Vector3.forward;
        directionLeft   = distanceAbove * Vector3.up - distanceBehind * Vector3.right;
        directionRight  = distanceAbove * Vector3.up + distanceBehind * Vector3.right;
    }

	void FixedUpdate ()
    {
        ReadInput();

        Vector3 targetDirection = Vector3.zero;
        switch (focusDirection)
        {
            case FocusDirection.CENTER:
                targetDirection = directionBehind;
                break;
            case FocusDirection.LEFT:
                targetDirection = directionLeft;
                break;
            case FocusDirection.RIGHT:
                targetDirection = directionRight;
                break;
        }

        Quaternion targetRotation = Quaternion.FromToRotation(directionBehind, targetDirection);
        orbitRotation = Quaternion.Slerp(orbitRotation, targetRotation, rate * Time.deltaTime);

        transform.LookAt(ship, Vector3.up);

        transform.position = ship.position + ship.rotation * (orbitRotation * directionBehind);
	}

    private void ReadInput()
    {
        focusDirection = FocusDirection.CENTER;

        if (Input.GetAxis("CameraLeftJoystick" + joystickIndex) > 0.25f)
        {
            focusDirection = FocusDirection.LEFT;
        }

        if (Input.GetAxis("CameraRightJoystick" + joystickIndex) > 0.25f)
        {
            focusDirection = FocusDirection.RIGHT;
        }
    }
}
