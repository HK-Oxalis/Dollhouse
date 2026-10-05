using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterVision : MonoBehaviour
{
    [CanBeNull] public event Action MonsterSeesPlayer;

    private GameObject monster;
    private Transform monsterTransform;
    private Vector3 monsterPosition;

    private GameObject player;
    private Transform playerTransform;
    private Vector3 playerPosition;

    private bool seesPlayer = false;

    private void Start()
    {
        monster = GameObject.Find("MonsterTemp");
        monsterTransform = monster.GetComponent<Transform>();
        monsterPosition = monsterTransform.position;

        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
    }

    private void Update()
    {
        playerPosition = playerTransform.position;
    }

    private void MonsterLook()
    {
        Physics.Raycast(monsterPosition, );
        
        if (seesPlayer == true)
        {
            MonsterSeesPlayer?.Invoke();
        }
    }
}
