using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	private Vector3 firstPosition;
	private List<GameObject> resultsPositions = new List<GameObject> ();

	private bool replay;

	void Start ()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		firstPosition = player.GetComponent<Ball> ().resultPanel.transform.position;

		resultsPositions.Add (player);
		resultsPositions.Add (enemy1);
		resultsPositions.Add (enemy2);
		resultsPositions.Add (enemy3);
		game = new Game (this, 60, player, enemy1, enemy2, enemy3);
	}

	void FixedUpdate ()
	{
		game.Update ();

		for (int i = 0; i < resultsPositions.Count; ++i) {
			resultsPositions [i].GetComponent<Ball> ().resultPanel.transform.position = Vector3.Lerp (resultsPositions [i].GetComponent<Ball> ().resultPanel.transform.position, 
				new Vector3 (firstPosition.x, -90 * i + firstPosition.y, firstPosition.z),
				5f * Time.deltaTime);
		}
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

	public void ReplayGame ()
	{
		replay = true;
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
		resultsPositions = resultsPositions.OrderByDescending (ball => ball.GetComponent<Ball> ().points).ToList ();
	}

	public void GameDidResetBall (Game game, Ball ball)
	{
	}

	public void GameDidFinishGame (Game game) 
	{
		animator.SetBool ("isFinishPanelHidden", false);
	}

	IEnumerator UnpauseGame ()
	{
		float pauseEndTime = Time.realtimeSinceStartup + 0.5f;
		while (Time.realtimeSinceStartup < pauseEndTime) {
			yield return 0;
		}

		game.Paused = false;
	}

	public void AnimationDidFaded ()
	{
		if (replay) {
			SceneManager.LoadScene ("Level1");
		} else {
			SceneManager.LoadScene ("MapScreen");
		}
	}
}
