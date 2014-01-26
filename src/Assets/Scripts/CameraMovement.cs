using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform ship;

    [SerializeField]
    private float distanceAbove;

    [SerializeField]
    private float distanceBehind;

	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = ship.position + distanceBehind * Vector3.back + distanceAbove * Vector3.up;
	}
}
