using System;
using System.Collections;
using System.Diagnostics;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class MonsterVision : MonoBehaviour
{
    //Define event
    [CanBeNull] public event Action MonsterSeesPlayer;

    private IEnumerator coroutine;
    
    //Variables to access various components
    private GameObject monster;
    private Transform monsterTransform;
    private Vector3 monsterPosition;

    private GameObject player;
    private Transform playerTransform;
    private Vector3 playerPosition;
    private Collider playerCollider;
    
    //Delete when proper trigger implemented
    private bool monsterSearching;

    //Create stopwatch used to time monster's searching period
    private Stopwatch stopwatch = new Stopwatch();
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
        //Check whether the monster can see the player when space is pressed [testing purposes]
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !monsterSearching)
        { 
            monsterSearching = true;
            coroutine = MonsterLook();
            StartCoroutine(coroutine);
            monsterSearching = false;
        }
    }

    private IEnumerator MonsterLook()
    {
        //Resets stopwatch in case it was already started earlier
        stopwatch.Reset();
        //Start timing monster's search period
        stopwatch.Start();
        //Monster searches for 3 seconds if the player isn't visible
        while (stopwatch.Elapsed < TimeSpan.FromSeconds(3))
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
                    //Invoke event for the PlayerSpotted script to handle detection
                    MonsterSeesPlayer?.Invoke();
                    //End search period early because player wasn't seen
                    yield break;
                }
            }
            yield return null;
        }
//If the timer runs out, the player was hidden and the monster stops searching
        if (stopwatch.Elapsed >= TimeSpan.FromSeconds(3))
        { 
            Debug.Log("Player wasn't seen");
        }
    }
}
