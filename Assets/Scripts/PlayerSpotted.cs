using System;
using UnityEngine;

public class PlayerSpotted : MonoBehaviour
{
    private MonsterVision monsterScript;
    private void Start()
    {
        monsterScript = GetComponent<MonsterVision>();

        monsterScript.MonsterSeesPlayer += OnPlayerSpotted;
    }

    private void OnPlayerSpotted()
    {
        Debug.Log("You've been spotted!");
    }
}
