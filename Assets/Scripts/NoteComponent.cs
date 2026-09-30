using UnityEngine;

public class NoteComponent : MonoBehaviour, IInteractable
{
    public void Interact(){
        Debug.Log("Interaction successful");
    }
}
