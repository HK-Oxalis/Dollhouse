using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerFlashlight : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;

    public bool isOn { get; private set; }

    public void Start()
    {
        flashlight.SetActive(false);
    }


    public void ToggleFlashlight(InputAction.CallbackContext context)
    {
        isOn = context.started || context.performed;
        
        flashlight.SetActive(isOn);
    }
    
}
