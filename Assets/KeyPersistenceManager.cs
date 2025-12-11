using UnityEngine;
using UnityEngine.AI;

public class KeyPersistenceManager : MonoBehaviour
{
    [Header("Setup")]
    public Transform targetObject;
    public float spawnRadius = 4.0f;

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
        PlayerPrefs.SetFloat("KeyX", targetObject.position.x);
        PlayerPrefs.SetFloat("KeyY", targetObject.position.y);
        PlayerPrefs.SetFloat("KeyZ", targetObject.position.z);
        PlayerPrefs.Save();
        Debug.Log("Saved Key position.");
    }

    void SpawnOnFloor()
    {
        Vector3 randomPoint = transform.position + (Random.insideUnitSphere * spawnRadius);
        
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, NavMesh.AllAreas))
        {
            targetObject.position = hit.position + Vector3.up * 0.2f;
            Debug.Log("Spawned Key aligned to Floor/NavMesh.");
        }
    }

    [ContextMenu("Delete Save Data")]
    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Save data cleared!");
    }
}