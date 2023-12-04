using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float atkDistance = 1f;
    public float speed = 1.8f;

    private CharAnimation enemyAnim;
    private Rigidbody myBody;

    private Transform playerTarget;
    private float chasePlayerAfterAtk = 1f;
    private float currentAtkTime;
    private readonly float defaultAtkTime = 2f;

    private bool followPlayer, atkPlayer;
    // private readonly float rotationY = -90f;
    // private float rotationSpd = 15f;

    private void Awake()
    {
        enemyAnim = GetComponentInChildren<CharAnimation>();
        myBody = GetComponent<Rigidbody>();

        playerTarget = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        followPlayer = true;
        atkPlayer = false;
        currentAtkTime = defaultAtkTime;
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
        Attack();
    }

    void FollowTarget()
    {
        if (!followPlayer)
        {
            return;
        }
        if (Vector3.Distance(transform.position, playerTarget.position) > atkDistance)
        {
            transform.LookAt(playerTarget);
            myBody.velocity = transform.forward * speed;

            if (myBody.velocity.sqrMagnitude != 0)
            {
                enemyAnim.Walk(true);
            }
        }
        else
        {
            myBody.velocity = Vector3.zero;
            enemyAnim.Walk(false);
            followPlayer = false;
            atkPlayer = true;
        }
    }

    void Attack()
    {
        if (!atkPlayer)
        {
            return;
        }

        enemyAnim.Walk(false);
        currentAtkTime += Time.deltaTime;
        if (currentAtkTime > defaultAtkTime)
        {
            enemyAnim.EnemyAtk(Random.Range(0, Constants.ENEMY_ATK_CNT));
            currentAtkTime = 0f;
        }
        if (Vector3.Distance(transform.position, playerTarget.position) > atkDistance + chasePlayerAfterAtk)
        {
            atkPlayer = false;
            followPlayer = true;
        }
    }
}
