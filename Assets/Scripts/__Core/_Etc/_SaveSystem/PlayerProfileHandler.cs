using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfileHandler : MonoBehaviour
{
    #region General
    public static SaveableData Profile => SaveSystem.PlayerProfile;
    public static void SaveProfile() => SaveSystem.SaveProfile();

    #endregion

    #region OptionExample
    //public static void OnExampleClassDataChange(ExampleSaveableClass mainObject, bool savingProfile)
    //{
    //    bool contains = false;

    //    foreach (ExampleSaveData savedItem in Profile.ExampleSaveDatas)
    //    {
    //        if (savedItem.IsCorrespondingObject(mainObject))
    //        {
    //            savedItem.progress = mainObject.progress;
    //            contains = true;
    //            break;
    //        }
    //    }

    //    if (!contains)
    //    {
    //        Profile.ExampleSaveDatas.Add(ExampleSaveData.CreateNewSaveData(mainObject, mainObject.progress));
    //    }

    //    if (savingProfile)
    //        SaveProfile();
    //}

    //public static ExampleSaveData GetExampleSaveData(ExampleSaveableClass mainObject)
    //{
    //    foreach (ExampleSaveData savedItem in Profile.ExampleSaveDatas)
    //        if (savedItem.IsCorrespondingObject(mainObject))
    //            return savedItem;

    //    // If not found, save data is created.
    //    ExampleSaveData newSaveData = ExampleSaveData.CreateNewSaveData(mainObject, mainObject.progress); // or default state, instead of progress.
    //    Profile.ExampleSaveDatas.Add(newSaveData);
    //    return newSaveData;
    //}
    #endregion



    //public static int GetUpgradeLevel(ProgressiveValue progressiveValue)
    //{
    //    ProgressiveValueSaveData progressiveValueSaveData = null;
    //    for (int i = 0; i < Profile.UpgradeLevels.Count; i++)
    //    {
    //        if (Profile.UpgradeLevels[i].saveId == progressiveValue.saveId)
    //        {
    //            progressiveValueSaveData = Profile.UpgradeLevels[i];
    //            break;
    //        }
    //    }

    //    if (progressiveValueSaveData == null)
    //    {
    //        progressiveValueSaveData = new ProgressiveValueSaveData();
    //        progressiveValueSaveData.saveId = progressiveValue.saveId;
    //        Profile.UpgradeLevels.Add(progressiveValueSaveData);
    //    }

    //    return progressiveValueSaveData.level;
    //}

    //public static void SetUpgradeLevel(ProgressiveValue progressiveValue, int level)
    //{
    //    ProgressiveValueSaveData progressiveValueSaveData = null;
    //    for (int i = 0; i < Profile.UpgradeLevels.Count; i++)
    //    {
    //        if (Profile.UpgradeLevels[i].saveId == progressiveValue.saveId)
    //        {
    //            progressiveValueSaveData = Profile.UpgradeLevels[i];
    //            break;
    //        }
    //    }

    //    if (progressiveValueSaveData == null)
    //    {
    //        progressiveValueSaveData = new ProgressiveValueSaveData();
    //        progressiveValueSaveData.saveId = progressiveValue.saveId;
    //        progressiveValueSaveData.level = level;
    //        Profile.UpgradeLevels.Add(progressiveValueSaveData);
    //    }
    //    else
    //    {
    //        progressiveValueSaveData.level = level;
    //    }
    //}


}

