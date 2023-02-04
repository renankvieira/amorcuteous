using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeClicker : GenericActionCaller
{

    public UnityEngine.UI.Button buttonToFakeClick;

    public override void MethodToCall()
    {
        base.MethodToCall();
        PerformFakeClick();
    }

    public void PerformFakeClick()
    {
        buttonToFakeClick.FakeClick();
    }

}
