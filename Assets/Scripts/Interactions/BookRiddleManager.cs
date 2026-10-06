using UnityEngine;
using System;

public class BookRiddleManager : MonoBehaviour
{
    [SerializeField] private BookRiddleComponent[] books;

    private int currentBookIndex;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (BookRiddleComponent book in books)
        {
            book.OnInteract.AddListener(OnBookInteracted);
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
            return;
        }

        currentBookIndex++;
        if (currentBookIndex >= books.Length)
        {
            //Puzzle completed
            Debug.Log("Puzzle complete!");
        }
    }
}
