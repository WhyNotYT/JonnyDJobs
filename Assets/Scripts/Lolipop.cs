using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lolipop : MonoBehaviour
{
	




	void PlayAudio()
	{
		this.GetComponent<AudioSource>().Play();
	}
	void CallAdd()
	{
		FindObjectOfType<LolipopMiniGame>().amount++;
	}

	void CallSubstract()
	{
		FindObjectOfType<LolipopMiniGame>().amount--;
	}
}
