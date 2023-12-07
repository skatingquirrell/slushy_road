using UnityEngine;

public class AttackUniversal : MonoBehaviour
{
    public SFXObjectPool hitSFXObjectPool;
    public LayerMask collisionLayer;
    public float radius = 1f;
    public float damage = 2f;

    public bool isPlayer, isEnemy;
    public GameObject hitFXPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        hitSFXObjectPool = FindAnyObjectByType<SFXObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectCollision();
    }

    void DetectCollision()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, collisionLayer);
        if (hit.Length > 0)
        {
            if (isPlayer)
            {
                Vector3 hitFXPos = hit[0].transform.position;
                hitFXPos.y += 1.3f;
                hitSFXObjectPool.GetSFXInstance(hitFXPos, Quaternion.identity);

                if (gameObject.CompareTag(Tags.LEFT_ARM_TAG) ||
                    gameObject.CompareTag(Tags.LEFT_LEG_TAG))
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, true);
                }
                else
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);
                }
            }
            if (isEnemy)
            {
                hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);
            }
            gameObject.SetActive(false);
        }
    }
}
