using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneChangeButton : MonoBehaviour
{
    [SerializeField] private int destinationSceneIndex;
    [SerializeField] private string buttonName;
    
    private UIDocument uiDocument;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();

        uiDocument.rootVisualElement.Q<Button>(buttonName).clicked += ChangeScene;
    }


    public void ChangeScene()
    {
        SceneManager.LoadScene(destinationSceneIndex);
    }
}
