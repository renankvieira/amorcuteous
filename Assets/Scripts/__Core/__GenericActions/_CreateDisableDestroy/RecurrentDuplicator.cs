using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecurrentDuplicator : MonoBehaviour
{
    [Header("Duplicatables")]
    public GameObject[] possibleObjects;

    [Header("Timer")]
    public MinMax timeBetweenDuplications = new MinMax() { min = 3f, max = 5f };
    public bool runImmediately = false;

    [Space(16, order = -1)]
    [Header("New Object Settings")]
    public Transform copyParent;
    public bool flipOnDuplicate = true;
    public Transform copyParentFlipped;

    [Header("Extra Settings")]
    public bool limitRepetitionCount = false;
    public int repetitionLimit = 10;
    public bool setCopyAsInactive = true;
    public bool setCopyAsActive = true;

    [Header("Watch")]
    float timeOfNextCall;
    float timeOfLastCall;
    int repetitionCount = 0;
    bool nextIsFlipped = false;

    IEnumerator Start()
    {
        if (!runImmediately)
            timeOfNextCall = Time.time + timeBetweenDuplications.NewRandomValueInside();

        while (true)
        {
            if (Time.time >= timeOfNextCall || runImmediately)
                Duplicate();

            if (limitRepetitionCount)
                if (repetitionCount >= repetitionLimit)
                    yield break;

            yield return null;
        }
    }

    void Duplicate()
    {
        GameObject newObject;

        if (GetNextCopyParent() == null)
            newObject = Instantiate(possibleObjects.GetRandom());
        else
            newObject = Instantiate(possibleObjects.GetRandom(), GetNextCopyParent());

        if (setCopyAsInactive)
            newObject.SetActive(false);
        if (setCopyAsActive)
            newObject.SetActive(true);

        repetitionCount++;
        timeOfLastCall = Time.time;
        timeOfNextCall = Time.time + timeBetweenDuplications.NewRandomValueInside();
        runImmediately = false;

        if (flipOnDuplicate)
            nextIsFlipped = !nextIsFlipped;
    }



    Transform GetNextCopyParent()
    {
        if (flipOnDuplicate)
            return nextIsFlipped ? copyParentFlipped : copyParent;
        else
            return copyParent;
    }
}
