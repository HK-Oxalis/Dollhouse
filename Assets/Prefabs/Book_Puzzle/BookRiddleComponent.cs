using System;
using UnityEngine;
using UnityEngine.Events;

public class BookRiddleComponent : MonoBehaviour, IInteractable
{
    [SerializeField] private float rotationSpeed = 40;
    [SerializeField] private int rotationDegrees = 30;
    public UnityEvent<BookRiddleComponent> OnInteract;
    public AudioClip interactSound;
    
    private AudioSource audioSource;
    
    private bool isMoving = false;
    private bool movingForward = true;
    private float rotationAmount = 0;
    
    public void Awake()
    {
        if(OnInteract == null){ OnInteract = new UnityEvent<BookRiddleComponent>(); }
        
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Update()
    {
        if (isMoving)
        {
            float rotationTick = rotationSpeed * Time.deltaTime;

            if (movingForward)
            {
                rotationAmount += rotationTick;
                if (rotationAmount >= rotationDegrees)
                {
                    isMoving = false;
                    movingForward = false;
                }
            }
            else
            {
                rotationAmount -= rotationTick;
                rotationTick *= -1;
                if (rotationAmount <= 0)
                {
                    isMoving = false;
                    movingForward = true;
                }
            }
            transform.Rotate(Vector3.right, rotationTick);
        }
    }

    public void Interact()
    {
        OnInteract.Invoke(this);
        
        audioSource.PlayOneShot(interactSound);
        
        isMoving = true;
    }

    public void CancelInteraction()
    {
    }

    public void ReturnToStart()
    {
        movingForward = false;
        isMoving = true;
    }
}
