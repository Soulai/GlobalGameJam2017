using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour 
{
	public string sceneName;
	public Button startGameButton;

	private bool isLoading = false;

	void Update()
	{
		startGameButton.Select();
	}

	public void OnStartGameButtonClicked()
	{
		if (!isLoading)
		{
			startGameButton.GetComponentInChildren<Text>().text = "Loading...";
			SceneManager.LoadSceneAsync(sceneName);
			isLoading = true;
		}
	}
}
