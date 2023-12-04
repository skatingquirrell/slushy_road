using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUniversal : MonoBehaviour
{
    public LayerMask collisionLayer;
    public float radius = 1f;
    public float damage = 2f;

    public bool isPlayer, isEnemy;
    public GameObject hitFXPrefab;

    // Start is called before the first frame update
    void Start()
    {

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
                // print("hit[0] transform forward vector" + hit[0].transform.forward);
                // if(hit[0].transform.forward.x > 0)
                // {
                //     hitFXPos.x += 0.3f;
                // }
                // else if(hit[0].transform.forward.x < 0)
                // {
                //     hitFXPos.x -= 0.3f;
                // }
                Instantiate(hitFXPrefab, hitFXPos, Quaternion.identity);

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
            if(isEnemy)
            {
                hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);
            }
            gameObject.SetActive(false);
        }
    }
}
