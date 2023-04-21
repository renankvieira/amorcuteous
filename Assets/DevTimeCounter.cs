using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTimeCounter : MonoBehaviour
{
    float timeCount = 0f;
    TMPro.TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        timeCount += Time.deltaTime;
        text.text = timeCount.ToString("F1");
    }
}
