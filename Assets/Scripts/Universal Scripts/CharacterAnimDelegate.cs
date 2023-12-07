using System.Collections;
using UnityEngine;

public class CharacterAnimDelegate : MonoBehaviour
{
    public GameObject leftArmAtkPt, rightArmAtkPt, leftLegAtkPt, rightLegAtkPt;
    public float standUpTimer = 2f;
    public AudioClip whooshSound, fallSound, groundHitSound, deadSound;
    private CharAnimation animScript;
    private AudioSource audioSource;
    private EnemyMovement enemyMovement;
    private ShakeCamera shakeCamera;
    private void Awake()
    {
        shakeCamera = GameObject.FindWithTag(Tags.MAIN_CAMERA_TAG).GetComponent<ShakeCamera>();
        animScript = GetComponent<CharAnimation>();
        audioSource = GetComponent<AudioSource>();
        if (gameObject.CompareTag(Tags.ENEMY_TAG))
        {
            enemyMovement = GetComponentInParent<EnemyMovement>();
        }
    }
    void LeftArmAtkPtOn()
    {
        leftArmAtkPt.SetActive(true);
    }

    void LeftArmAtkPtOff()
    {
        if (leftArmAtkPt.activeInHierarchy)
        {
            leftArmAtkPt.SetActive(false);
        }
    }

    void RightArmAtkPtOn()
    {
        rightArmAtkPt.SetActive(true);
    }

    void RightArmAtkPtOff()
    {
        if (rightArmAtkPt.activeInHierarchy)
        {
            rightArmAtkPt.SetActive(false);
        }
    }

    void RightLegAtkPtOn()
    {
        rightLegAtkPt.SetActive(true);
    }

    void RightLegAtkPtOff()
    {
        if (rightLegAtkPt.activeInHierarchy)
        {
            rightLegAtkPt.SetActive(false);
        }
    }

    void LeftLegAtkPtOn()
    {
        leftLegAtkPt.SetActive(true);
    }

    void LeftLegAtkPtOff()
    {
        if (leftArmAtkPt.activeInHierarchy)
        {
            leftLegAtkPt.SetActive(false);
        }
    }

    void TagLeftArm()
    {
        leftArmAtkPt.tag = Tags.LEFT_ARM_TAG;
    }

    void UnTagLeftArm()
    {
        leftArmAtkPt.tag = Tags.UNTAGGED_TAG;
    }

    void TagLeftLeg()
    {
        leftLegAtkPt.tag = Tags.LEFT_LEG_TAG;
    }

    void UnTagLeftLeg()
    {
        leftLegAtkPt.tag = Tags.UNTAGGED_TAG;
    }

    void EnemyStandUp()
    {
        StartCoroutine(StandUpAfterTime());
    }

    IEnumerator StandUpAfterTime()
    {
        yield return new WaitForSeconds(standUpTimer);
        animScript.StandUp();
    }

    public void AttackFXSound()
    {
        audioSource.volume = 0.2f;
        audioSource.clip = whooshSound;
        audioSource.Play();
    }

    public void CharacterDiedSound()
    {
        // audioSource.volume = 1f;
        audioSource.clip = deadSound;
        audioSource.Play();
    }

    public void EnemyKnockedDown()
    {
        // audioSource.volume = 1f;
        audioSource.clip = fallSound;
        audioSource.Play();
    }

    public void EnemyHitGround()
    {
        // audioSource.volume = 1f;
        audioSource.clip = groundHitSound;
        audioSource.Play();
    }

    void DisableMovement()
    {
        // print("Disable Movement");
        enemyMovement.enabled = false;
        //set the enemy parent to default layer to avoid being continuously hit
        transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
    }
    void EnableMovement()
    {
        // print("Enable Movement");
        enemyMovement.enabled = true;
        //set the enemy parent to enemy layer to reactivate hit detection
        transform.parent.gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    void ShakeCameraOnFall()
    {
        shakeCamera.ShouldShake = true;
    }
    void CharacterDied()
    {
        //trigger game over        
        if (transform.parent.CompareTag(Tags.PLAYER_TAG))
        {
            GameManager.Instance.EndGame();
            return;
        }
        Invoke("DeactivateGameObj", 2f);
    }
    void DeactivateGameObj()
    {
        // gameObject.SetActive(false);
        foreach (GameObject enemyObj in GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG))
        {
            if ((enemyObj.GetComponent<HealthScript>() != null) &&
            enemyObj.GetComponent<HealthScript>().health <= 0f)
            {
                EnemyManager.instance.DestroyEnemy(enemyObj);
            }
            // enemyObj.SetActive(false);
        }
        //Spawn new enemy after one died
        EnemyManager.instance.GetEnemyInstance();
    }
}
