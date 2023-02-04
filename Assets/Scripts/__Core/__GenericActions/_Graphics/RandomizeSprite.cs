using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSprite : MonoBehaviour
{
    public bool runInAwake = true;
    public Sprite[] sprites;

    void Awake()
    {
        if (runInAwake) Randomize();
    }

    public void Randomize()
    {
        if (sprites.Length == 0)
        {
            Debug.Log("[RandomizeSprite] Empty array: " + gameObject.name);
            return;
        }

        int nSort = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[nSort];
    }
}
