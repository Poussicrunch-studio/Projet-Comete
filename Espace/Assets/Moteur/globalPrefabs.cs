using UnityEngine;
using System.Collections;
using System.Collections.Generic;   //Required for Dictionary

public class globalPrefabs : ScriptableObject
{
	public static Dictionary<int, Object> objectList = new Dictionary<int, Object>();
	
	public static void LoadAll(string pPath) 
	{
	//	pPath = Application.dataPath + "/" + pPath;
		Debug.Log("Load all prefab from " + pPath);
	
		Object[] ObjectArray = Resources.LoadAll(pPath);
		
		foreach (Object o in ObjectArray) {
			Debug.Log("Load prefab : " + o.name);
			Debug.Log("Load prefab : " + o.name);
			objectList.Add (o.name.GetHashCode(), (Object)o);
		}
			
	}

	public static Object getPrefab(string objName)
	{
		Object obj;
		
		if (objectList.TryGetValue(objName.GetHashCode(), out obj))
			return obj;
		else 
		{
			Debug.Log("Object not found");
			return (null);
		}
	}
}