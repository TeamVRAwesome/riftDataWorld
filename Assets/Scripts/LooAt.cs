using UnityEngine;
using System.Collections;

public class LooAt : MonoBehaviour {

	public GameObject headPos;
	private Transform myTrasform;
	// Use this for initialization
	void Start () {
		headPos = GameObject.FindGameObjectWithTag("Player");
		myTrasform = transform;
		myTrasform.rotation = Quaternion.LookRotation(transform.position - headPos.transform.position);

	}
	
	// Update is called once per frame
	void Update () {
		myTrasform.LookAt(headPos.transform.position);
	}
}
