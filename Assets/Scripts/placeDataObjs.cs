using UnityEngine;
using System.Collections;

public class placeDataObjs : dataManager {

	public GameObject dataObject;

	public GameObject[] allObjs;


	// Use this for initialization
	void Start () 
	{
		finishedLoading += (o) => {
			foreach(var p in o)
			{
				DOtoGO(p);
				Debug.Log ("Called for " + p.label);
			}
		};
		
		loadData("./Assets/Data/sampleData.csv", out dataPoints);
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	public void DOtoGO (DataPoint p)
	{
		GameObject g = GameObject.Instantiate(dataObject, latlongToVector3(p.lat,p.lon,14.0f), Quaternion.identity) as GameObject;
		g.GetComponentInChildren<TextMesh>().text = p.shortLabel;//string.Format("{0} \n{1} - {2}", p.label, p.dLabel[0].ToString(), p.dVal[0].ToString());
		if(p.dVal[0] > 500000.0f)
		{
			g.renderer.material.color = Color.red;
			
			g.transform.localScale = new Vector3(4f,4f,4f);
		}
		else if(p.dVal[0] > 50000.0f)
		{
			g.renderer.material.color = Color.yellow;
			
			g.transform.localScale = new Vector3(2f,2f,2f);
		}
		else if (p.dVal[0] > 3000.0f)
		{
			g.renderer.material.color = Color.green;
			
			g.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
		}
		else
		{
			g.renderer.material.color = Color.grey;
			
			g.transform.localScale = new Vector3(1f,1f,1f);
		}


	
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
