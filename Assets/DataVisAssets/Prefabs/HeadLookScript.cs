using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


public class HeadLookScript : MonoBehaviour 
{
	public GameObject OculusLeftCameraHere;
	public float LookSelectRadius = 2f;
	public float LookRange = 600f;
	public TextMesh label;
	public bool ShowDebugSphere = true;
	RayCaster _cast;
	RayCastArgs _args;
	void Start () 
	{
		_cast = new RayCaster (OculusLeftCameraHere, LookRange);	
		_cast.OnAimedAt += a => 
		{
			_args = a;
			SendLookedAtMessageToAllInLookRadius(a.ThisHit); 
		};
	}

	List<GameObject> lastGuys;
	void SendLookedAtMessageToAllInLookRadius (RaycastHit thisHit)
	{
		Collider[] items = GetAllItemsInSphereRadius (thisHit.point);
		ShowDebugLabel ("Looking At: " + items.Count ());
	
		if (lastGuys != null) lastGuys.ForEach (TellItemItIsNotBeingLookedAt);
		lastGuys = items.Select (i => i.gameObject).ToList ();
		lastGuys.ForEach (TellItemItIsBeingLookedAt);
	}

	Vector3 currentHitPoint;
	Collider[] GetAllItemsInSphereRadius(Vector3 hitPoint) 
	{
		currentHitPoint = hitPoint;

		return Physics.OverlapSphere (hitPoint, LookSelectRadius); 
	}



	void ShowDebugLabel(string message,params object[] args) { 	if (label != null) label.text = string.Format (message,args); 	}

	void TellItemItIsBeingLookedAt(GameObject obj) { obj.SendMessage ("OnLookedAt", _args, SendMessageOptions.DontRequireReceiver); }
	void TellItemItIsNotBeingLookedAt(GameObject obj) { obj.SendMessage ("OnLookedAway", _args, SendMessageOptions.DontRequireReceiver); }

	void Update () { _cast.Update (); }





	void OnDrawGizmos() 
	{
		if (!ShowDebugSphere) return;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(currentHitPoint, LookSelectRadius);

	}

}
