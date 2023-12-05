using System.Collections.Generic;
using UnityEngine;

public class SFXObjectPool : MonoBehaviour
{
    public GameObject sfxPrefab; // Prefab of the particle system
    public int maxPoolSize = 5; // Maximum number of SFX instances in the pool

    private Queue<GameObject> sfxPool = new Queue<GameObject>(); // Queue to store SFX instances

    // private void Start()
    // {
    //     // Initialize the object pool
    //     for (int i = 0; i < maxPoolSize; i++)
    //     {
    //         GameObject sfxInstance = InstantiateSFX();
    //         sfxInstance.SetActive(false);
    //         sfxPool.Enqueue(sfxInstance);
    //     }
    // }

    public GameObject GetSFXInstance(Vector3 position, Quaternion rotation)
    {
        if (sfxPool.Count == 0)
        {
            // If the pool is empty, instantiate a new SFX instance
            GameObject sfxInstance = InstantiateSFX();
            sfxInstance.SetActive(false);
            sfxPool.Enqueue(sfxInstance);
        }
        // Debug.Log("SFX Pool current size: " + sfxPool.Count);

        // Dequeue a SFX instance from the pool and set its position and rotation
        GameObject pooledSFX = sfxPool.Dequeue();
        pooledSFX.transform.position = position;
        pooledSFX.transform.rotation = rotation;
        pooledSFX.SetActive(true);

        // Start a coroutine to check when the particle system has finished playing
        StartCoroutine(CheckSFXFinished(pooledSFX));

        return pooledSFX;
    }

    private System.Collections.IEnumerator CheckSFXFinished(GameObject sfxInstance)
    {
        ParticleSystem particleSystem = sfxInstance.GetComponentInChildren<ParticleSystem>();

        while (particleSystem.isPlaying)
        {
            yield return null;
        }

        ReturnSFXInstance(sfxInstance);
    }

    public void ReturnSFXInstance(GameObject sfxInstance)
    {
        // Reset the position and rotation of the returned SFX instance
        sfxInstance.transform.position = Vector3.zero;
        sfxInstance.transform.rotation = Quaternion.identity;
        sfxInstance.SetActive(false);

        // Enqueue the returned SFX instance back into the pool
        sfxPool.Enqueue(sfxInstance);
    }

    private GameObject InstantiateSFX()
    {
        // Instantiate a new SFX instance from the prefab
        return Instantiate(sfxPrefab, Vector3.zero, Quaternion.identity);
    }
}
