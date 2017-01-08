using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

	public Animator startPanelAnimator;
	public GameObject mapsPanel;

	public void loadScene(string sceneName) {
		SceneManager.LoadScene (sceneName);	
	}

	public void openMaps() {
		startPanelAnimator.SetBool("isHidden", true);
		mapsPanel.SetActive(true);
	}
}
