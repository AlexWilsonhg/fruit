using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 100;
    [SerializeField] private int rotateSpeed = 1;
    [SerializeField] private int maxPitch = 45;
    [SerializeField] private int minPitch = -45;
    [SerializeField] private Transform playerHead;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool enabled = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Disable()
    {
        enabled = false;
    }

    public void Enable()
    {
        enabled = true;
    }

    
    void Update()
    {
        if(enabled)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            moveDirection = (horizontal * transform.right + vertical * transform.forward).normalized;

            PitchAndYaw();
        }
    }

    void FixedUpdate()
    {
    	Move(moveDirection);
    }

    void Move(Vector3 moveDir)
    {
    	rb.MovePosition(transform.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    void PitchAndYaw()
    {
    	float pitch = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
    	float yaw = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;

    	playerHead.Rotate(-pitch, 0.0f, 0.0f);
    	transform.Rotate(0.0f, yaw, 0.0f);
    }
}
