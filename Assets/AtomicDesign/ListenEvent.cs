using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VT.Observer.ObservableVar<int> myVar = new VT.Observer.ObservableVar<int>("Counter2");
        new VT.Front.FrontAction("<AtomicDesign>", (_msg) =>
        {
            myVar.Value = myVar.Value + 1;
            Debug.Log($"Received message {_msg}");
        });
    }
}
