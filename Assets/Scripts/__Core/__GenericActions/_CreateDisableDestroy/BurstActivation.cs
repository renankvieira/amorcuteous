using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstActivation : GenericActionCaller
{
    [Header("Burst")]
    public bool includeAllChildren = true;
    public float timeBefore = 0f;
    public float timeBetween = 0.1f;
    public List<Transform> activatables;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void MethodToCall()
    {
        base.MethodToCall();

        if (includeAllChildren)
        {
            activatables.Clear();

            for (int i = 0; i < transform.childCount; i++)
                activatables.Add(transform.GetChild(i));
        }

        foreach (Transform t in activatables)
            t.gameObject.SetActive(false);

        DoBurst();
    }

    public void DoBurst()
    {
        //print(1);
        StartCoroutine(BurstCoroutine());
    }

    IEnumerator BurstCoroutine()
    {
        yield return new WaitForSeconds(timeBefore);

        int nextIndex = 0;
        while (nextIndex < activatables.Count)
        {
            //print(3);
            activatables[nextIndex].gameObject.SetActive(true);
            nextIndex++;
            yield return new WaitForSeconds(timeBetween);
        }
    }

}
