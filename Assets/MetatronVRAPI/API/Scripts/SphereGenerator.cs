using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using System;


public class SphereGenerator : MonoBehaviour 
{

	public WeightedItem[] Items = new WeightedItem[1];
	public int NumberOfItemsToGenerate = 100;
	public float ItemSpacing = 4f;
	public Vector3 Scaling = new Vector3(1,1,1);

	GameObject SelectObject() { return Rand (); }


	void Start()
	{
		if (ItemsGenerated.Count == 0)
						Generate ();

	}

	public void DestroyAll()
	{
		ItemsGenerated.ForEach (i => { DestroyImmediate(i); });
		ItemsGenerated.Clear ();
	}

	public List<GameObject> ItemsGenerated = new List<GameObject>();
	public void Generate()
	{
		DestroyAll ();
		if (Items.Count () == 0)
						return;
		//float scaleXTotal = Items.Sum ( s => s.Item.transform.localScale.x );
		//float avgScale = scaleXTotal / Items.Count ();
		
		List<Vector3> pts = UniformPointsOnSphere ((float)NumberOfItemsToGenerate, ItemSpacing );
		
		
		foreach (Vector3 spherePoint in pts)
		{
			GameObject ob = SelectObject();

			ob.transform.localScale = Scaling;
			Vector3 v = transform.position + spherePoint;
			
			GameObject bubble = Instantiate(ob,v, Quaternion.identity) as GameObject;
			ItemsGenerated.Add(bubble);
			bubble.name = "SphereItem";
			try{
			bubble.transform.parent = gameObject.transform;
			}catch{}
		}
	}


	List<Vector3> PointsOnSphere(float n, Vector3 rotation)
	{
		List<Vector3> upts = new List<Vector3>();
		float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
		float off = 2.0f / n;
		float x,y,z,r,phi;
		
		for (var k = 0; k < n; k++)
		{
			y = k * off - 1 + (off / 2);
			r = Mathf.Sqrt(1 - y * y);
			phi = (k * inc) + rotation.magnitude;
			x = Mathf.Cos(phi) * r;
			z = Mathf.Sin(phi) * r;
			
			upts.Add(new Vector3(x, y, z));
		}
		return upts;
	}


	List<Vector3> UniformPointsOnSphere(float N, float scale) {
		var points = new List<Vector3>();
		var i = Mathf.PI * (3 - Mathf.Sqrt(5));
		var o = 2 / N;
		for(var k=0; k<N; k++) {
			var y = k * o - 1 + (o / 2);
			var r = Mathf.Sqrt(1 - y*y);
			var phi = k * i;
			points.Add(new Vector3(Mathf.Cos(phi)*r, y, Mathf.Sin(phi)*r) * scale);
		}
		return points.ToList();
	}


	public GameObject Rand()
	{
		int totalWeight = 0; 
		WeightedItem selected = null; 
		foreach (var data in Items)
		{
			int weight = data.Weight; 
			int r = (UnityEngine.Random.Range(0, totalWeight + weight));
			if (r >= totalWeight)
				selected = data; 
			totalWeight += weight; 
		}
		
		return selected.Item; 
	}



}



