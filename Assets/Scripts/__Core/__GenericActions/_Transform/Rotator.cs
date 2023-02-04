using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Range(-1.0f, 1.0f)]
    public float xComponent = 0.0f;
    [Range(-1.0f, 1.0f)]
    public float yComponent = 0.0f;
    [Range(-1.0f, 1.0f)]
    public float zComponent = 0.0f;

    public float speedMultiplier = 1;

    public bool worldPivot = false;

    void Update()
    {
        float speedOnFrame = speedMultiplier * Time.deltaTime;
        transform.Rotate(xComponent * speedOnFrame, yComponent * speedOnFrame, zComponent * speedOnFrame, worldPivot ? Space.World : Space.Self);
    }

}