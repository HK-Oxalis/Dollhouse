using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class WinGame : MonoBehaviour
{
    [SerializeField] private UIDocument winUI;

    private void Start()
    {
        winUI.rootVisualElement.visible = false;
    }

    public void Win()
    {
        winUI.rootVisualElement.visible = true;
        
        winUI.rootVisualElement.Q<Button>().clicked += EndGame;
        
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
