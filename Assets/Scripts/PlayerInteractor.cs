using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] UIDocument crosshairUI;
    
    private Transform currentInteractable;
    private Image crosshair;

    public void Start()
    {
        crosshair = crosshairUI.rootVisualElement.Q<Image>("crosshair");
    }

    public void CheckInteractables(InputAction.CallbackContext context){
        
        int layerMask = 1 << 7; 
        RaycastHit hit_Object;

        if(Physics.Raycast(transform.position, transform.forward, out hit_Object, 10, layerMask)){
            currentInteractable = hit_Object.transform;
            crosshair.style.opacity = 1;
        }
        else
        {
            crosshair.style.opacity = 0;
        }

    }

    public void OnInteractionAttempt(InputAction.CallbackContext context){
        
        IInteractable interaction = currentInteractable.GetComponent<IInteractable>();

        interaction.Interact();
    }
}
