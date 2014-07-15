using UnityEngine;
using System.Collections;
using UnityEditor;

public class EditorList 
{



	public static void Show(SerializedProperty list, bool showListSize = true)
	{
		if (!list.isArray) {
			EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
			return;
		}
		EditorGUILayout.PropertyField(list);
		EditorGUI.indentLevel += 1;
		if (list.isExpanded) 
		{
			SerializedProperty size = list.FindPropertyRelative("Array.size");
			if (size.hasMultipleDifferentValues) EditorGUILayout.HelpBox("Cannot edit size when multi-editing. (unless lists have same number of elements)", MessageType.Info);
			else BuildSizeField(list);
			



			ShowListElements(list);
		}
		EditorGUI.indentLevel -= 1;
	}



	private static void ShowListElements(SerializedProperty list)
	{
		for (int i = 0; i < list.arraySize; i++) 
		{
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.PropertyField (list.GetArrayElementAtIndex (i),GUIContent.none);
			var size = list.FindPropertyRelative("Array.size");
			if (!size.hasMultipleDifferentValues)
			ShowButtons(list,i); 
			EditorGUILayout.EndHorizontal ();
		}
	}


	private static void BuildSizeField(SerializedProperty list)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
		if (GUILayout.Button ("+", EditorStyles.miniButtonLeft, miniButtonWidth,miniButtonHeight)) list.InsertArrayElementAtIndex(list.arraySize);

		

		EditorGUILayout.EndHorizontal();
	}
	private static GUILayoutOption miniButtonWidth = GUILayout.Width(30f);
	private static GUILayoutOption miniButtonHeight = GUILayout.Height(20f);
	private static void ShowButtons (SerializedProperty list, int index) {
		EditorGUILayout.BeginHorizontal ();
		if(GUILayout.Button(moveButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth,miniButtonHeight))list.MoveArrayElement(index, index + 1);
		if(GUILayout.Button(duplicateButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth,miniButtonHeight))list.InsertArrayElementAtIndex(index);
		if (GUILayout.Button (deleteButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth, miniButtonHeight)) 
		{
			int oldSize = list.arraySize;
			list.DeleteArrayElementAtIndex(index);
			if (list.arraySize == oldSize) list.DeleteArrayElementAtIndex(index);	
		}
		EditorGUILayout.EndHorizontal ();
	}

	private static GUIContent
		moveButtonContent = new GUIContent("\u21b4", "move down"),
		duplicateButtonContent = new GUIContent("+", "duplicate"),
		deleteButtonContent = new GUIContent("-", "delete");
}
