using System;
using UnityEngine;

public class FlipRoom : MonoBehaviour
{
    public TriggerFlipRoom roomFlipScript;
    private Vector3 position;
    private void Start()
    {
        roomFlipScript = gameObject.GetComponent<TriggerFlipRoom>();
        roomFlipScript.RoomFlipTriggered += OnRoomFlipped;
    }

    private void OnRoomFlipped()
    {
        transform.Rotate(new Vector3(0, 0, -180),Space.Self);
    }
}
