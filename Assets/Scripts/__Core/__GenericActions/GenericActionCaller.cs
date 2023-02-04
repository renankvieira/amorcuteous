using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using System;

public class GenericActionCaller : MonoBehaviour
{
    [Header("GAC Config")]
    public CallEvent unityEventToListen = CallEvent.START;

    public ExtraGACSettings extraGacSettings;
    [Serializable] public class ExtraGACSettings
    {
        public float delayOnCall = 0f;
        public UnityEvent eventToCall;
        public bool ignoreAppIsQuitting = false;
    }

    // Call Set Actions
    virtual public void MethodToCall() { }
    virtual public void EventToCall() => extraGacSettings.eventToCall.Invoke();

    // Tests
    virtual public bool RunExtraTests() { return true; }
    [Button]
    public void CallMethodAsTest() { MethodToCall(); }

    // Listen to UnityEvents
    virtual protected void Awake() => TryToDoAction(CallEvent.AWAKE);
    virtual protected void OnEnable() => TryToDoAction(CallEvent.ON_ENABLE);
    virtual protected void Start() => TryToDoAction(CallEvent.START);
    virtual protected void Update() => TryToDoAction(CallEvent.UPDATE);
    virtual protected void OnDisable() => TryToDoAction(CallEvent.ON_DISABLE);
    virtual protected void OnDestroy() => TryToDoAction(CallEvent.ON_DESTROY);

    protected void TryToDoAction(CallEvent currentUnityEvent)
    {
        if (currentUnityEvent != unityEventToListen) return;
        if (AppManager.AppIsQuitting && !extraGacSettings.ignoreAppIsQuitting) return;
        if (!RunExtraTests()) return;

        if (extraGacSettings.delayOnCall <= 0f)
        {
            MethodToCall();
            EventToCall();
        }
        else
        {
            this.DelayedInvoke(MethodToCall, extraGacSettings.delayOnCall);
            this.DelayedInvoke(EventToCall, extraGacSettings.delayOnCall);
        }
    }
}