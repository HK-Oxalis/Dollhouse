using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    private Transform currentInteractable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckInteractables(InputAction.CallbackContext context){
        
        int layerMask = 1 << 7; 
        RaycastHit hit_Object;

        if(Physics.Raycast(transform.position, transform.forward, out hit_Object, 10, layerMask)){
            currentInteractable = hit_Object.transform;
        };

    }

    public void OnInteractionAttempt(InputAction.CallbackContext context){
        
        IInteractable interaction = currentInteractable.GetComponent<IInteractable>();

        interaction.Interact();
    }
}
