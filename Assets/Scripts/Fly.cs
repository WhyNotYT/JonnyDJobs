using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
	public FlyMiniGame game;
	//public bool feeding;
	public float enterTime;
	public bool fleeing;
	public Vector3 target;
	//public Animator anim;

	private void Start()
	{
		game = FindObjectOfType<FlyMiniGame>();
	}




	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log("entered " + Time.time);
		if (!fleeing)
		{
			enterTime = Time.time;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		//Debug.Log("exited " + Time.time);
		enterTime = -1;
	}
}
