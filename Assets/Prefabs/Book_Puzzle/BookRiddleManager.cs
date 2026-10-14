using UnityEngine;
using UnityEngine.Events;
using System;

public class BookRiddleManager : MonoBehaviour
{
    [SerializeField] private AudioClip interactSound;
    [SerializeField] private AudioClip OpenDoorSound;
    [SerializeField] private BookRiddleComponent[] books;
    [SerializeField] private Animator doorAnimator;

    public UnityEvent OnPuzzleSolved;
    
    private AudioSource audioSource;
    private int currentBookIndex;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        foreach (BookRiddleComponent book in books)
        {
            book.OnInteract.AddListener(OnBookInteracted);
            book.interactSound = interactSound;
        }
    }

    private void OnBookInteracted(BookRiddleComponent book)
    {
        int bookIndex = Array.IndexOf(books, book);

        if (bookIndex != currentBookIndex)
        {
            //Incorrect input, reset puzzle
            Debug.Log("Puzzle failed, resetting");
            currentBookIndex = 0;
            foreach (BookRiddleComponent otherBook in books)
            {
                otherBook.ReturnToStart();
            }
            
            return;
        }

        currentBookIndex++;
        if (currentBookIndex >= books.Length)
        {
            //Puzzle completed
            Debug.Log("Puzzle complete!");
            
            OnPuzzleSolved.Invoke();
        }
    }

    public void Open_Door()
    {
        doorAnimator.SetTrigger("Open");
        audioSource.PlayOneShot(OpenDoorSound);
    }
}
