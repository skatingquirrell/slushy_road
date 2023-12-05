using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public CharAnimation playerAnim; // Reference to the character animation script

    // Declare example combos
    private Combo[] exampleCombos = new Combo[]
    {
        new Combo("PUNCH_3", new KeyCode[] { KeyCode.Z, KeyCode.Z, KeyCode.Z }, new List<AnimationAction> { AnimationAction.Punch1, AnimationAction.Punch2, AnimationAction.Punch3 }),
        // new Combo("PUNCH_2", new KeyCode[] { KeyCode.Z, KeyCode.Z }, new List<AnimationAction> { AnimationAction.Punch1, AnimationAction.Punch2 }),
        // new Combo("PUNCH_1", new KeyCode[] { KeyCode.Z }, new List<AnimationAction> { AnimationAction.Punch1 }),
        new Combo("KICK_2", new KeyCode[] { KeyCode.X, KeyCode.X }, new List<AnimationAction> { AnimationAction.Kick1, AnimationAction.Kick2 }),
        // new Combo("KICK_1", new KeyCode[] { KeyCode.X }, new List<AnimationAction> { AnimationAction.Kick1 }),
        new Combo("PUNCH_KICK", new KeyCode[] { KeyCode.Z, KeyCode.X }, new List<AnimationAction> { AnimationAction.Punch1, AnimationAction.Kick1 }),
        new Combo("KICK_PUNCH_KICK", new KeyCode[] { KeyCode.X, KeyCode.Z, KeyCode.X }, new List<AnimationAction> { AnimationAction.Kick1, AnimationAction.Punch1, AnimationAction.Kick2 })
    };

    private List<AnimationAction> currentComboActions = new List<AnimationAction>(); // List to store the current combo actions
    private List<KeyCode> currentComboKeys = new List<KeyCode>(); // List to store the current combo actions

    private float comboInputTimeLimit = 0.5f; // Time limit for combo input in seconds
    private float currentComboInputTime = 0f; // Time elapsed since last combo input

    private void Awake()
    {
        playerAnim = GetComponentInChildren<CharAnimation>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            CheckComboInput();
        }

        // Decrease the current combo input time and reset the input buffer if the time limit is exceeded
        currentComboInputTime -= Time.deltaTime;
        if (currentComboInputTime <= 0f)
        {
            currentComboActions.Clear();
        }
    }

    private void CheckComboInput()
    {
        // Add the pressed key to the input buffer
        KeyCode pressedKey = Input.GetKeyDown(KeyCode.Z) ? KeyCode.Z : KeyCode.X;
        currentComboActions.Add(GetAnimationAction(pressedKey));
        currentComboKeys.Add(pressedKey);
                    Debug.Log("Combo executed: ");

        foreach(KeyCode key in currentComboKeys)
                    Debug.Log("key pressed: " + key);

        // Check if any combo matches the current input sequence
        foreach (Combo combo in exampleCombos)
        {
            if (combo.CheckInput(currentComboActions))
            {
                if (combo.inputSequence.Length == currentComboActions.Count)
                {
                    
                    PlayComboAnimation(combo.animationSequence);
                    Debug.Log("Combo executed: " + combo.name);

                    currentComboInputTime = comboInputTimeLimit;
                    currentComboActions.Clear();
                }
                return;
            }
        }

        // If no combo matches, clear the input buffer
        currentComboActions.Clear();

        currentComboInputTime = comboInputTimeLimit;
    }

    private void PlayComboAnimation(List<AnimationAction> animationActions)
    {
        foreach (AnimationAction action in animationActions)
        {
            switch (action)
            {
                case AnimationAction.Punch1:
                    playerAnim.Punch1();
                    break;
                case AnimationAction.Punch2:
                    playerAnim.Punch2();
                    break;
                case AnimationAction.Punch3:
                    playerAnim.Punch3();
                    break;
                case AnimationAction.Kick1:
                    playerAnim.Kick1();
                    break;
                case AnimationAction.Kick2:
                    playerAnim.Kick2();
                    break;
                // Add more cases for additional animation actions
            }
        }
    }

    private AnimationAction GetAnimationAction(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Z:
                return AnimationAction.Punch1;
            case KeyCode.X:
                return AnimationAction.Kick1;
            default:
                return AnimationAction.None;
        }
    }
}

