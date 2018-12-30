using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// Start or quit the game

public class GameOverScript : MonoBehaviour {

	private Button[] buttons;
	private Text[] FinalScores;

	void Awake()
	{
		// Get the buttons
		buttons = GetComponentsInChildren<Button> ();
		FinalScores = GameObject.FindGameObjectWithTag("PlayerFinalScore").GetComponentsInChildren<Text>();
		//FinalScores = GetComponentsInChildren<Text> ();
		// Disable them
		HideButtons();
		HideFinalScore();
	}

	public void HideButtons()
	{
		foreach (var b in buttons) {
			b.gameObject.SetActive(false);
		}
	}

	public void HideFinalScore()
	{
		//FinalScore.gameObject.SetActive(false);
		foreach (var s in FinalScores) {
			s.gameObject.SetActive(false);
		}
	}

	public void ShowButtons()
	{
		foreach (var b in buttons) {
			b.gameObject.SetActive(true);
		}
	}

	public void ShowFinalScore()
	{
		//FinalScore.gameObject.SetActive(true);
		foreach (var s in FinalScores) {
			s.gameObject.SetActive (true);
			s.text = "Your Final Score Is: "+ UiManagerScript.score;
		}

	}

	public void ExitToMenu()
	{
		// Reload the level
		Application.LoadLevel ("Menu");
	}

	public void RestartGame()
	{
		// Reload the level
		Application.LoadLevel ("Stage1");
		// Reset Values of : Score = 0 and Health = 0
		HealthScript.ScoreReturn = 0;
		HealthScript.playerHealthReturn = 0;
	}
}
