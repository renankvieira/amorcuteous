using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePrinter : MonoBehaviour
{
    public bool printOnAwake = false;
    public string messageToPrint = "test";

    private void Awake()
    {
        if (printOnAwake)
            PrintMessage();
    }

    public void PrintMessage()
    {
        Debug.Log(messageToPrint);
    }
}
