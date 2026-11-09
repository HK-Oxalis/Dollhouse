using UnityEngine;
using UnityEngine.Events;

public class SpawnMonsterTrigger : MonoBehaviour
{
    [SerializeField] private float spawnChance = 0.4f;
    [SerializeField] private Transform spawnPoint;

    public UnityEvent<Vector3> onSpawnSuccessful;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        float roll = Random.value;

        if (roll < spawnChance)
        {
            onSpawnSuccessful.Invoke(spawnPoint.position);
        }
    }
}
