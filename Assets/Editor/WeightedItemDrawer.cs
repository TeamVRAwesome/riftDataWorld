using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(WeightedItem))]
public class WeightedItemDrawer : PropertyDrawer 
{
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		int oldIndentLevel = EditorGUI.indentLevel;




		label = EditorGUI.BeginProperty(position, label, property);
		Rect r = EditorGUI.PrefixLabel(position, label);
		r.width *= 0.5f;
		EditorGUI.indentLevel = 0;
		EditorGUI.PropertyField(r, property.FindPropertyRelative("Item"), GUIContent.none);

		r.x += r.width;
		r.width /= 0.1f;
		r.x += 10;
		EditorGUI.LabelField (r, "%");
		r.x += 20;
		r.width = 40f;
		EditorGUI.PropertyField(r, property.FindPropertyRelative ("Weight"), GUIContent.none);//, 0,100);
		//r.x += 10;
		//if (GUILayout.Button ("+")) {}
		//r.x += 10;
		//if (GUILayout.Button ("-")) {}

		EditorGUI.EndProperty ();
		EditorGUI.indentLevel = oldIndentLevel;
	}


}
