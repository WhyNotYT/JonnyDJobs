using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class textAnimator : MonoBehaviour
{
	public float speed;
	public TMP_Text text;
	float timeCounter;
	string textInitial;



	private void Start()
	{
		text.maxVisibleCharacters = 0;
	}

	private void Update()
	{
		if (timeCounter < Time.time)
		{
			if (text.maxVisibleCharacters < text.text.Length)
			{
				text.maxVisibleCharacters++;
				timeCounter = Time.time + speed;
			}
		}
	}
}

