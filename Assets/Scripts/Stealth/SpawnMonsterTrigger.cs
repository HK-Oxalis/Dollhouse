using UnityEngine;
using UnityEngine.Events;

public class SpawnMonsterTrigger : MonoBehaviour
{
    [SerializeField] private float spawnChance = 0.4f;
    [SerializeField] private Transform spawnPoint;

    public UnityEvent<Vector3> onSpawnSuccessful;

    private float flashlightRisk = 0.5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        float roll = Random.value;

        float risk = other.GetComponentInChildren<PlayerFlashlight>().isOn ? flashlightRisk : 0;

        if (roll < spawnChance + risk)
        {
            onSpawnSuccessful.Invoke(spawnPoint.position);
        }
    }
}
