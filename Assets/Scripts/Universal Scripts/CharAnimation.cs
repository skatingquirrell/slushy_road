using UnityEngine;

public class CharAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //CHARACTER ANIMATIONS
    public void Walk(bool move)
    {
        anim.SetBool(AnimationTags.MOVEMENT, move);
    }

    public void Punch1()
    {
        anim.SetTrigger(AnimationTags.PUNCH_1_TRIGGER);
    }

    public void Punch2()
    {
        anim.SetTrigger(AnimationTags.PUNCH_2_TRIGGER);
    }

    public void Punch3()
    {
        anim.SetTrigger(AnimationTags.PUNCH_3_TRIGGER);
    }

    public void Kick1()
    {
        anim.SetTrigger(AnimationTags.KICK_1_TRIGGER);
    }

    public void Kick2()
    {
        anim.SetTrigger(AnimationTags.KICK_2_TRIGGER);
    }

    // END OF CHARACTER ANIMATIONS

    // ENEMY ANIMATIONS
    public void EnemyAtk(int atk)
    {
        switch (atk)
        {
            case 0:
                anim.SetTrigger(AnimationTags.ATTACK_1_TRIGGER);
                break;
            case 1:
                anim.SetTrigger(AnimationTags.ATTACK_2_TRIGGER);
                break;
            case 2:
                anim.SetTrigger(AnimationTags.ATTACK_3_TRIGGER);
                break;
            default:
                break;
        }
    }

    public void PlayIdleAnim()
    {
        anim.Play(AnimationTags.IDLE);
    }

    public void KnockDown()
    {
        // Debug.Log(AnimationTags.KNOCK_DOWN_TRIGGER);
        anim.SetTrigger(AnimationTags.KNOCK_DOWN_TRIGGER);
    }
    public void StandUp()
    {
        // Debug.Log(AnimationTags.STAND_UP_TRIGGER);
        anim.SetTrigger(AnimationTags.STAND_UP_TRIGGER);
    }
    public void Hit()
    {
        anim.SetTrigger(AnimationTags.HIT_TRIGGER);
    }

    public void Death()
    {
        anim.SetTrigger(AnimationTags.DEATH_TRIGGER);
    }
}
