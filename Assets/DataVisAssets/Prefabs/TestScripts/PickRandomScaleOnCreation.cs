using UnityEngine;
using System.Collections;

public class PickRandomScaleOnCreation : MonoBehaviour 
{


	void Start () 
	{
		float scale = Random.Range (0.01f, 2f);	
		transform.localScale = new Vector3 (scale, scale, scale);

	}

	void Update () {
	
	}
}
