using UnityEngine;
using System.Collections;
using System;

















public class RayCastSelection : MonoBehaviour 
{

	RayCaster _cast;

	void Start () 
	{
		_cast = new RayCaster (gameObject, 60f);	

		_cast.OnAimedAt += a =>
		{

			Debug.Log("Aimed At: "+a.AimedAt.name);
		};

	}

	void Update () { _cast.Update (); }

}
































[Serializable]
public class RayCastArgs
{
	public GameObject Sender;
	public GameObject AimedAt;
	public float LengthOfTimeAimedAt  = 0f;
	public GameObject LastAimedAt;
	public float TimeAimedAtLastThing = 0f;
	public RaycastHit ThisHit,LastHit;
}

public class RayCaster
{
	public bool Active = true;
	public Action<RayCastArgs> OnAimedAt,OnStillAimingAt,OnAimedAway;
	public float LookDistance = 60f;
	public GameObject RayCastParent;
	RaycastHit _thisHit,_lastHit;
	public RayCaster (GameObject self,float lookDistance)
	{
		RayCastParent = self;
		LookDistance = lookDistance;
	}

	private GameObject _last;


	private GameObject LookingAt { get;  set; }
	private float TimeSpentLooking{ get;  set; }
	float _lastTimeSpentLooking = 0f;

	public void Update()
	{
		if (RayCastParent == null || LookDistance < 0f) return;
		
		LookingAt = getLookingAt ();
		
		
		
		if (LookingAt == null) 
		{

			FireAimedAway();
			_last = null;
			return;
		}
		
		
		if(LookingAt == _last) 
		{
			TimeSpentLooking += Time.deltaTime;
			FireLookingAt();
		}
		else
		{
			FireAimedAt();
			_lastTimeSpentLooking = TimeSpentLooking;
			TimeSpentLooking = 0f;
			FireAimedAway();

		}

	}

	void FireAimedAway () { if(Active && OnAimedAway != null) OnAimedAway(Args); }
	void FireAimedAt () { if(Active && OnAimedAt != null) OnAimedAt(Args); }
	void FireLookingAt () { if(Active && OnAimedAt != null) OnAimedAt(Args); }


	public RayCastArgs Args 
	{
		get 
			{ 
				return new RayCastArgs 
				{ 
					Sender = RayCastParent,
					AimedAt = LookingAt,
					LastAimedAt = _last,
					LengthOfTimeAimedAt = _lastTimeSpentLooking,
					TimeAimedAtLastThing = _lastTimeSpentLooking, 
					LastHit = _lastHit,
					ThisHit = _thisHit
				};
			}
	}

	GameObject getLookingAt()
	{
		_lastHit = _thisHit;
		if(_thisHit.collider != null)
		_last = _thisHit.collider.gameObject;
		_thisHit = new RaycastHit();
		Physics.Raycast(RayCastParent.transform.position, RayCastParent.transform.forward, out _thisHit, LookDistance);
		return _thisHit.collider != null ? _thisHit.collider.gameObject : null;
	}
}


