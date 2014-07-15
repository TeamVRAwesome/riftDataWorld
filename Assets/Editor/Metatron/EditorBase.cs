using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

public abstract class EditorBase<T> : Editor where T : UnityEngine.Object
{

	public virtual bool Section(string name,Action a,bool current)
	{
		if (a == null) return false;

		var fold = EditorGUILayout.Foldout (current,name);
		if (fold) a ();

		return fold;
	}

	public void Logo(string image)
	{
		Texture2D logo = (Texture2D)Resources.LoadAssetAtPath ("Assets/Resources/Images/"+image, typeof(Texture2D));
		GUILayout.Label (logo);
	}



	public void Hor (Action a)
	{
		if (a == null) 	return;
		EditorGUILayout.BeginHorizontal();
		a ();
		EditorGUILayout.EndHorizontal();
	}

	protected void Ind(int i) { EditorGUI.indentLevel += i; }

	protected void Sep(int x) 
	{
		for (int i = 0; i < x; i++) EditorGUILayout.Separator (); 
	}
	protected float EditRangeLarge(float c,float lowestValue)
	{
		c = ButtonPair (c, 1);
		Hor(()=> 
		    {
			c = ButtonPair (c, 5f);
			c = ButtonPair (c, 20f);
			c = ButtonPair (c, 50f);
		});
		return c < lowestValue ? lowestValue : c;
	}

	protected float EditRangeSmall(float c,float lowestValue)
	{
		c = ButtonPair (c, 0.1f);
		Hor(()=> 
		    {
			c = ButtonPair (c, 0.01f);
			c = ButtonPair (c, 0.5f);
			c = ButtonPair (c, 1f);
		});
		return c < lowestValue ? lowestValue : c;
	}



	
	protected float ButtonPair(float c,float amount) { Hor(()=> { c = MinButton(c,amount); c  = MaxButton(c,amount); }); return c; }	
	float MinButton(float c,float amount) { if (GUILayout.Button ("-" + amount)) c -= amount; return c; }
	float MaxButton(float c,float amount) { if (GUILayout.Button ("+" + amount)) c += amount; return c; }
	

	public bool Btn(string name) { return GUILayout.Button (name, EditorStyles.miniButtonLeft); }


	public void Box(string description,string warning,bool condition)
	{
		if (condition) EditorGUILayout.HelpBox (description, MessageType.Info);
		else           EditorGUILayout.HelpBox (warning, MessageType.Error);
	}

	public void Box(string description) { EditorGUILayout.HelpBox (description, MessageType.Info); }

	public void Lbl(string name,params object[] args) { EditorGUILayout.LabelField (String.Format(name,args)); }

	public void Box(string name,string description) { Lbl (name); Box (description); }
	
	float RoundAndtidy(float f) { return Mathf.Round(f * 100)/100; }

	public Vector3 VectorField (string name, Vector3 val, Vector3 min)
	{
		float x = val.x;
		float y = val.y;
		float z = val.z;
		Ind (2);
		Fld (name);
		Lbl ("All");
		Hor (() => { if (GUILayout.Button ("Identity")) x = y = z = 1; x = y = z = ButtonPair(x,0.1f); });
		Lbl("X");
		Hor (() => { x = ButtonPair(x,0.1f); x = ButtonPair(x,0.01f); });
		Lbl("Y");
		Hor (() => { y = ButtonPair(y,0.1f); y = ButtonPair(y,0.01f); });
		Lbl("Z");
		Hor (() => { z = ButtonPair(z,0.1f); z = ButtonPair(z,0.01f); });
		Ind (-2);
		return new Vector3(RoundAndtidy(x),RoundAndtidy(y),RoundAndtidy(z));
	}


	public float NumberFieldLarge(string field,float fil,float min)
	{
		float ret = fil;
		Fld(field);
		ret = (int)EditRangeLarge(fil,min);
		return ret;
	}

	public float NumberFieldSmall(string field,float fil,float min)
	{
		float ret = fil;
		Fld(field);
		ret = (int)EditRangeSmall(fil,min);
		return ret;
	}



	public void Lst(string name) { EditorList.Show (serializedObject.FindProperty (name)); }
	
	public void Fld(string name,bool noGuiContent = false)
	{
		if(noGuiContent)
		EditorGUILayout.PropertyField(serializedObject.FindProperty(name),GUIContent.none);
		else
		EditorGUILayout.PropertyField(serializedObject.FindProperty(name));
	}

	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();
		Show ((T)target);
		serializedObject.ApplyModifiedProperties ();
	}

	protected abstract void Show(T item);
	




}
