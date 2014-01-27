using UnityEngine;
using System.Collections;
using System;

public static class Globals {
    public static string[]  players = {"","","",""};
}

public class GameMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    void gameStart(){
        if(Array.IndexOf(Globals.players, "boat")>=0){            
            //Load level
            Application.LoadLevel(1);
        }
        
    }

    bool setBoat(int playerIndex) {
        Globals.players[playerIndex] = "boat";
        TextMesh mesh = (TextMesh)GameObject.Find("Player " + playerIndex.ToString()).GetComponent("TextMesh");
        mesh.text = "BOAT";
        
        return true;
    }

    bool setWhale(int playerIndex){
        //If no player is the whale, set the whale
        Debug.Log(Array.IndexOf(Globals.players, "whale"));
        if( Array.IndexOf(Globals.players, "whale") < 0 ){
            Globals.players[playerIndex] = "whale";
            TextMesh mesh = (TextMesh)GameObject.Find("Player " + playerIndex.ToString()).GetComponent("TextMesh");
            mesh.text = "WHALE";
            return true;
        }

        return false;    
    }

    bool removePlayer(int playerIndex){
        Globals.players[playerIndex] = "";
        TextMesh mesh = (TextMesh)GameObject.Find("Player " + playerIndex.ToString()).GetComponent("TextMesh");
        mesh.text = "<PRESS A TO JOIN>";
        return true;
    }

	// Update is called once per frame
	void Update () {
        
        //START = Start game
        if(Input.GetButtonUp("Any Player Start")){
            gameStart();
        }

        //ESC = Quit Game
        if (Input.GetButtonUp("Any Player Quit")) {
            Application.Quit();
        }
    

        //LEFT = Set player as boat
        //RIGHT = Set player as whale
        Debug.Log(Input.GetAxis("Player 0 Left Analog"));

        if (Input.GetAxis("Player 0 Left Analog") < -0.8 ||
            (Input.GetButtonUp("Player 0 A Button") && Globals.players[0] != "boat"))
        {
            setBoat(0);
        }
        else if (Input.GetAxis("Player 0 Left Analog") > 0.8 ||
            (Input.GetButtonUp("Player 0 A Button") && Globals.players[0] != "whale"))
        {
            setWhale(0);
        }
        if (Input.GetAxis("Player 1 Left Analog") < -0.8 ||
            (Input.GetButtonUp("Player 1 A Button") && Globals.players[1] != "boat"))
        {
            setBoat(1);
        }
        else if (Input.GetAxis("Player 1 Left Analog") > 0.8 ||
            (Input.GetButtonUp("Player 1 A Button") && Globals.players[1] != "whale"))
        {
            setWhale(1);
        }
        if (Input.GetAxis("Player 2 Left Analog") < -0.8 ||
            (Input.GetButtonUp("Player 2 A Button") && Globals.players[2] != "boat"))
        {
            setBoat(2);
        }
        else if (Input.GetAxis("Player 2 Left Analog") > 0.8 ||
            (Input.GetButtonUp("Player 2 A Button") && Globals.players[2] != "whale"))
        {
            setWhale(2);
        }
        if (Input.GetAxis("Player 3 Left Analog") < -0.8 ||
            (Input.GetButtonUp("Player 3 A Button") && Globals.players[3] != "boat"))
        {
            setBoat(3);
        }
        else if (Input.GetAxis("Player 3 Left Analog") > 0.8 ||
            (Input.GetButtonUp("Player 3 A Button") && Globals.players[3] != "whale"))
        {
            setWhale(3);
        }

        //B BUTTON = Remove player
        if (Input.GetButton("Player 0 B Button"))
        {
            removePlayer(0);
        }
        if (Input.GetButton("Player 1 B Button"))
        {
            removePlayer(1);
        }
        if (Input.GetButton("Player 2 B Button"))
        {
            removePlayer(2);
        }
        if (Input.GetButton("Player 3 B Button"))
        {
            removePlayer(3);
        }

	}
}