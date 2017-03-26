using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1 : MonoBehaviour, GameDelegate
{

	public Animator animator;

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
		if (game == null) {
			return;
		}

		game.Paused = true;
		animator.SetBool ("isPausePanelHidden", false);
	}

	public void PauseGame ()
	{
		game.Paused = true;
		animator.SetBool ("isPausePanelHidden", false);
	}

	public void ResumeGame ()
	{
		animator.SetBool ("isPausePanelHidden", true);
		StartCoroutine (UnpauseGame ());
	}

	public void OpenMenu ()
	{
		game.Paused = false;
		animator.SetBool ("isGameHidden", true);
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

	IEnumerator UnpauseGame ()
	{
		float pauseEndTime = Time.realtimeSinceStartup + 0.5f;
		while (Time.realtimeSinceStartup < pauseEndTime) {
			yield return 0;
		}

		game.Paused = false;
	}

	public void AnimationDidFaded()
	{
		SceneManager.LoadScene ("MapScreen");
	}
}
