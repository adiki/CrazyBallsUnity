using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class Level : MonoBehaviour, GameDelegate {

	public int levelNumber;

	public Animator animator;

	public Text startTimer;
	public Text gameTimer;

	public GameObject star1;
	public GameObject star2;
	public GameObject star3;
	public GameObject tryAgain;
	public Button nextButton;

	public GameObject player;
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;

	protected Game game;

	private Vector3 firstPosition;
	private float positionsSpace;
	private List<GameObject> resultsPositions = new List<GameObject> ();

	private bool replay;
	private bool next;

	void Start ()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		firstPosition = player.GetComponent<Ball> ().resultPanel.transform.position;
		positionsSpace = enemy1.GetComponent<Ball> ().resultPanel.transform.position.y - firstPosition.y;

		resultsPositions.Add (player);
		resultsPositions.Add (enemy1);
		resultsPositions.Add (enemy2);
		resultsPositions.Add (enemy3);

		game = new Game (this, gameTime (), player, enemy1, enemy2, enemy3);

		MapScreen.lastPlayedLevel = levelNumber;
	}

	protected abstract int gameTime ();
	protected abstract int pointsForStar1 ();
	protected abstract int pointsForStar2 ();
	protected abstract int pointsForStar3 ();

	void FixedUpdate ()
	{

		game.Update ();

		resultsPositions = resultsPositions.OrderByDescending (ball => ball.GetComponent<Ball> ().points).ToList ();

		for (int i = 0; i < resultsPositions.Count; ++i) {
			resultsPositions [i].GetComponent<Ball> ().resultPanel.transform.position = Vector3.Lerp (resultsPositions [i].GetComponent<Ball> ().resultPanel.transform.position, 
				new Vector3 (firstPosition.x, positionsSpace * i + firstPosition.y, firstPosition.z),
				5f * Time.deltaTime);
		}

		if (Input.GetKeyUp (KeyCode.Escape)) {
			game.Paused = true;
			animator.SetBool ("isPausePanelHidden", false);
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

	public void OpenNext ()
	{
		next = true;
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

	public void GameDidFinishGame (Game game)
	{
		tryAgain.SetActive (!game.didPlayerWin ());

		if (game.didPlayerWin () && DataStore.unlockedLevelNumber () == levelNumber) {
			DataStore.bumpLevel ();
		}

		int starsNumber = 0;
		if (game.playerBall.points >= pointsForStar1 ()) {
			star1.GetComponent<Image>().color = new Color(1f, 0.6f, 0f, 1f);
			++starsNumber;
		}
		if (game.playerBall.points >= pointsForStar2 ()) {
			star2.GetComponent<Image>().color = new Color(1f, 0.6f, 0f, 1f);
			++starsNumber;
		}
		if (game.playerBall.points >= pointsForStar3 ()) {
			star3.GetComponent<Image>().color = new Color(1f, 0.6f, 0f, 1f);
			++starsNumber;
		}
		DataStore.updateStarsForLevel (levelNumber, starsNumber);
		nextButton.interactable = DataStore.unlockedLevelNumber () > levelNumber;
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
			SceneManager.LoadScene ("Level" + levelNumber);
		} else if (next) {
			int nextLevel = levelNumber + 1;
			SceneManager.LoadScene ("Level" + nextLevel);
		} else {
			SceneManager.LoadScene ("MapScreen");
		}
	}

	public void FinishPanelDidAppear ()
	{
		Invoke ("activateStar1", 0.5f);
		Invoke ("activateStar2", 1);
		Invoke ("activateStar3", 1.5f);
	}

	private void activateStar1() 
	{
		star1.SetActive (game.didPlayerWin ());
	}

	private void activateStar2() 
	{
		star2.SetActive (game.didPlayerWin ());
	}

	private void activateStar3() 
	{
		star3.SetActive (game.didPlayerWin ());
	}
}
