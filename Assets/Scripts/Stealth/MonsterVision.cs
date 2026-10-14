using System;
using System.Collections;
using System.Diagnostics;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

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

    private bool isGracePeriodActive;
    

    //Create stopwatchs used to time monster's searching period
    private Stopwatch startTimer = new Stopwatch();
    private Stopwatch lookTimer = new Stopwatch();
    private Stopwatch graceTimer = new Stopwatch();

    public float lookTime = 3f;
    public float startUpTime = 3f;
    public float gracePeriod = 2f;
    private void Start()
    {
        //Access the necessary components and store them
        monster = GameObject.Find("Monster");
        monsterTransform = monster.GetComponent<Transform>();

        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
        playerCollider = player.GetComponent<Collider>();
        
    }

    private void Update()
    {
        isGracePeriodActive = MonsterGracePeriod() != null;
    }

    public void MonsterStartLooking()
    {
        StartCoroutine(MonsterStartUp());
    }

    public IEnumerator MonsterStartUp()
    {
        Debug.Log("Monster has started to activate");
        startTimer.Start();
        while (startTimer.Elapsed < TimeSpan.FromSeconds(startUpTime)) {yield return null;}
        
        if (startTimer.Elapsed >= TimeSpan.FromSeconds(startUpTime)) {StartCoroutine(MonsterLook());}
        
        startTimer.Reset();
    }
    
    public IEnumerator MonsterLook()
    {
        Debug.Log("Monster started looking");
        //Start timing monster's search period
        lookTimer.Start();
        //Monster searches for 3 seconds if the player isn't visible
        while (lookTimer.Elapsed < TimeSpan.FromSeconds(lookTime))
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
                    StartCoroutine(MonsterGracePeriod());
                    yield return new WaitUntil(() => !isGracePeriodActive);
                    lookTimer.Start();
                }
            }
            yield return null;
        } 
        //If the timer runs out, the player was hidden and the monster stops searching
        if (lookTimer.Elapsed >= TimeSpan.FromSeconds(lookTime))
        { 
            Debug.Log("Player wasn't seen");
            lookTimer.Reset();
        }
    }

    public IEnumerator MonsterGracePeriod()
    {
        isGracePeriodActive = true;
        lookTimer.Stop();
        graceTimer.Start();
        Debug.Log("Grace Period started");
        while (graceTimer.Elapsed < TimeSpan.FromSeconds(gracePeriod)) {yield return null;}

        if (graceTimer.Elapsed >= TimeSpan.FromSeconds(gracePeriod))
        {
            if (Physics.Raycast(monsterTransform.position, 
                    monsterTransform.TransformDirection(playerTransform.position - monsterTransform.position),
                    out RaycastHit hitInfo2))
            {
                Debug.DrawRay(monsterTransform.position,
                    monsterTransform.TransformDirection(playerTransform.position - monsterTransform.position) *
                    hitInfo2.distance, Color.blue);
                
                if (hitInfo2.collider == playerCollider)
                {
                    //Invoke event for the PlayerSpotted script to handle detection
                    MonsterSeesPlayer?.Invoke();
                    lookTimer.Reset();
                }
            } 
        }
        graceTimer.Reset();
        isGracePeriodActive = false;
    }
}
