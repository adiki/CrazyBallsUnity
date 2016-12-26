using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManagerScript : MonoBehaviour {
	
	public void loadScene(string sceneName) {
		SceneManager.LoadScene (sceneName);	
	}
}
