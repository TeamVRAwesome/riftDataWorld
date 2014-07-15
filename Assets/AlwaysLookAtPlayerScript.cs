using UnityEngine;
using System.Collections;
using System.Linq;

public class AlwaysLookAtPlayerScript : MonoBehaviour 
{

	GameObject Player;

	void Start()
	{
		Player = GameObject.FindGameObjectsWithTag("GamePlayer").FirstOrDefault();
	}

	void Update () 
	{


		if (Player == null) return;

		transform.rotation = Quaternion.LookRotation(transform.position - Player.transform.position);


	}
}
