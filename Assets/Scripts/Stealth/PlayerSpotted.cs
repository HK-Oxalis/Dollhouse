using System;
using UnityEngine;

public class PlayerSpotted : MonoBehaviour
{
    //Access the MonsterVision script so the event can be subscribed to
    private MonsterVision monsterScript;
    private void Start()
    {
        monsterScript = gameObject.GetComponent<MonsterVision>();
//Subscribe to MonsterSeesPlayer event
        monsterScript.MonsterSeesPlayer += OnPlayerSpotted;
    }

    private void OnPlayerSpotted()
    {
        //Right now, this message is used to show when the player has been spotted by the monster
        //Death is being added later
        Debug.Log("You've been spotted!");
    }
}
