using UnityEngine;

public class BookPosChecker : MonoBehaviour
{
    //Can set the correct book to be placed here in the editor
    //Make sure the book object's name and the string here are exactly the same
    public string assignedBookName;

    public GameObject book;

    private string currentBookName;

    public bool correctBookPlaced;

    // Update is called once per frame
    void Update()
    {
        //Prevents code from repeating if the book placed is correct
        if (correctBookPlaced) return;
        
        //Prevents error from being thrown by the next line of code
        if(transform.childCount == 0) return;
        
        //Get access to the book stored here
        book = transform.GetChild(0).gameObject;

        //Compare the name of the book placed here with the name of the book that needs to be placed here
        currentBookName = book.name;

        if (currentBookName == assignedBookName)
        {
            correctBookPlaced = true;
            Debug.Log("Book placed in correct position");
        }
        else
        {
            correctBookPlaced = false;
        }
    }
}
