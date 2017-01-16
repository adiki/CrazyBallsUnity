using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManagerScript : MonoBehaviour
{
	public GameObject mapsPanel;

	void Start ()
	{
//		mapsPanel.SetActive (false);
	}

	public void loadScene (string sceneName)
	{
		SceneManager.LoadScene (sceneName);	
	}

	public void openMaps ()
	{
//		startPanelAnimator.SetBool ("isHidden", true);
//		mapsPanel.SetActive (true);
//		mapsPanelAnimator.SetBool ("isHidden", false);
	}
}
