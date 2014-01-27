using UnityEngine;
using System.Collections;

public class WhaleController : MonoBehaviour {

    public int PlayerNumber
    {
        get;
        private set;
    }

	public float RotationSpeed = 2.0f;
	public float SwimSpeed = 0.052f;
	public int Health = 100;
	
	private Quaternion TurningRotation = Quaternion.identity;
	
	// Use this for initialization
	public void Start() {
        for (var i = 0; i < Globals.players.Length; i++)
        {
            if (Globals.players[i] == "whale")
            {
                PlayerNumber = i;
                //CameraMovement cam = GameObject.Find("ShipCameraPlayer" + PlayerNumber).GetComponent<CameraMovement>();
                //cam.ship = transform;
                //cam.distanceAbove = 8;
                //cam.distanceBehind = 6;

                break;
            }
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<PlayerDamage>().Kill(null);
    }

	// Update is called once per frame
	public void Update() {
		UpdateMovement();
	}
	
	private void UpdateMovement() {
		string xAxisName = "Player " + PlayerNumber + " Whale X";
		string yAxisName = "Player " + PlayerNumber + " Whale Y";
		float x = Input.GetAxis(xAxisName);
		float y = Mathf.Clamp (-Input.GetAxis(yAxisName), 0, 1);
		transform.Rotate(0.0f, x * RotationSpeed, 0.0f);
		transform.Translate(Vector3.forward * y * SwimSpeed);
	}
	
}
