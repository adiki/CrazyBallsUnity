using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1 : MonoBehaviour, GameDelegate
{

	public Text startTimer;
	public Text gameTimer;

	public GameObject player;
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;

	private Game game;

	void Start ()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		game = new Game (this, 60, player, enemy1, enemy2, enemy3);
	}

	void FixedUpdate ()
	{
		game.Update ();
	}

	void OnApplicationPause (bool paused)
	{
		if (!paused && game != null) {
			game.Paused = true;
		}
	}

	public void SwitchPause ()
	{
		game.Paused = !game.Paused;
	}

	public void GameDidUpdateStartTimer (Game game, string startTimerText)
	{
		startTimer.text = startTimerText;
	}

	public void GameDidUpdateGameTimer (Game game, string gameTimerText)
	{
		gameTimer.text = gameTimerText;
	}

	public void GameDidUpdatePoints (Game game)
	{
	}

	public void GameDidResetBall (Game game, Ball ball)
	{
	}
}
