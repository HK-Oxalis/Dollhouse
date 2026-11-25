using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip normalMusic;
    [SerializeField] private AudioClip searchMusic;
    
    private AudioSource audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void OnMonsterStartSearch()
    {
        audioSource.generator = searchMusic;
        audioSource.Play();
    }
    
    public void OnMonsterEndSearch()
    {
        if (normalMusic)
        {
            audioSource.generator = normalMusic;
        }
        else
        {
            audioSource.Stop();
            audioSource.generator = null;
        }
    }
}
