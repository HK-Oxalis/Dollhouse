using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Cursor = UnityEngine.Cursor;

public class PlayerSpotted : MonoBehaviour
{
    [SerializeField] private UIDocument deathUI;
    //Access the MonsterVision script so the event can be subscribed to
    private MonsterVision monsterScript;
    private void Start()
    {
        monsterScript = gameObject.GetComponent<MonsterVision>();
//Subscribe to MonsterSeesPlayer event
        monsterScript.MonsterSeesPlayer += OnPlayerSpotted;

        deathUI.rootVisualElement.visible = false;
    }

    private void OnPlayerSpotted()
    {
        //Right now, this message is used to show when the player has been spotted by the monster
        Debug.Log("You've been spotted!");
        deathUI.rootVisualElement.visible = true;
        
        deathUI.rootVisualElement.Q<Button>().RegisterCallback<ClickEvent>(evt => ReloadGame() );

        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;

    }

    public void ReloadGame()
    {
        Debug.Log("Reloading");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
