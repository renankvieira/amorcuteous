using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

public class ActionOnMouseInteraction : MonoBehaviour
{
    // Mouse events need a collider.

    [Header("Config")]
    public MouseEvent mouseEventToListen = MouseEvent.MOUSE_ENTER;
    public bool runOnlyOnce = true;
    public bool printMessageOnAction = false;
    public UnityEvent eventToCall;

    bool alreadyRan = false;

    // Listen to MouseEvents
    virtual protected void OnMouseOver() => TryToDoAction(MouseEvent.MOUSE_OVER);
    virtual protected void OnMouseEnter() => TryToDoAction(MouseEvent.MOUSE_ENTER);
    virtual protected void OnMouseDown() => TryToDoAction(MouseEvent.MOUSE_DOWN);
    virtual protected void OnMouseExit() => TryToDoAction(MouseEvent.MOUSE_EXIT);

    virtual protected void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryToDoAction(MouseEvent.MOUSE_BUTTON_0_DOWN);
        if (Input.GetMouseButtonUp(0))
            TryToDoAction(MouseEvent.MOUSE_BUTTON_0_UP);
    }

    // Generic actions
    virtual public void MethodToCall() { }
    virtual public void EventToCall() => eventToCall.Invoke();

    // Tests
    virtual public bool RunExtraTests() { return true; }

    protected void TryToDoAction(MouseEvent currentMouseEvent)
    {
        if (currentMouseEvent != mouseEventToListen) return;
        if (runOnlyOnce && alreadyRan) return;
        if (!RunExtraTests()) return;

        if (printMessageOnAction)
            Debug.LogFormat(gameObject, "[AOMI] Action on MouseEvent {0}, for object {1}.", currentMouseEvent, gameObject.name);

        MethodToCall();
        EventToCall();

        alreadyRan = true;
    }

    public enum MouseEvent
    {
        MOUSE_ENTER = 10,
        MOUSE_OVER = 20,
        MOUSE_DOWN = 30,
        MOUSE_EXIT = 40,

        MOUSE_BUTTON_0_DOWN = 50,
        MOUSE_BUTTON_0_UP = 60
    }
}
