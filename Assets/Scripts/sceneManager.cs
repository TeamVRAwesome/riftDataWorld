using UnityEngine;
using System.Collections;

public class sceneManager : MonoBehaviour {


	public GameObject dataSphere;
	public enum mode{map, blank};
	public mode currentMode;
	public Material mapMat;
	public Material blankMat;

	// Use this for initialization
	void Start () 
	{
		currentMode = mode.map;
		dataSphere.renderer.material = mapMat;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyUp(KeyCode.Space))
		{
			switchMode();
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
}
