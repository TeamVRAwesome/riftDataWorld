using UnityEngine;
using System.Collections;
using System.IO;


public class CoordinateFinder : MonoBehaviour {
	public GameObject myObj;
	public TextmeshUpdate countryTextLink;

	/*[System.Serializable]
	public struct dataPoint
	{
		public string countryName;
		public int value;
		public GameObject dataObj;

	}
	public dataPoint[] coDat = new dataPoint[10];*/


	
	private Vector3 latlongToVector3(float latitude, float longitude, float radius)
	{
		float LAT = latitude * Mathf.PI / 180;
		float LON = longitude * Mathf.PI / 180;
		float x = -radius * Mathf.Cos(LAT) * Mathf.Cos(LON);
		float y =  radius * Mathf.Sin(LAT);
		float z =  radius * Mathf.Cos(LAT) * Mathf.Sin(LON);
		return new Vector3(x,y,z);
	}

	void Start() {
		float radius = GetComponent<SphereCollider>().radius;

		string[] values = File.ReadAllText(Application.dataPath + "/country_latlon.txt").Split(null);

		string countryCode = null;
		float latitude = 0.0f, longitude = 0.0f;
		
		bool latitudeAdded = false;

		foreach(var eachvalue in values)
		{
			float valueToAdd;

			if( float.TryParse(eachvalue, out valueToAdd) )
			{
				if(!latitudeAdded)
				{
					latitude = valueToAdd;
					latitudeAdded = true;

				}
				else
				{
					longitude = valueToAdd;
					latitudeAdded = false;

					Vector3 countryPosition = latlongToVector3(latitude, longitude, radius);
					GameObject instancedObj =(GameObject)Instantiate(myObj,countryPosition, new Quaternion(0,0,0,0));
					instancedObj.GetComponent<TextmeshUpdate>().countryNameSet(countryCode);
				}
			}
			else
			{
				countryCode = eachvalue;

			}
		}
	}
}



