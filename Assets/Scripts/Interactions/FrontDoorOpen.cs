using UnityEngine;

public class FrontDoorOpen : MonoBehaviour
{
    private BookPuzzleManager bookPuzzleScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bookPuzzleScript = GameObject.Find("GameManager").GetComponent<BookPuzzleManager>();
        bookPuzzleScript.BookPuzzleCompleted += OnPuzzleCompleted;
    }
    
    private void OnPuzzleCompleted()
    {
        Destroy(gameObject);
    }
}
