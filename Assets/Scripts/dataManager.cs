using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class dataManager : MonoBehaviour {

	public System.Action<List<DataPoint>> finishedLoading;
 

	public List<DataPoint> dataPoints;

	[System.Serializable]
	public class CountrySet
	{
		public string Country;
		public float Val;
	}



	[System.Serializable]
	public class DataPoint
	{
		public string shortLabel;
		public string label;
		public float lat;
		public float lon;
		public List<string> dLabel;
		public List<float> dVal;

	}

	// Use this for initialization
	void Start ()
	{



	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void placePoints()
	{


	}

	public bool loadData(string filePath, out List<DataPoint> returnList)
	{
		string[] fileBuffer = System.IO.File.ReadAllLines(filePath);
		Debug.Log (fileBuffer);
		int lineCount = fileBuffer.Length;
		string[] headerLine = fileBuffer[0].Split(',');
		int lineLenght = headerLine.Length;
		List<DataPoint> tmpList = new List<DataPoint>();
		for(int i = 1; i < lineCount; i++) //per line
		{
			string[] lineString = fileBuffer[i].Split(',');
			float lati;
			float.TryParse(lineString[2], out lati);
			float loni;
			float.TryParse(lineString[3], out loni);
			List<string> labels = new List<string>();
			List<float> objs = new List<float>();

			for(int v = 4; v < lineLenght; v++) //
			{
				labels.Add (headerLine[v]);
				float o;
				float.TryParse(lineString[v], out o);
				objs.Add(o);
			}
			DataPoint thisPoint = new DataPoint
			{
				label = lineString[0],
				shortLabel  = lineString[1],
				lat = lati,
				lon = loni,
				dLabel = labels,
				dVal = objs
			};
			tmpList.Add(thisPoint);
		}
		returnList = tmpList;
		if(finishedLoading != null)
		{
			finishedLoading(returnList);
		}
		return true;
	}








}
