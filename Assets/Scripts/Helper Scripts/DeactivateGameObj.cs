using UnityEngine;

public class DeactivateGameObj : MonoBehaviour
{
    public float timer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateAfterTime", timer);
    }

    void DeactivateAfterTime()
    {
        gameObject.SetActive(false);
    }
}
