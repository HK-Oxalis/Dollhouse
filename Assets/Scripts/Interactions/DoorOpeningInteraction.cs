using UnityEngine;

public class DoorOpeningInteraction : MonoBehaviour, IInteractable
{
    private Animator animator;

    private bool isOpen = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            animator.SetTrigger("Open");}
        else
        {
            animator.SetTrigger("Close");
        }
        
        

    }
    
    public void CancelInteraction()
    {
    }
}
