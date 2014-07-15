using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class dataManager : MonoBehaviour {


	List<CountrySet> allData;
	public List<CountrySet> perCData;

	[System.Serializable]
	public class dataObject
	{

		public string label;
		public float lati;
		public float longi;
		public float sphereRad;
		public GameObject obj;
		private Vector3 position()
		{
			float LAT = lati* Mathf.PI / 180;
			float LON = longi * Mathf.PI / 180;
			float x = -sphereRad * Mathf.Cos(LAT) * Mathf.Cos(LON);
			float y =  sphereRad * Mathf.Sin(LAT);
			float z =  sphereRad * Mathf.Cos(LAT) * Mathf.Sin(LON);
			return new Vector3(x,y,z);
		}
		public float value;
		public string infotext;


	}
	public dataObject[] countryData;

	[System.Serializable]
	class dataLine
	{
		public string label1, label2, label3, freqLable, country, timeframe;
		public float value;

	}

	[System.Serializable]
	public class CountrySet
	{
		public string Country;
		public float Val;
	}

	// Use this for initialization
	void Start ()
	{
		//import csv
		List<dataLine> csvLines;
		bool sload = loadData(out csvLines);
		var a = (from dta in csvLines
		           group dta by new {dta.country, dta.value}
		into g
		select new CountrySet
		{
			Country = g.Key.country.ToString(),
			Val = g.Sum(s => s.value)
		});

		//create array of dataPoints with one for each country
		int totalLines = a.Count();
		countryData = new dataObject[totalLines];
		int countryDataIndexer = 0;
		allData = a.ToList();
		totalDataSets();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool loadData(out List<dataLine> allLines)
	{
		List<dataLine> _lines = new List<dataLine>();
		string fileBuffer = System.IO.File.ReadAllText("./MERCH_EXP.csv");
		string[] lineData = fileBuffer.Split(',');
		Debug.Log (fileBuffer);
		for (int i = 14; i <lineData.Length - 7; i+=7) 
		{
			float f;
			float.TryParse(lineData[i+6], out f);
			dataLine thisLine =	new dataLine
			{
				label1 = lineData[i], 
				label2 = lineData[i+1],
				label3 = lineData[i+2],
				freqLable = lineData[i+3],
				country = lineData[i+4],
				timeframe = lineData[i+5],
				value = f
			};
			_lines.Add(thisLine);
		}
		allLines = _lines;
		return true;
	}

	void totalDataSets()
	{
		string currentCountry = string.Empty;
		int lastCCount = 0;
		bool firstPass = true;
		foreach(CountrySet x in allData)
		{
			if(currentCountry != x.Country)
			{
				if(!firstPass)
					perCData.Last().Val = perCData.Last().Val/lastCCount; //avg
				Debug.Log("found dup");
				perCData.Add(x);
				currentCountry = x.Country;
				lastCCount = 1;
			}
			else
			{
				perCData.Last().Val += x.Val;
				lastCCount++;
			}
		}
	}

}
