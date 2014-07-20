using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class sceneManager : dataManager {


	public GameObject dataSphere;
	public GameObject dataManager;
	public enum mode{map, blank};
	public mode currentMode;
	public Material mapMat;
	public Material blankMat;

	public Material[] dataMats = new Material[4];

	public List<GameObject> allObjs;

	public int dataSetIndex;
	public int maxDataSetIndex = 0;



	// Use this for initialization
	void Start () 
	{
		dataSetIndex = 0;
		currentMode = mode.map;
		dataSphere.renderer.material = mapMat;


		finishedLoading += (o) => {
			Debug.Log ("test1");

		};
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		int z = dataSetIndex;
		if(Input.GetKeyUp(KeyCode.Space))
		{
			switchMode();
		}
		if(Input.GetKeyUp(KeyCode.UpArrow))
		{
			if(dataSetIndex < maxDataSetIndex)
			{
				dataSetIndex++;
			}
			else
			{
				dataSetIndex = 0;
			}

		}
		if(Input.GetKeyUp(KeyCode.DownArrow))
		{

			if(dataSetIndex > 0)
			{
				dataSetIndex--;
			}
			else
			{
				dataSetIndex = maxDataSetIndex;
			}

		}
		if(z != dataSetIndex)
		{
			Debug.Log("Changed dataset");
			updateObjsInList(allObjs);
		}
	
	}

	void updateObjsInList(List<GameObject> toUpdate)
	{
		Debug.Log (toUpdate.Count);
		foreach(GameObject g in toUpdate)
		{
			updateVis(g);
		}
	}

	void switchMode()
	{
		switch(currentMode)
		{
		case mode.map:
			currentMode = mode.blank;
			dataSphere.renderer.material = blankMat;
			break;
		case mode.blank:
			currentMode = mode.map;
			dataSphere.renderer.material = mapMat;
			break;
		}
	}

	public void updateVis(GameObject g)
	{
		if(g == null)
			return;
		Debug.Log("updateVis - " + g.GetComponent<dataPointLinker>().objData.label);
		DataPoint p = g.GetComponent<dataPointLinker>().objData;
		var t = g.GetComponentInChildren<TextMesh>();
		if(t != null)
			t.text = p.shortLabel;
		if(p.dVal[dataSetIndex] > 500000.0f)
		{
			g.renderer.material = dataMats[0];
			
			g.transform.localScale = new Vector3(4f,4f,4f);
		}
		else if(p.dVal[dataSetIndex] > 50000.0f)
		{
			g.renderer.material = dataMats[1];
			
			g.transform.localScale = new Vector3(2f,2f,2f);
		}
		else if (p.dVal[dataSetIndex] > 3000.0f)
		{
			g.renderer.material = dataMats[2];
			
			g.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
		}
		else
		{
			g.renderer.material  = dataMats[3];
			
			g.transform.localScale = new Vector3(1f,1f,1f);
		}
		
	}
}
