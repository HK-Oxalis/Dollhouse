using System;
using UnityEngine;
using UnityEngine.Events;

public class BookRiddleComponent : MonoBehaviour, IInteractable
{

    public UnityEvent<BookRiddleComponent> OnInteract;
    public AudioClip interactSound;
    
    private AudioSource audioSource;
    
    private Animator animator;
    
    public void Awake()
    {
        if(OnInteract == null){ OnInteract = new UnityEvent<BookRiddleComponent>(); }
        
        
        animator = gameObject.GetComponent<Animator>();
    }

    public void Update()
    {
        
    }

    public void Interact()
    {
        //animator.ResetTrigger("Reset");
        animator.SetTrigger("Interact");
        
        OnInteract.Invoke(this);

    }

    public void CancelInteraction()
    {
    }

    public void ReturnToStart()
    {
        animator.ResetTrigger("Interact");
        animator.SetTrigger("Reset");
    }
}
