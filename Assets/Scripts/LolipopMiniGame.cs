using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LolipopMiniGame : MonoBehaviour
{

	public int amount;
	public int amountCounted;
	public TMP_Text amountText;
	public TMP_Text timeText;
	public TMP_Text AccuracyText;
	public GameObject loliPopOK;
	public GameObject loliPopNotOK;
	public bool started;
	public int spawnChance;
	public float spawnInterval;
	public Transform spawnPoint;
	private int a;
	private float spawnTimeCounter;
	public int maxTime;
	public GameObject gameOverDisplay;
	public GameObject startGameDisplay;
	private float startTime;
	private bool Win;
	public TMP_Text gameOverTitleText;
	private void Start()
	{
		Debug.Log(PlayerPrefs.GetInt("difficulty"));
		if (PlayerPrefs.GetInt("Endless") == 1)
		{
			maxTime = Mathf.RoundToInt(maxTime * (1f + ((PlayerPrefs.GetInt("difficulty") * 0.5f))));
			
			spawnInterval = spawnInterval / (1 + (PlayerPrefs.GetInt("difficulty") * 0.1f));
		}
	}

	public void startGame()
	{
		startTime = Time.time;
		startGameDisplay.SetActive(false);
		started = true;
	}

	private void Update()
	{
		if (started)
		{
			if (maxTime > (Time.time - startTime))
			{
				timeText.text = (Mathf.CeilToInt(maxTime - (Time.time - startTime)) / 60) + ":" + (Mathf.CeilToInt(maxTime - (Time.time - startTime)) % 60);

				if (spawnTimeCounter < Time.time)
				{
					if (maxTime - (Time.time - startTime) > 4)
					{
						a = Random.Range(0, spawnChance);
						if (a == 0)
						{
							Destroy(Instantiate(loliPopNotOK, spawnPoint), 10);
						}
						else
						{
							Destroy(Instantiate(loliPopOK, spawnPoint), 10);
						}
						//Debug.Log(Mathf.Round((float)(Mathf.Abs(amount - amountCounted);
					}
					spawnTimeCounter = Time.time + spawnInterval;
				}
			}
			else
			{
				if(Mathf.Abs(amount - amountCounted) < 5)
				{
					Win = true;
					gameOverTitleText.text = "Nice Job!";
				}
				else
				{
					Win = false;
					gameOverTitleText.text = "You Falied The Job.";
				}
				timeText.text = "0:00";
				gameOverDisplay.SetActive(true);
				AccuracyText.text = Mathf.Abs(amount - amountCounted).ToString();
				started = false;
				//Time.timeScale = 0;

			}



#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				add();
			}
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				subtract();
			}
#endif
		}
		else
		{
			if (!gameOverDisplay.activeInHierarchy)
			{
				if (Input.GetMouseButtonUp(0))
				{
					startGame();
				}
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

				if (Input.GetKeyUp(KeyCode.Return))
				{
					startGame();
				}
#endif
			}
			else
			{
				if(Input.GetKeyUp(KeyCode.Return))
				{
					next();
				}
			}
		}
	}


	public void next()
	{
		if (Win)
		{
			if (PlayerPrefs.GetInt("Endless") == 1)
			{
				FindObjectOfType<MainMenuManager>().LoadScene("ShoePolishMiniGame");
				//PlayerPrefs.SetInt("difficulty", PlayerPrefs.GetInt("difficulty") + 1);
			}
			else
			{
				FindObjectOfType<MainMenuManager>().LoadScene("CutScene2");
			}
		}
		else
		{

			FindObjectOfType<MainMenuManager>().LoadScene("MainMenu");
		}
	}



	public void subtract()
	{
		amountCounted--;
		amountText.text = amountCounted.ToString();
	}

	public void add()
	{
		amountCounted++;
		amountText.text = amountCounted.ToString();
	}

}
