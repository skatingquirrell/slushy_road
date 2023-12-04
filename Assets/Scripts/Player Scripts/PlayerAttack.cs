using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboState
{
    NONE,
    PUNCH_1,
    PUNCH_2,
    PUNCH_3,
    KICK_1,
    KICK_2
}
public class PlayerAttack : MonoBehaviour
{
    private CharAnimation playerAnim;

    private bool activateTimerToReset;

    //input time limit for each move in a combo
    private float defaultComboTimer = 0.4f;

    //count down from the moment pressing the previous move in a combo
    private float currentComboTimer;

    private ComboState currentComboState;

    private void Awake()
    {
        playerAnim = GetComponentInChildren<CharAnimation>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        ComboAttacks();
        ResetComboState();
    }

    void ComboAttacks()
    {
        //punch and punch combo
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentComboState == ComboState.PUNCH_3 ||
                currentComboState == ComboState.KICK_1 ||
                currentComboState == ComboState.KICK_2)
            {
                return;
            }
            currentComboState++;
            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;

            if (currentComboState == ComboState.PUNCH_1)
            {
                playerAnim.Punch1();
            }

            if (currentComboState == ComboState.PUNCH_2)
            {
                playerAnim.Punch2();
            }

            if (currentComboState == ComboState.PUNCH_3)
            {
                playerAnim.Punch3();
            }
        }

        //kick and double kicks combo 
        if (Input.GetKeyDown(KeyCode.X))
        {
            // return since there is no combo to perform
            if (currentComboState == ComboState.KICK_2 ||
                currentComboState == ComboState.PUNCH_3)
            {
                return;
            }
            // there can be no more than 2 punches before a kick 1 for a combo, so chain kick 1 move to combo
            if (currentComboState == ComboState.NONE ||
                currentComboState == ComboState.PUNCH_1 ||
                currentComboState == ComboState.PUNCH_2)
            {
                currentComboState = ComboState.KICK_1;
            }
            // move to kick 2
            else if (currentComboState == ComboState.KICK_1)
            {
                currentComboState++;
            }

            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;

            if (currentComboState == ComboState.KICK_1)
            {
                playerAnim.Kick1();
            }

            if (currentComboState == ComboState.KICK_2)
            {
                playerAnim.Kick2();
            }
        }
    }

    void ResetComboState()
    {
        if (activateTimerToReset)
        {
            currentComboTimer -= Time.deltaTime;

            if (currentComboTimer <= 0f)
            {
                currentComboState = ComboState.NONE;
                activateTimerToReset = false;
                currentComboTimer = defaultComboTimer;
            }
        }
    }
}
