using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachFromParent : GenericActionCaller
{
    [Header("Detach")]
    public bool followParent = false;
    public bool destroyOnParentKill = false;

    Transform exParent;

    public override void MethodToCall()
    {
        base.MethodToCall();
        Detach();
    }

    public void Detach()
    {
        exParent = transform.parent;
        transform.parent = null;

        if (followParent || destroyOnParentKill)
        {
            FollowTarget followTargetComponent = gameObject.AddComponent<FollowTarget>();
            if (!followParent)
                followTargetComponent.followSpeed = 0f;

            followTargetComponent.followTarget = exParent;
            followTargetComponent.destroyOnParentKill = destroyOnParentKill;
            followTargetComponent.Initialize();
        }
    }
}
