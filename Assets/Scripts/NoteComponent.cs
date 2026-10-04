using UnityEngine;

public class NoteComponent : MonoBehaviour, IInteractable
{
    [SerializeField] [TextArea] private string noteText;
    
    public void Interact(){
        Debug.Log("Interaction successful");
    }
}
