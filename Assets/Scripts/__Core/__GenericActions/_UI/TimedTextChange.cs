using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedTextChange : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textComponent;

    public float delay = 5f;
    [Multiline] public string[] textOptions;

    int currentOption = 0;

    void Awake()
    {
        StartCoroutine(ChangeCoroutine());
    }

    IEnumerator ChangeCoroutine()
    {
        currentOption = Random.Range(0, textOptions.Length);
        while (true)
        {
            textComponent.text = textOptions[currentOption];
            yield return new WaitForSeconds(delay);

            currentOption++;
            currentOption %= textOptions.Length;
        }
    }

}
