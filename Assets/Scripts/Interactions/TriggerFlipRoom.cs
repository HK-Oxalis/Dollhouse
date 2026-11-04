using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class TriggerFlipRoom : MonoBehaviour
{

    public event Action RoomFlipTriggered;

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            RoomFlipTriggered?.Invoke();
        }
    }
}
