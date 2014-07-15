using UnityEngine;
using System.Collections;

public class TextmeshUpdate : MonoBehaviour {

	public TextMesh countryMeshCollider;

	public void countryNameSet(string countryName)
	{
		countryMeshCollider.text =  countryName;
	}

}