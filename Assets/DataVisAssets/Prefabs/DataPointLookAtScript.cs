using UnityEngine;
using System.Collections;

public class DataPointLookAtScript : MonoBehaviour 
{
	
	TextMesh label;
	bool isBeingLookedAt = false;
		
	void Start() { label = GetComponentInChildren<TextMesh> (); }
	
	void OnLookedAt(RayCastArgs args) { isBeingLookedAt = true; }
	void OnLookedAway() { isBeingLookedAt = false; }

	void OnPointedAt(RayCastArgs args)
	{
		gameObject.transform.localScale = new Vector3 (3, 3, 3);
	}

	void OnPointedAway(RayCastArgs args)
	{
		gameObject.transform.localScale = new Vector3 (1, 1, 1);
	}

	void Update()
	{


if (isBeingLookedAt)
		{
			
			GetComponent<MeshRenderer>().enabled = true;
			label.gameObject.SetActive(true);
		}
		
		else
		{
			
			GetComponent<MeshRenderer>().enabled = false;
			label.gameObject.SetActive(false);
		}

	}


}
