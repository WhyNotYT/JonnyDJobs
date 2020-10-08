using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FlyMiniGame : MonoBehaviour
{
	public int flyAmount;
	public GameObject flyPrefab;
	public Fly[] flies;
	public Transform SpawnPoint;
	public bool started;
	public TMP_Text timeText;
	public float maxTime;
	public float startTime;
	public Animator anim;
	public Transform hand;
	public float fleeRange;
	public float hp;
	public float maxHp;
	public float damageStartTime;
	public float damageInterval;
	public float damageAmount;
	public float FlySpeed;
	public Transform[] foods;
	private float damageCounter;
	public float stoppingDistance;
	public Image healthBar;
	public GameObject gameOverObject;
	public GameObject tutorialObject;
	public bool Win;
	public TMP_Text gameOverTitleText;
	public AudioSource flyNoise;
	public AudioSource handFlap;
	private void Start()
	{
		if (PlayerPrefs.GetInt("Endless") == 1)
		{
			maxTime = Mathf.RoundToInt(maxTime * (1f + ((PlayerPrefs.GetInt("difficulty") * 0.5f))));
			flyAmount = Mathf.RoundToInt(flyAmount * (1f + PlayerPrefs.GetInt("difficulty") * 0.5f));
		}
		maxHp = hp;
		flies = new Fly[flyAmount];
		for (int i = 0; i < flyAmount; i++)
		{
			flies[i] = Instantiate(flyPrefab, SpawnPoint.transform.position + new Vector3(Random.Range(-10, 10), 0, 0), Quaternion.identity ).GetComponent<Fly>();
		}
	}
	public void startGame()
	{
		startTime = Time.time;
		tutorialObject.SetActive(false);
		started = true;
		flyNoise.Play();
	}

	public void gameOver()
	{
		flyNoise.Stop();
		if(hp > 0)
		{
			Win = true;
			gameOverTitleText.text = "Nice Job!";
		}
		else
		{
			gameOverTitleText.text = "You Failed The Job.";
			Win = false;
		}


		gameOverObject.SetActive(true);
		timeText.text = "0:00";
		started = false;

		hand.position = new Vector3(0, 100, -100);
	}






	public void next()
	{
		if (Win)
		{
			if (PlayerPrefs.GetInt("Endless") == 1)
			{
				FindObjectOfType<MainMenuManager>().LoadScene("LoliPopMiniGame");
				PlayerPrefs.SetInt("difficulty", PlayerPrefs.GetInt("difficulty") + 1);
			}
			else
			{
				FindObjectOfType<MainMenuManager>().LoadScene("CutScene4");
			}
		}
		else
		{

			FindObjectOfType<MainMenuManager>().LoadScene("MainMenu");
		}
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{

			FindObjectOfType<MainMenuManager>().LoadScene("MainMenu");
		}

		if (started)
		{
			if (maxTime > (Time.time - startTime))
			{
				timeText.text = (Mathf.CeilToInt(maxTime - (Time.time - startTime)) / 60) + ":" + (Mathf.CeilToInt(maxTime - (Time.time - startTime)) % 60);

				healthBar.fillAmount = ((hp) / maxHp);

				if(hp < 0)
				{
					gameOver();
				}
				if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Flap"))
				{
					hand.position = new Vector3(0, 100, -100);
					
					if (Input.GetMouseButtonDown(0))
					{
						//Debug.Log("MouseDown");
						anim.Play("Flap");
						hand.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 5);
						handFlap.Play();
					}
				}
				else
				{

				}
				for (int i = 0; i < flyAmount; i++)
				{
					if(!flies[i].fleeing)
					{
						if((flies[i].transform.position - hand.transform.position).sqrMagnitude < fleeRange)
						{
							flyflee(i);
						}
						if(flies[i].target == Vector3.zero)
						{
							retargetFly(i);
						}

						//Debug.Log(flies[i].enterTime);
					}

					if(flies[i].target != null)
					{
						//Debug.Log((flies[i].transform.position - flies[i].target).sqrMagnitude);
						if (((Vector2)flies[i].transform.position - (Vector2)flies[i].target).sqrMagnitude > stoppingDistance)
						{
							flies[i].transform.position = Vector2.MoveTowards(flies[i].transform.position, flies[i].target, FlySpeed * Time.deltaTime);
							Vector3 a = flies[i].target - flies[i].transform.position;
							a.z = 0;
							flies[i].transform.up = a;
						}
						else
						{
							if(flies[i].fleeing)
							{
								flies[i].fleeing = false;
								flies[i].target = Vector3.zero;
							}

							if (flies[i].enterTime != -1)
							{
								if (Time.time - flies[i].enterTime > damageStartTime)
								{
									if (damageCounter < Time.time)
									{
										hp -= damageAmount;
										damageCounter = damageInterval + Time.time;
									}
								}
							}
						}
					}
				}

			}
			else
			{
				gameOver();
				
			}
		}
		else
		{
			if (Input.GetKeyUp(KeyCode.Return))
			{
				if (!gameOverObject.activeInHierarchy)
				{
					startGame();

				}
				else
				{
					next();
				}
			}
		}
	}

	void retargetFly(int i)
	{
		int a = Random.Range(0, foods.Length);

		flies[i].target = foods[a].position;
	}


	void flyflee(int i)
	{
		Debug.Log("Fleeing");
		flies[i].fleeing = true;
		flies[i].target = SpawnPoint.position + new Vector3(Random.Range(-10, 10), 0, 0);
		flies[i].enterTime = -1;
	}
}