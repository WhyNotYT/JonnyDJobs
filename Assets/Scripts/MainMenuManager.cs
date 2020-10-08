using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenuManager : MonoBehaviour
{
	private string sceneToLoad;
	private string playerPref;
	public GameObject loadTransition;
	private void Load()
	{
		SceneManager.LoadSceneAsync(sceneToLoad);
	}

	public void quit()
	{
		Application.Quit();
	}

	public void LoadScene(string name)
	{
		sceneToLoad = name;
		loadTransition.SetActive(true);
		Invoke("Load", 0.5f);
	}

	public void setPlayerPrefStr(string name)
	{
		playerPref = name;
	}

	public void setPlayerPrefValue(int value)
	{
		PlayerPrefs.SetInt(playerPref, value);
	}
	public void openURL(string url)
	{
		Application.OpenURL(url);
	}
}
