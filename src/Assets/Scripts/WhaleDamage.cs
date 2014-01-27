using UnityEngine;
using System.Collections;

public class WhaleDamage : Damagable {

	public override void Kill(GameObject killer)
	{
        //int whaleIndex = 0;
        //if (killer != null)
        //{
        //    for (int i = 0; i < 4; i++)
        //    {
        //        Globals.players[i] = "player";
        //    }
        //    whaleIndex = (killer.GetComponent<ShipMovement>().joystickIndex - 1);
        //    Globals.players[whaleIndex] = "whale";
        //}

		Application.LoadLevel(1);
	}
}
