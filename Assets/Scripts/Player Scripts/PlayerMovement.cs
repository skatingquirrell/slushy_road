using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharAnimation playerAnim;
    private Rigidbody myBody;

    public float walkSpd = 3f;
    public float zSpd = 1.5f;

    private readonly float rotationY = -90f;
    // private float rotationSpd = 15f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        playerAnim = GetComponentInChildren<CharAnimation>();
        myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //print("The value is: " + Input.GetAxisRaw(Axis.HORIZONTAL_AXIS));
        RotatePlayer();
        AnimatePlayerWalk();
    }
    private void FixedUpdate()
    {
        DetectMovement();
    }

    void DetectMovement()
    {
        myBody.velocity = new Vector3(
            Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * (-walkSpd),
            myBody.velocity.y,
            Input.GetAxisRaw(Axis.VERTICAL_AXIS) * (-zSpd)
            );
    }

    void RotatePlayer()
    {
        if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > 0)
        {
            transform.rotation = Quaternion.Euler(0f, -Mathf.Abs(rotationY), 0f);
        }
        else if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) < 0)
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotationY), 0f);
        }
    }

    void AnimatePlayerWalk()
    {
        bool isWalking = Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) != 0 ||
                            Input.GetAxisRaw(Axis.VERTICAL_AXIS) != 0;
        playerAnim.Walk(isWalking);
    }
}
