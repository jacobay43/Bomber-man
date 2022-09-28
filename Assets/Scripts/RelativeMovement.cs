using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    private TheSceneManager SceneManager;
    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;
    private CharacterController charController;
    private Animator animator;
    private float horInput;
    private float vertInput;
    private float movementSqrd = 0f;
    private float gravity = -9.8f;
    private float vertSpeed = 0f;
    private float terminalVelocity = -10f;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<TheSceneManager>();
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.IsGameOver)
            return;
        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (horInput != 0 || vertInput != 0)
        {
            Vector3 right = target.right;
            Vector3 forward = Vector3.Cross(right, Vector3.up);
            movement = (right * horInput) + (forward * vertInput);
            movement *= moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);
            movementSqrd = movement.sqrMagnitude;

            movement *= Time.deltaTime;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }
        else
            movementSqrd = 0f;

        if (!charController.isGrounded)
        {
                vertSpeed += gravity * 5 * Time.deltaTime;
                if (vertSpeed < terminalVelocity)
                {
                    vertSpeed = terminalVelocity;
                }
         
            movement.y = vertSpeed;
        }


        charController.Move(movement);

        animator.SetFloat("Speed", movementSqrd);
        

    }
}
