using UnityEngine;

public class DoorOpeningInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private AnimationClip openingAnimation;
    
    private Animator animator;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        animator.Play(openingAnimation.name);
        gameObject.layer = 1; 
        Destroy(this);

    }
}
