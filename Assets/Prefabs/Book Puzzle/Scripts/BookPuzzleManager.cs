using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BookPuzzleManager : MonoBehaviour
{
    //The event to open the front door/whatever we decide will happen
    [CanBeNull] public event Action BookPuzzleCompleted;
    
    //Stores all the shelves
    //Add these references in the editor
    public List<GameObject> shelves;

    private bool puzzleCompleted;

    private bool booksCorrect;

    // Update is called once per frame
    void Update()
    {
        //Stops code from being executed repeatedly if the puzzle is already completed
        if (puzzleCompleted) return;
        
        //If any books are incorrect, we exit the loop and report that the books aren't
        //in their correct positions
        booksCorrect = true;
        foreach (GameObject shelf in shelves)
        {
            if (!shelf.GetComponent<BookPosChecker>().correctBookPlaced)
            {
                booksCorrect = false;
                return;
            }
        }

        //If we make it through the loop once without booksCorrect being set to false, the puzzle is completed
        if (booksCorrect)
        {
            //BookPuzzleCompleted.Invoke();
            Debug.Log("Puzzle completed");
            puzzleCompleted = true;
        }

    }
}
