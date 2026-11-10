using UnityEngine;

public class PutDownBook : MonoBehaviour, IInteractable
{
    //Get access to necessary components
    public GameObject book;
    public GameObject hand;
    public IsHoldingBook handScript;

    //Variable that stores whether a book is placed here
    public bool isEmpty;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Sets values of above variables
        hand = GameObject.Find("Hand");
        handScript = hand.GetComponent<IsHoldingBook>();
    }

    public void Interact()
    {
        //Player cannot put a book in a position where there is already a book
        if (!isEmpty) return;

        //Get access to the book
        book = hand.transform.GetChild(0).gameObject;
        
        //Put the book into its position
        book.transform.SetParent(gameObject.transform, false);
        book.transform.position = transform.position;
        book.transform.rotation = transform.rotation;

        //Inform the hand's script that it is no longer holding a book
        handScript.bookHeld = false;

        //This position is now filled, so a book cannot be placed here
        isEmpty = false;
    }

    public void CancelInteraction()
    {
    }
}
