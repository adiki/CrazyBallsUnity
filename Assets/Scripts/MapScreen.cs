using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapScreen : MonoBehaviour
{

	public GameObject content;
	public Animator animator;

	private int levelToOpen;
	private static float positionX = 0;

	void Start ()
	{
		content.transform.Translate (new Vector3 (positionX, 0, 0));
	}

	public void openLevel (int level)
	{
		animator.SetBool ("isGameHidden", false);
		positionX = content.transform.transform.position.x;
		levelToOpen = level;
	}

	public void AnimationDidFaded ()
	{
		SceneManager.LoadScene ("Level" + levelToOpen);
	}
}
