using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface GameDelegate
{
	void GameDidUpdateTimer (Game game, string timerText);

	void GameDidUpdatePoints (Game game);

	void GameDidResetBall (Game game, Ball ball);
}

public class Game
{

	private GameDelegate gameDelegate;

	private float timeLeftToStart = 3.5f;

	private bool started;

	public Ball playerBall;
	public Ball enemy1Ball;
	public Ball enemy2Ball;
	public Ball enemy3Ball;

	public Game (GameDelegate gameDelegate, GameObject player, GameObject enemy1, GameObject enemy2, GameObject enemy3)
	{
		this.gameDelegate = gameDelegate;
		playerBall = player.GetComponent<Ball> ();
		enemy1Ball = enemy1.GetComponent<Ball> ();
		enemy2Ball = enemy2.GetComponent<Ball> ();
		enemy3Ball = enemy3.GetComponent<Ball> ();
	}

	public void Update ()
	{
		timeLeftToStart -= Time.deltaTime;
		if (!started) {
			if (timeLeftToStart > 0) {
				gameDelegate.GameDidUpdateTimer (this, Mathf.Min (Mathf.Ceil (timeLeftToStart), 3).ToString ());
			} else {
				gameDelegate.GameDidUpdateTimer (this, "");
				Start ();
			}
		}

		CountPointsAndResetPositionsIfNeeded ();
	}

	private void Start ()
	{
		started = true;
		playerBall.started = true;
		enemy1Ball.started = true;
		enemy2Ball.started = true;
		enemy3Ball.started = true;
	}

	private void CountPointsAndResetPositionsIfNeeded ()
	{

		CountPoints (playerBall);
		CountPoints (enemy1Ball);
		CountPoints (enemy2Ball);
		CountPoints (enemy3Ball);

		ResetPositionsIfNeeded (playerBall);
		ResetPositionsIfNeeded (enemy1Ball);
		ResetPositionsIfNeeded (enemy2Ball);
		ResetPositionsIfNeeded (enemy3Ball);
	}

	private void CountPoints (Ball ball)
	{
		if (ball.transform.position.y > -1) {
			return;
		}

		if (ball.lastColliders.Count == 0) {
			return;
		}

		Ball lastColliderBall = ball.lastColliders[0].GetComponent<Ball> ();

		if (lastColliderBall.transform.position.y > -0.1) {
			ball.lastColliders.Clear ();
			lastColliderBall.lastColliders.Clear ();
			lastColliderBall.AddPoint ();
			gameDelegate.GameDidUpdatePoints (this);
		}
	}

	private void ResetPositionsIfNeeded (Ball ball)
	{
		if (ball.transform.position.y > -50) {
			return;
		}

		ball.rigidBody.velocity = Vector3.zero;

		if (ball == playerBall) { 
			playerBall.transform.position = new Vector3 (0, 1, 0);	
		} else if (ball == enemy1Ball) {
			enemy1Ball.transform.position = new Vector3 (3, 1, -2);	
		} else if (ball == enemy2Ball) {
			enemy2Ball.transform.position = new Vector3 (-3, 1, -2);	
		} else if (ball == enemy3Ball) {
			enemy3Ball.transform.position = new Vector3 (0, 1, 3);	
		}	

		gameDelegate.GameDidResetBall (this, ball);
	}
}
