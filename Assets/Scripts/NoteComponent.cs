using UnityEngine;
using UnityEngine.UIElements;

public class NoteComponent : MonoBehaviour, IInteractable
{
    [SerializeField] [TextArea] private string noteText;

    [SerializeField] private VisualTreeAsset uiTree;
    [SerializeField] private PanelSettings panelSettings;
    
    private GameObject uiObject;
    private bool noteShowing = false;
    
    public void Interact(){
        Debug.Log("Interaction successful");

        if (noteShowing)
        {
            noteShowing = false;
            Destroy(uiObject);
        }
        else
        {
            noteShowing = true;
            
            uiObject = new GameObject("Note");
            uiObject.transform.SetParent(transform);
            UIDocument uiDocument = uiObject.AddComponent<UIDocument>();
            uiDocument.panelSettings = panelSettings;
            uiDocument.visualTreeAsset = uiTree;
            
            Label noteLabel = uiDocument.rootVisualElement.Q<Label>("noteText");

            noteLabel.text = noteText;
        }
    }
}
