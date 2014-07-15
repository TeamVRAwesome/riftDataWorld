using UnityEngine;
using System.Collections;

public class HandPointScript : MonoBehaviour 
{
	public GameObject LeftHand,RightHand;
	RayCaster _leftCaster,_rightCaster;
	RayCastArgs _args;

	GameObject lastLeft,lastRight;
	void Start()
	{
		_leftCaster = new RayCaster (LeftHand, 100f);
		_rightCaster = new RayCaster (LeftHand, 100f);

		_leftCaster.OnAimedAt += a => 
		{
			lastLeft = a.AimedAt;
			Debug.Log("Left hand pointed");
			_args = a;
			a.AimedAt.SendMessage ("OnPointedAt", _args, SendMessageOptions.DontRequireReceiver);
		};

		_rightCaster.OnAimedAt += a => 
		{
			lastRight = a.AimedAt;
			_args = a;
			a.AimedAt.SendMessage ("OnPointedAt", _args, SendMessageOptions.DontRequireReceiver);
		};

		_leftCaster.OnAimedAway += a => 
		{
			Debug.Log(lastLeft);

	 		lastLeft.SendMessage ("OnPointedAway", _args, SendMessageOptions.DontRequireReceiver);
		//	_args.AimedAt.SendMessage ("OnPointedAway", _args, SendMessageOptions.DontRequireReceiver);
		};
		
		_rightCaster.OnAimedAway += a => 
		{
			Debug.Log(lastRight);

			lastRight.SendMessage ("OnPointedAway", _args, SendMessageOptions.DontRequireReceiver);
			//_args.AimedAt.SendMessage ("OnPointedAway", _args, SendMessageOptions.DontRequireReceiver);
		};

	}

	void Update()
	{
		_leftCaster.Update ();
		_rightCaster.Update ();
	}


}
