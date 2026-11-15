using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    
    private UIDocument uiDocument;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();

        uiDocument.rootVisualElement.Q<Button>("OptionsButton").clicked += () => optionsMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
