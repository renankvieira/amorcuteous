using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectCounter : MonoBehaviour
{

#if UNITY_EDITOR
	[UnityEditor.MenuItem("GameObject/CountChildren", false, 40)]
	private static void OpenNewWindow()
	{
		GameObject selected = UnityEditor.Selection.activeGameObject;
		Transform[] allChildren = selected.GetComponentsInChildren<Transform>(true);
		Debug.LogFormat(selected, "Transform count: {1}, {0}", allChildren.Length - 1, selected.name);
	}

	[UnityEditor.MenuItem("GameObject/CountComponents", false, 40)]
	private static void OpenNewWindow2()
	{
		GameObject selected = UnityEditor.Selection.activeGameObject;

		Component[] allChildren = selected.GetComponentsInChildren<Component>(true);
		//Transform[] allChildren = selected.GetComponentsInChildren<Transform>(true);
		Debug.LogFormat(selected, "Component count: {1}, {0}", allChildren.Length - 1, selected.name);
	}

#endif
}
