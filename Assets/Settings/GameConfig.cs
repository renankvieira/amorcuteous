using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "General/New Game Config")]
public class GameConfig : ScriptableObject
{
    public static GameConfig Instance => AppManager.Instance.gameConfig;
}