public enum AnimationAction
{
    None,
    Punch1,
    Punch2,
    Punch3,
    Kick1,
    Kick2
}

public class Combo
{
    public string name; // Name of the combo
    public KeyCode[] inputSequence; // Array of keycodes representing the input sequence
    public List<AnimationAction> animationSequence; // List of animation actions to be played for the combo

    public Combo(string name, KeyCode[] inputSequence, List<AnimationAction> animationSequence)
    {
        this.name = name;
        this.inputSequence = inputSequence;
        this.animationSequence = animationSequence;
    }

    public bool CheckInput(List<AnimationAction> inputBuffer)
    {
        // Check if the input buffer matches the combo's animation sequence
        if (inputBuffer.Count < animationSequence.Count)
        {
            return false;
        }

        for (int i = 0; i < animationSequence.Count; i++)
        {
            if (inputBuffer[i] != animationSequence[i])
            {
                return false;
            }
        }

        return true;
    }
}

// public class ComboManager : MonoBehaviour {
//     public float comboTimeLimit = 1.5f; // Time limit to execute a combo
//     public KeyCode[] comboKeys; // Array of keys used in combos

//     private List<Combo> combos = new List<Combo>(); // List of available combos
//     private List<KeyCode> currentInput = new List<KeyCode>(); // Current input buffer
//     private float comboTimer; // Timer for combo execution

//     private void Start() {
//         // Declare example combos
//         Combo[] exampleCombos = new Combo[] {
//             new Combo("PUNCH_1", new KeyCode[] { KeyCode.Z }),
//             new Combo("PUNCH_2", new KeyCode[] { KeyCode.Z, KeyCode.Z }),
//             new Combo("PUNCH_3", new KeyCode[] { KeyCode.Z, KeyCode.Z, KeyCode.Z }),
//             new Combo("KICK_1", new KeyCode[] { KeyCode.X }),
//             new Combo("KICK_2", new KeyCode[] { KeyCode.X, KeyCode.X }),
//             new Combo("PUNCH_KICK", new KeyCode[] { KeyCode.Z, KeyCode.X }),
//             new Combo("KICK_PUNCH_KICK", new KeyCode[] { KeyCode.X, KeyCode.Z, KeyCode.X })
//         };

//         // Add example combos to the list of available combos
//         combos.AddRange(exampleCombos);
//     }

//     private void Update() {
//         // Check for combo input
//         foreach (KeyCode key in comboKeys) {
//             if (Input.GetKeyDown(key)) {
//                 currentInput.Add(key);
//                 comboTimer = comboTimeLimit;
//                 break;
//             }
//         }

//         // Decrement combo timer
//         if (comboTimer > 0f) {
//             comboTimer -= Time.deltaTime;
//         } else {
//                 Debug.Log("Combo executed: " + currentInput);

//             currentInput.Clear(); // Reset input buffer if time limit exceeded
//         }

//         // Check for matching combos
//         foreach (Combo combo in combos) {
//             if (combo.IsMatch(currentInput)) {
//                 Debug.Log("Combo executed: " + combo.name);
//                 currentInput.Clear(); // Reset input buffer on successful match
//                 break;
//             }
//         }
//     }
// }

// public class Combo {
//     public string name;
//     public KeyCode[] keys;

//     public Combo(string name, KeyCode[] keys) {
//         this.name = name;
//         this.keys = keys;
//     }

//     public bool IsMatch(List<KeyCode> input) {
//         if (input.Count < keys.Length) {
//             return false;
//         }

//         for (int i = 0; i < keys.Length; i++) {
//             if (input[input.Count - keys.Length + i] != keys[i]) {
//                 return false;
//             }
//         }

//         return true;
//     }
// }
