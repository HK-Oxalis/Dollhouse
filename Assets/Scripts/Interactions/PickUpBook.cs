using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PickUpBook : MonoBehaviour, IInteractable
{ 
    //Gives access to necessary objects and components
    public GameObject hand;
    public IsHoldingBook handScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set above references
        hand = GameObject.Find("Hand");
        handScript = hand.GetComponent<IsHoldingBook>();
    }

    public void Interact()
    {
        //Player cannot pick up a book if they are already holding one
        if (handScript.bookHeld) return;

        //When we pick up the book, we inform the book position's script that it is now devoid of a book
        transform.parent.GetComponent<PutDownBook>().isEmpty = true;
        
        //Code to put the book into the player's hand so they can carry it around
        transform.SetParent(hand.transform, false);
        transform.position = hand.transform.position;
        transform.rotation = hand.transform.rotation;
        
        //Inform the hand's script that it is now holding a book
        handScript.bookHeld = true;
    }

    public void CancelInteraction()
    {
        
    }
}
