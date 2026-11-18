using UnityEngine;

public class FrontDoorOpen : MonoBehaviour, IInteractable
{
    private BookPuzzleManager bookPuzzleScript;
    private bool doorUnlocked;

    private WinGame winScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bookPuzzleScript = GameObject.Find("GameManager").GetComponent<BookPuzzleManager>();
        winScript = GameObject.Find("GameManager").GetComponent<WinGame>();
        bookPuzzleScript.BookPuzzleCompleted += OnPuzzleCompleted;
    }
    
    private void OnPuzzleCompleted()
    {
        doorUnlocked = true;
    }

    public void Interact()
    {
        if (doorUnlocked) winScript.Win();
    }

    public void CancelInteraction()
    {
        
    }
}
