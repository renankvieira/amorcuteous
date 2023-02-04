using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : SingletonOfType<PersistentData>
{
    override protected void Awake()
    {
        base.Awake();
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public bool runOnValidateMethod = false;
    void OnValidate()
    {
        bool run = false;
        if (!run)
            return;

        if (!runOnValidateMethod)
            return;

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        //}
    }

}
