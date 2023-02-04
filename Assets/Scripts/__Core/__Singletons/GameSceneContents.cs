using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneContents : SingletonOfType<GameSceneContents>
{
    protected override void Awake()
    {
        base.Awake();
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        shotsParent = new GameObject("__ShotsParent").transform;
        enemiesParent = new GameObject("__EnemiesParent").transform;
        fxParent = new GameObject("__FxParent").transform;
    }

    [Header("Etc")]
    //public Player player;
    public Transform playerTransform;
    //public List<Enemy> enemiesAlive;

    public Transform shotsParent;
    public Transform enemiesParent;
    public Transform fxParent;

    //Transform runtimeObjectsParent;
    //public Transform RuntimeObjectsParent
    //{
    //    get
    //    {
    //        if (runtimeObjectsParent == null)
    //            runtimeObjectsParent = new GameObject("RuntimeObjectsParent").transform;
    //        return runtimeObjectsParent;
    //    }
    //}

    //[Header("Toy")]
    //public ToyPlayer toyPlayer;
    //public Dictionary<int, ToyWaypointGroup> waypointGroups;
    //public List<ToyEnemy> enemiesAlive;
    //public List<ToyCutie> cutiesAlive;

    //public void AddWaypointGroup(ToyWaypointGroup waypointGroup)
    //{
    //    if (waypointGroups == null)
    //        waypointGroups = new Dictionary<int, ToyWaypointGroup>();

    //    if (waypointGroups.ContainsKey(waypointGroup.channel))
    //    {
    //        Debug.LogError("Duplicated channel: " + waypointGroup.channel, waypointGroup.gameObject);
    //        Debug.Break();
    //    }

    //    waypointGroups.Add(waypointGroup.channel, waypointGroup);
    //}


}
