using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float cooldownTime = 0.3f;
    
    [SerializeField] UIDocument crosshairUI;
    
    private Transform currentInteractable;
    private Image crosshair;
    
    private Stopwatch cooldownTimer = new Stopwatch();

    public void Start()
    {
        crosshair = crosshairUI.rootVisualElement.Q<Image>("crosshair");
        cooldownTimer.Start();
    }

    public void CheckInteractables(InputAction.CallbackContext context){
        
        int layerMask = 1 << 7; 
        RaycastHit hit_Object;

        if(Physics.Raycast(transform.position, transform.forward, out hit_Object, 4, layerMask)){
            currentInteractable = hit_Object.transform;
            crosshair.style.opacity = 1;
        }
        else
        {
            
            IInteractable interaction = currentInteractable?.GetComponent<IInteractable>();
            interaction?.CancelInteraction();
            
            currentInteractable = null;
            crosshair.style.opacity = 0;
        }

    }

    public void OnInteractionAttempt(InputAction.CallbackContext context){

        if (context.started == false)
        {
            return;
        }

        if (cooldownTimer.Elapsed.Seconds < cooldownTime)
        {
            return;
        }
        
        cooldownTimer.Restart();
        
        IInteractable interaction = currentInteractable?.GetComponent<IInteractable>();

        interaction?.Interact();
    }
}
