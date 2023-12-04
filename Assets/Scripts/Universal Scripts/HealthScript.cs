using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health = 100f;
    private CharAnimation animationScript;
    private EnemyMovement enemyMovement;
    private bool charDied;
    public bool isPlayer;
    private HealthUI healthUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        animationScript = GetComponentInChildren<CharAnimation>();
        if(isPlayer)
        {
            healthUI = GetComponent<HealthUI>();
        }
    }

    public void ApplyDamage(float damage, bool knockDown)
    {
        if(charDied)
        {
            return;
        }
        health -= damage;
        //display health UI
        if(isPlayer)
        {
            healthUI.DisplayHealth(health);
        }

        if(health <= 0f)
        {
            animationScript.Death();
            charDied = true;
            if(isPlayer)
            {
                GameObject.FindWithTag(Tags.ENEMY_TAG).GetComponent<EnemyMovement>().enabled = false;
            }
            return;
        }
        if(!isPlayer)
        {
            if(knockDown)
            {
                if(Random.Range(0,2) > 0)
                {
                    animationScript.KnockDown();
                }
                else
                {
                    if(Random.Range(0,3) > 1)
                    {
                        animationScript.Hit();
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
