using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "General/New Level Config")]
public class LevelConfig : ScriptableObject
{
    public AnimationClip spawnProgression;
    public float spawnProgressionLength = 120f;
}