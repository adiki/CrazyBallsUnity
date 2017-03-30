﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapScreen : MonoBehaviour
{
	public Animator animator;
	public GameObject scrollView;
	public GameObject scrollViewContentView;

	private int levelToOpen;
	private static float positionX = 0;

	void Start ()
	{
		for (int i = 2; i <= 15; ++i) {
			Button button = GameObject.FindGameObjectWithTag (i.ToString ()).GetComponent<Button> ();
			button.interactable = i <= DataStore.unlockedLevelNumber ();
		}

		for (int i = 1; i <= 15; ++i) {
			for (int j = 1; j <= DataStore.starsForLevel (i); ++j) {
				Image startImage = GameObject.FindGameObjectWithTag ("level" + i + "star" + j).GetComponent<Image> ();
				startImage.color = new Color (1f, 0.6f, 0f, 1f);
			}
		}

		if (positionX == 0) {
			GameObject unlockedPanel = GameObject.FindGameObjectWithTag ("panel" + DataStore.unlockedLevelNumber ());
			float posX = unlockedPanel.GetComponent<RectTransform> ().anchoredPosition.x - scrollView.GetComponent<RectTransform> ().rect.width / 2;
			float x = Mathf.Min (Mathf.Max (-scrollViewContentView.GetComponent<RectTransform> ().rect.width + scrollView.GetComponent<RectTransform> ().rect.width, -posX), 0);
			Rect rect = scrollViewContentView.GetComponent<RectTransform> ().rect;
			scrollViewContentView.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (x, 0, 0);
		} else {
			scrollViewContentView.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (positionX, 0, 0);
		}
	}

	void Update() 
	{
		if (Input.GetKeyUp(KeyCode.Escape)) {
			if (Application.platform == RuntimePlatform.Android) {
				AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
				activity.Call<bool>("moveTaskToBack", true);
			}
		}
	}

	public void openLevel (int level)
	{
		animator.SetBool ("isGameHidden", false);
		positionX = scrollViewContentView.GetComponent<RectTransform> ().anchoredPosition.x;
		levelToOpen = level;
	}

	public void AnimationDidFaded ()
	{
		SceneManager.LoadScene ("Level" + levelToOpen);
	}
}
