using UnityEngine;
using UnityEngine.Events;

public class SpawnMonsterTrigger : MonoBehaviour
{
    [SerializeField] private float spawnChance = 0.4f;

    public UnityEvent onSpawnSuccessful;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        float roll = Random.value;

        if (roll < spawnChance)
        {
            onSpawnSuccessful.Invoke();
        }
    }
}
