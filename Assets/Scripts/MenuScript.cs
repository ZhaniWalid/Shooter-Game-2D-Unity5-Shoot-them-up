using UnityEngine;
using System.Collections;

/// Title screen script

public class MenuScript : MonoBehaviour {
	

	// Use this for initialization
	public void StartGame ()
	{
		// "Stage1" is the name of the first scene we created.
		Application.LoadLevel ("Stage1");
		// Reset Values of : Score = 0 and Health = 0
		HealthScript.ScoreReturn = 0;
		HealthScript.playerHealthReturn = 0;
	}

	public void ExitGame()
	{
		Application.Quit ();
	}
	
}
