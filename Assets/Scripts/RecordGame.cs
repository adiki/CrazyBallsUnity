using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordGame : MonoBehaviour, GameDelegate
{
	public Text timer;
	public Text playerPoints;

	public GameObject player;
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;

	private Game game;

	void Start ()
	{
		game = new Game (this, player, enemy1, enemy2, enemy3);
	}

	void FixedUpdate ()
	{
		game.Update ();
	}

	public void GameDidUpdateTimer (Game game, string timerText)
	{
		timer.text = timerText;
	}

	public void GameDidUpdatePoints (Game game)
	{
		playerPoints.text = game.playerPoints.ToString ();
	}

	public void GameDidResetBall (Game game, Ball ball)
	{
		if (ball.rigidBody.mass < 1) {
			ball.rigidBody.mass = Mathf.Min(ball.rigidBody.mass + 0.05f, 1);
		} else {
			ball.movementFactor = Mathf.Min (ball.movementFactor, 3);
		}
	}
}
