using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform ship;

	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(ship.position.x, transform.position.y, ship.position.z);
	}
}
