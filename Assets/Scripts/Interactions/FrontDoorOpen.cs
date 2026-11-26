using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class FrontDoorOpen : MonoBehaviour, IInteractable
{
    private BookPuzzleManager bookPuzzleScript;
    private bool doorUnlocked;

    private WinGame winScript;

    private float doorOpenTime = 0.55f;

    private Stopwatch doorOpenTimer = new Stopwatch();

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bookPuzzleScript = GameObject.Find("GameManager").GetComponent<BookPuzzleManager>();
        winScript = GameObject.Find("GameManager").GetComponent<WinGame>();

        animator = GetComponent<Animator>();
        
        bookPuzzleScript.BookPuzzleCompleted += OnPuzzleCompleted;
    }
    
    private void OnPuzzleCompleted()
    {
        doorUnlocked = true;
    }

    public void Interact()
    {
        if (doorUnlocked) StartCoroutine(OpenDoorAnim());
    }

    public void CancelInteraction() {}

    public IEnumerator OpenDoorAnim()
    {
        animator.SetTrigger("Open");
        
        while (doorOpenTimer.Elapsed < TimeSpan.FromSeconds(doorOpenTime)) {yield return null;}
        if (doorOpenTimer.Elapsed >= TimeSpan.FromSeconds(doorOpenTime)) {winScript.Win();}
    }
}
