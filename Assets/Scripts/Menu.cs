using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour 
{
	public GameObject controls;
	public string sceneName;
	public Button controlsGameButton;
	public Button startGameButton;

	private bool isLoading = false;

	void Update()
	{
		if (!EventSystem.current.currentSelectedGameObject == controlsGameButton.gameObject &&
		    !EventSystem.current.currentSelectedGameObject == startGameButton.gameObject)
		{
			startGameButton.Select();
		}
	}

	public void OnControlsButtonClicked()
	{
		if (!controls.activeInHierarchy)
		{
			controls.SetActive(true);
			StartCoroutine(WaitForInputAndClose());
		}
	}

	public void OnStartGameButtonClicked()
	{
		if (!controls.activeInHierarchy && !isLoading)
		{
			startGameButton.GetComponentInChildren<Text>().text = "Loading...";
			SceneManager.LoadSceneAsync(sceneName);
			isLoading = true;
		}
	}

	private IEnumerator WaitForInputAndClose()
	{
		yield return new WaitForSeconds(0.25f);
		while (true)
		{
			if (controls.activeInHierarchy && Input.anyKeyDown)
			{
				controls.SetActive(false);
				yield break;
			}
			yield return null;
		}
	}
}
