using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class WebAssemblyOption
{
    //[MenuItem("Web Assembly/Enable Threads Support")]
    //static void EnableThreads()
    //{
    //    PlayerSettings.WebGL.threadsSupport = true;
    //    ToggleActionValidate();
    //}

    //[MenuItem("Web Assembly/Disable Threads Support")]
    //static void DisableThreads()
    //{
    //    PlayerSettings.WebGL.threadsSupport = false;
    //    ToggleActionValidate();
    //}

    [MenuItem("Web Assembly/Enable Wasm Only")]
    static void EnableWasm()
    {
        PlayerSettings.WebGL.linkerTarget = WebGLLinkerTarget.Wasm;
        WasmActionValidate();
    }

    //[MenuItem("Web Assembly/Disable Wasm Only")]
    //static void DisableWasm()
    //{
    //    PlayerSettings.WebGL.linkerTarget = WebGLLinkerTarget.Both;
    //    WasmActionValidate();
    //}

    //[MenuItem("Web Assembly/Threads Support Active", false)]
    //public static bool ToggleActionValidate()
    //{
    //    Menu.SetChecked("Web Assembly/Threads Support Active", PlayerSettings.WebGL.threadsSupport);
    //    return true;
    //}

    [MenuItem("Web Assembly/Wasm Only", false)]
    public static bool WasmActionValidate()
    {
        Menu.SetChecked("Web Assembly/Wasm Only", PlayerSettings.WebGL.linkerTarget == WebGLLinkerTarget.Wasm ? true : false);
        return true;
    }

}
