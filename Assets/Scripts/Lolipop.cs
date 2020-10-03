using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lolipop : MonoBehaviour
{
	





	void CallAdd()
	{
		FindObjectOfType<LolipopMiniGame>().amount++;
	}

	void CallSubstract()
	{
		FindObjectOfType<LolipopMiniGame>().amount--;
	}
}
