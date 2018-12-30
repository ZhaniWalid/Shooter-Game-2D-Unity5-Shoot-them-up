using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiManagerScript : MonoBehaviour {

	public Text scoreText;
	public Text healthText;
	bool gameOver;
	public static int score;
	int HealthPlayer;

	//singleton
	public static UiManagerScript Instance;

	//int TotalPlayerHealth;
	// Use this for initialization
	void Start()
	{
		gameOver = false;
		score = 0;
		HealthPlayer = 0;
		//TotalPlayerHealth = 100;
		// this function "InvokeRepeating",let you to repeat the call of another function in a number of time 
		// (how repeadtly call a function=9adh mn marra bsh tet3awd c a d)
		// besh t3ayt l fonction "scoreUpdate()"
		// à partir du temp=1seconde
		// et elle répète l'appelle du "scoreUpdate()" chaque 0.8 seconde
		// => début : lorsque temp=1seconde et répétetion chaque 0.8 seconde
		InvokeRepeating ("scoreUpdate",1.0f,0.4f);
		InvokeRepeating ("HealthPlayerUpdate",1.0f,0.1f);
	}

	// Update is called once per frame
	void Update()
	{
		scoreText.text = "Score: " + score;
		healthText.text = "Health: " + HealthPlayer + " %";
	}

	void scoreUpdate()
	{
		if (gameOver == false) {
			//score +=1;
			score = HealthScript.ScoreReturn;
		}
	}

	void HealthPlayerUpdate()
	{
		if (gameOver == false) {
			// Health -=1;
			HealthPlayer = HealthScript.playerHealthReturn;
		}
	}


	// Pause Method , for Pause/Resume The Game
	public void PauseResumeGame()
	{
		if (Time.timeScale == 1) { // Means that the Game is actually Running
			Time.timeScale = 0; // Pause The Game
		} else if (Time.timeScale == 0) { // Means that the Game is actually Not Running (Paused)
			Time.timeScale = 1; // Resume The Game
		}

	}

}
