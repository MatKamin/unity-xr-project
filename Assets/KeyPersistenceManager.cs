using UnityEngine;
using UnityEngine.AI;

public class KeyPersistenceManager : MonoBehaviour
{
    [Header("Setup")]
    public Transform targetObject; // The Key
    
    [Header("Spawn Settings")]
    [Tooltip("The closest the key can spawn to this object.")]
    public float minSpawnDistance = 2.0f; 
    
    [Tooltip("The furthest the key can spawn from this object.")]
    public float maxSpawnDistance = 6.0f;

    void Start()
    {
        if (PlayerPrefs.HasKey("KeyX"))
        {
            float x = PlayerPrefs.GetFloat("KeyX");
            float y = PlayerPrefs.GetFloat("KeyY");
            float z = PlayerPrefs.GetFloat("KeyZ");
            
            targetObject.position = new Vector3(x, y, z);
            Debug.Log("Loaded Key position from previous session.");
        }
        else
        {
            SpawnOnFloor();
        }
    }

    void OnApplicationQuit()
    {
        if (targetObject != null)
        {
            PlayerPrefs.SetFloat("KeyX", targetObject.position.x);
            PlayerPrefs.SetFloat("KeyY", targetObject.position.y);
            PlayerPrefs.SetFloat("KeyZ", targetObject.position.z);
            PlayerPrefs.Save();
            Debug.Log("Saved Key position.");
        }
    }

    void SpawnOnFloor()
    {
        // 1. Get a random direction (3D sphere direction)
        Vector3 randomDirection = Random.onUnitSphere;

        // 2. Pick a random distance strictly between Min and Max
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

        // 3. Calculate the target point relative to the Manager
        Vector3 randomPoint = transform.position + (randomDirection * randomDistance);
        
        NavMeshHit hit;
        // 4. Find the closest valid point on the Floor (NavMesh) within 10 units of that random point
        if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, NavMesh.AllAreas))
        {
            targetObject.position = hit.position + Vector3.up * 0.2f;
            Debug.Log($"Spawned Key at distance: {randomDistance:F2} (Target Range: {minSpawnDistance}-{maxSpawnDistance})");
        }
        else
        {
            Debug.LogWarning("Could not find a place on the NavMesh to spawn the key.");
        }
    }

    [ContextMenu("Delete Save Data")]
    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Save data cleared!");
    }
}