using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

		Scene currentScene;
		Scene nextScene;
		int nextSceneIndex;

		// Use this for initialization
		void Start () {
			SceneManager.LoadScene(1);
//			currentScene = SceneManager.GetSceneByBuildIndex (1);
//			Debug.Log (currentScene);
//			SceneManager.SetActiveScene (currentScene);
//			nextSceneIndex = 1;
//			nextScene = SceneManager.GetSceneByBuildIndex (nextSceneIndex);
		}

		// Update is called once per frame
		void Update () {

		} 

		public void ChangeLevel() {
			SceneManager.SetActiveScene (nextScene);
		}

		public void SetNextScene(int i) {
			nextSceneIndex = i;
	}
}