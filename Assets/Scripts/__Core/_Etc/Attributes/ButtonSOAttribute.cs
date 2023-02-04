using System;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;

[System.AttributeUsage(System.AttributeTargets.Method)]
public class ButtonSOAttribute : PropertyAttribute
{
}

#if UNITY_EDITOR
[CustomEditor(typeof(ScriptableObject), true)]
public class ButtonSOAttributeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var mono = target as ScriptableObject;

        var methods = mono.GetType()
            .GetMembers(BindingFlags.Instance | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                        BindingFlags.NonPublic)
            .Where(o => Attribute.IsDefined(o, typeof(ButtonSOAttribute)));


        bool showSpace = true;

        foreach (var memberInfo in methods)
        {
            if (showSpace)
            {
                GUILayout.Space(12);
                showSpace = false;
            }

            if (GUILayout.Button(memberInfo.Name))
            {
                GUILayout.Space(4);
                var method = memberInfo as MethodInfo;
                method.Invoke(mono, null);
            }
        }
    }
}
#endif