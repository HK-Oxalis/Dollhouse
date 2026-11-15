using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private UIDocument uiDocument;
    
    private Slider masterVolume;
    private Slider sfxVolume;
    private Slider musicVolume;
    
    private Button mainMenuButton;
    
    
    void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        
        masterVolume = uiDocument.rootVisualElement.Q<Slider>("MasterVolume");
        sfxVolume = uiDocument.rootVisualElement.Q<Slider>("SFXVolume");
        musicVolume = uiDocument.rootVisualElement.Q<Slider>("MusicVolume");
        
        mainMenuButton = uiDocument.rootVisualElement.Q<Button>("MainMenuButton");
        mainMenuButton.clicked += ChangeScene;
        
        Button closeButton = uiDocument.rootVisualElement.Q<Button>("CloseButton");
        closeButton.clicked += () =>
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        };

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenuButton.visible = false;
        }

        float outFloat;
        audioMixer.GetFloat("MasterVolume", out outFloat);
        masterVolume.value = outFloat;
        
        masterVolume.RegisterCallback<ChangeEvent<float>>((evt) =>
            audioMixer.SetFloat("MasterVolume", evt.newValue));
        
        audioMixer.GetFloat("SFXVolume", out outFloat);
        sfxVolume.value = outFloat;
        
        sfxVolume.RegisterCallback<ChangeEvent<float>>((evt) =>
            audioMixer.SetFloat("SFXVolume", evt.newValue));
        
        audioMixer.GetFloat("MusicVolume", out outFloat);
        musicVolume.value = outFloat;
        
        musicVolume.RegisterCallback<ChangeEvent<float>>((evt) => 
            audioMixer.SetFloat("MusicVolume", evt.newValue));
    }

    
    public void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }
}
