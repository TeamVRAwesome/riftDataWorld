using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;

[CustomEditor ( typeof(SphereGenerator)), CanEditMultipleObjects]
public class SphereGeneratorEditor  : MetatronEditorBase<SphereGenerator>  
{
	protected override void Show (SphereGenerator item)
	{
		about = AboutSection ("Sphere Generator", SphereManagerDescription, "Sphere.jpg");

		generation = Section ("Items To Generate", () => 
		{
			Box (info_generation);
			Ind(2);

			Lst("Items");
			Ind (-2);
		},generation);

		rendering = Section ("Amount and Size", () => 
		{
			Sep (3);
			item.NumberOfItemsToGenerate = (int)NumberFieldLarge("NumberOfItemsToGenerate",item.NumberOfItemsToGenerate,0);
			Sep (3);
			item.ItemSpacing = NumberFieldLarge("ItemSpacing", item.ItemSpacing,0); 
			Sep (3);
			item.Scaling = VectorField("Scaling",item.Scaling,new Vector3(0.1f,0.1f,0.1f));
		},rendering);

		bool anyItem = item.Items.Any(i=>i != null && i.Item != null);
		Box (err,warn,anyItem);

		if(anyItem && item.NumberOfItemsToGenerate > 0 && Btn("Generate"))item.Generate ();
		if(item.ItemsGenerated.Count > 0 )
			if(Btn("Clear")) item.DestroyAll();

		Sep (3);
	}
	
	bool rendering = false;
	bool generation = true;

	string warn = "Please Select An Item To Generate and a positive amount to Render";
	string err = "If You Don't Click This Button, The Item will be generated at Runtime Instead.";
	string info_generation = "Select a number of game objects to disperse throughout the sphere. Use the buttons provided to add/remove/clone items. Also optionally alter the percentage fields to fine tune probability of occurance";
	string SphereManagerDescription = "The Sphere Generator Distributes GameObjects along the Points of a Sphere, allowing for customization in weighted probability of items";

}
