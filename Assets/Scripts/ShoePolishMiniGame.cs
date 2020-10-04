using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShoePolishMiniGame : MonoBehaviour
{
	public GameObject tutorial, gameOver;
	public TMP_Text timeText, targetText, doneText, missedText;
	public int doneAmount;
	public Camera cam;
	public GameObject manShoe;
	public GameObject womanShoe;
	public GameObject sandles;
	public bool cottonSelected;
	public Transform spawnPoint;
	public Shoe movingShoe;
	public Vector3 offset;
	public GameObject cotton;
	public Vector3 mouseWorldPoint;
	public Shoe currentShoe;
	public bool cottonColliding;
	public float cleanMultiplier;
	public ParticleSystem dryParticle;
	public int maxTime;
	public float startTime;
	public bool started;
	public int target;
	public TMP_Text gameOverTitle;

	private bool Win;
	private Vector3 lastPoint;


	private void over()
	{

		if(maxTime > (Time.time - startTime))
		{
			Win = true;
		}
		else
		{
			if(target - doneAmount == 0)
			{
				Win = true;
			}
		}

		if(Win)
		{
			gameOverTitle.text = "Nice JOB!";
		}
		else
		{
			gameOverTitle.text = "You Failed The JOB.";
		}

		timeText.text = "0:00";
		missedText.text = (target - doneAmount).ToString();
		gameOver.SetActive(true);
		started = false;
	}





	public void next()
	{
		if (Win)
		{
			if (PlayerPrefs.GetInt("Endless") == 1)
			{
				FindObjectOfType<MainMenuManager>().LoadScene("FlyMiniGame");
			}
			else
			{

				FindObjectOfType<MainMenuManager>().LoadScene("CutScene3");
			}
		}
		else
		{

			FindObjectOfType<MainMenuManager>().LoadScene("MainMenu");
		}
	}





	private void Update()
	{
		if (started)
		{
			if (maxTime > (Time.time - startTime))
			{
				timeText.text = (Mathf.CeilToInt(maxTime - (Time.time - startTime)) / 60) + ":" + (Mathf.CeilToInt(maxTime - (Time.time - startTime)) % 60);

			}
			else
			{
				over();
			}

			mouseWorldPoint = cam.ScreenToWorldPoint(Input.mousePosition);


			if (!cottonSelected)
			{
				if (movingShoe != null)
				{
					movingShoe.transform.position = mouseWorldPoint + offset;

				}
			}
			else
			{
				if (Input.GetMouseButton(0))
				{
					cotton.transform.position = mouseWorldPoint + new Vector3(0, 0, 5);

					if (currentShoe != null)
					{
						if (cottonColliding)
						{
							Debug.Log("cleaning");
							currentShoe.clean((mouseWorldPoint - lastPoint).sqrMagnitude * cleanMultiplier);
							lastPoint = mouseWorldPoint;
						}
					}
				}
				else
				{
					cotton.transform.position = new Vector3(0, 0, -100);
					if (currentShoe != null)
					{
						lastPoint = currentShoe.transform.position;
					}
				}
			}
		}
		else
		{
			if (!gameOver.activeInHierarchy)
			{
				if (Input.GetKeyUp(KeyCode.Return))
				{
					startGame();
				}
			}
			else
			{

				if (Input.GetKeyUp(KeyCode.Return))
				{
					next();
				}
			}
		}
	}

	public void startGame()
	{
		started = true;
		tutorial.SetActive(false);
		startTime = Time.time;
	}

	private void Start()
	{

		if (PlayerPrefs.GetInt("Endless") == 1)
		{
			maxTime = Mathf.RoundToInt(maxTime * (1f + ((PlayerPrefs.GetInt("difficulty") * 0.5f))));
			target = Mathf.RoundToInt(target * (1f + (PlayerPrefs.GetInt("difficulty") * 0.5f)));
		}
		targetText.text = (target + 1).ToString();
		spawnShoe();



	}
	public void toggleCotton()
	{
		cottonColliding = false;
		lastPoint = Vector3.zero;
		cottonSelected = !cottonSelected;
		if(!cottonSelected)
		{
			cotton.transform.position = new Vector3(1000, 0, -100);
		}
	}

	public void dry()
	{
		//spawnShoe();
		dryParticle.Play();
	}
	void spawnShoe()
	{
		int a = Random.Range(0, 3);

		if(a == 0)
		{
			Instantiate(manShoe, spawnPoint.transform.position, Quaternion.identity);
		}
		else if (a == 1)
		{
			Instantiate(womanShoe, spawnPoint.transform.position, Quaternion.identity);
		}
		else
		{
			Instantiate(sandles, spawnPoint.transform.position, Quaternion.identity);
		}
	}
	public void done(Shoe shoe)
	{
		if (doneAmount < target)
		{
			Destroy(shoe.gameObject, 5);
			spawnShoe();
			movingShoe = null;
			currentShoe = null;
			doneAmount++;
			doneText.text = doneAmount.ToString();
		}
		else
		{
			over();
		}
		
	}
}
