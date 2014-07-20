using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class placeDataObjs : dataManager {

	public GameObject dataObject;


	public GameObject dataManager;


	// Use this for initialization
	void Start () 
	{
		finishedLoading += (o) => {
			GetComponent<sceneManager>().allObjs = new List<GameObject>();
			foreach(var p in o)
			{
				DOtoGO(p);

			}
			GetComponent<sceneManager>().maxDataSetIndex = GetComponent<dataManager>().dataPoints[0].dVal.Count - 1;

		};
		
		loadData("./Assets/Data/sampleData2.csv", out dataManager.GetComponent<dataManager>().dataPoints);
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	public void DOtoGO (DataPoint p)
	{
		Debug.Log("DOtoGO - " + p.label);
		GameObject g = GameObject.Instantiate(dataObject, latlongToVector3(p.lat,p.lon,14.0f), Quaternion.identity) as GameObject;
		g.GetComponentInChildren<TextMesh>().text = p.shortLabel;
		g.GetComponent<dataPointLinker>().objData = p;
		g.name = p.label;
		g.transform.parent = this.transform;
		dataManager.GetComponent<sceneManager>().updateVis(g);
		dataManager.GetComponent<sceneManager>().allObjs.Add(g);

	
	}



	private Vector3 latlongToVector3(float latitude, float longitude, float radius)
	{
			float LAT = latitude * Mathf.PI / 180;
			float LON = longitude * Mathf.PI / 180;
			float x = -radius * Mathf.Cos(LAT) * Mathf.Cos(LON);
			float y =  radius * Mathf.Sin(LAT);
			float z =  radius * Mathf.Cos(LAT) * Mathf.Sin(LON);
			return new Vector3(x,y,z);
	}
}
