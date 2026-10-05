using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MonsterVision : MonoBehaviour
{
    //Define event
    [CanBeNull] public event Action MonsterSeesPlayer;
    
    //Variables to access various components
    private GameObject monster;
    private Transform monsterTransform;
    private Vector3 monsterPosition;

    private GameObject player;
    private Transform playerTransform;
    private Vector3 playerPosition;
    private Collider playerCollider;

    //Variable for checking if the player is visible
    private bool seesPlayer = false;

    //Timer (for later)
    private float timer = 0f;

    private void Start()
    {
        //Access the necessary components and store them
        monster = GameObject.Find("MonsterTemp");
        monsterTransform = monster.GetComponent<Transform>();

        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
        playerCollider = player.GetComponent<Collider>();
    }

    private void Update()
    {
        //Check whether the monster can see the player when space is pressed
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            MonsterLook();
        }
    }

    private void MonsterLook()
    {
        
        //Create raycast between monster and player to represent monster's sightline
            if (Physics.Raycast(monsterTransform.position,
                    monsterTransform.TransformDirection(playerTransform.position - monsterTransform.position),
                    out RaycastHit hitInfo))
            {
                //Draws ray for testing purposes
                Debug.DrawRay(monsterTransform.position,
                    monsterTransform.TransformDirection(playerTransform.position - monsterTransform.position) *
                    hitInfo.distance, Color.red);
                //If there's nothing between the monster and player, the player is considered visible
                if (hitInfo.collider == playerCollider)
                {
                    seesPlayer = true;
                }
                else
                {
                    seesPlayer = false;
                }
            }
//Invoke event for the PlayerSpotted script to handle detection
            if (seesPlayer == true)
            {
                MonsterSeesPlayer?.Invoke();
            }
    }
}
