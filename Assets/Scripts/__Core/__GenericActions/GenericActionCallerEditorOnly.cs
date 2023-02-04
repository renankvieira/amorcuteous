using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericActionCallerEditorOnly : GenericActionCaller
{
    [Header("EditorOnly")]
    public bool logWarning = true;

    public override bool RunExtraTests()
    {
        bool isEditor = false;
#if UNITY_EDITOR
        if (logWarning)
            Debug.Log("[GACEO] Editor only script running.", gameObject);
        isEditor = true;
#endif
        if (!isEditor)
            this.enabled = false;

        return isEditor;
    }
}