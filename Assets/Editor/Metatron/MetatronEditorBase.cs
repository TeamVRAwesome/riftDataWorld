using UnityEngine;
using System.Collections;
using UnityEditor;

public abstract class MetatronEditorBase<T> : EditorBase<T> where T : UnityEngine.Object 
{
	protected bool AboutSection(string pageName,string PageDescription,string logoPath)
	{

			Logo (logoPath);
		about = Section ("About", () => 
		{
			EditorGUI.indentLevel += 1;
			aboutMeta = Section ("Team Metatron", () => { Box (MetatronConstants.Description);},aboutMeta);
			EditorGUI.indentLevel -= 1;
			Lbl(pageName);
			Box (PageDescription);
		}, about);
		return about;
	}
	
	protected bool about,aboutMeta;
}
