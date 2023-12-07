using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public float power = 0.2f;
    public float duration = 0.2f;
    public float slowDownAmt = 1f;
    private bool shouldShake;
    private float initialDuration;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        initialDuration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
    }
    void Shake()
    {
        if (shouldShake)
        {
            if (duration > 0f)
            {
                transform.localPosition = startPos + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmt;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                transform.localPosition = startPos;
            }
        }
    }

    public bool ShouldShake
    {
        get
        {
            return shouldShake;
        }
        set
        {
            shouldShake = value;
        }
    }
}
