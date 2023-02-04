using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public GameObject prefabToCreate;
    public Transform optionalSpawnAnchor;

    public void Execute()
    {
        GameObject createdGameObject = null;
        if (optionalSpawnAnchor == null)
        {
            createdGameObject = Instantiate(prefabToCreate);
        }
        else
        {
            createdGameObject = Instantiate(prefabToCreate, optionalSpawnAnchor.transform.position, prefabToCreate.transform.rotation);
        }
        //return createdGameObject;
    }

    public GameObject ExecuteAndReturn()
    {
        GameObject createdGameObject = null;
        if (optionalSpawnAnchor == null)
        {
            createdGameObject = Instantiate(prefabToCreate);
        }
        else
        {
            createdGameObject = Instantiate(prefabToCreate, optionalSpawnAnchor.transform.position, prefabToCreate.transform.rotation);
        }
        return createdGameObject;
    }

}
