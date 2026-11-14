using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float cameraSensitivity = 1;
    [SerializeField] private float walkSpeed = 5;

    [SerializeField] private Transform headTransform;
    
    private Vector3 walkInput;
    private CharacterController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Rotate walk input because CharacterController.Move uses absolute position, not local
        Vector3 rotatedWalk = Quaternion.Euler(0, headTransform.localEulerAngles.y, 0) * walkInput;
        
        controller.SimpleMove(rotatedWalk  * (walkSpeed * Time.deltaTime));
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>() * cameraSensitivity * Time.deltaTime;
        
        headTransform.Rotate(new Vector3(-lookInput.y, lookInput.x, 0), Space.Self);

        headTransform.localEulerAngles = new Vector3(headTransform.localEulerAngles.x, headTransform.localEulerAngles.y, 0);
    }
    
    
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        walkInput = new Vector3(input.x, 0, input.y);
    }
}
