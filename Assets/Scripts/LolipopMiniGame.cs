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
	private int startTime;
	



	public void startGame()
	{
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
				timeText.text = "0:00";
				gameOverDisplay.SetActive(true);
				AccuracyText.text = Mathf.Abs(amount - amountCounted).ToString();
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
			if(Input.GetMouseButtonUp(0))
			{
				startGame();
			}
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

			if(Input.GetKeyDown(KeyCode.Return))
			{
				startGame();
			}
#endif
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
