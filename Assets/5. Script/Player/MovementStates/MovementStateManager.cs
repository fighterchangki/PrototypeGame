using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    #region Movement
    public float currentMoveSpeed;
    public float walkSpeed, walkbackSpeed;
    public float CrouchSpeed, CrouchbackSpeed;
    public float airSpeed = 1.5f;
    public JumpState Jump = new JumpState();
    #endregion
    [HideInInspector]public Vector3 dir;
    public float hzInput, vInput;
    CharacterController controller;



    [SerializeField]float groundYOffset;
    [SerializeField]LayerMask groundMask = 3;
    Vector3 spherePos;
    [SerializeField]float gravity = -9.81f;
    [SerializeField] float jumpForce = 10;
    [HideInInspector] public bool jumped;
    Vector3 velocity;

    public MovementBaseState previousState;
    public MovementBaseState currentState;
    public IdleState Idle = new IdleState();
    public WalkState Walk= new WalkState();
    public CrouchState Crouch=new CrouchState();

    [HideInInspector] public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        //MouseHide();
        GetDirectionAndMove();
        Gravity();
        Falling();
        anim.SetFloat("hzInput", hzInput);
        anim.SetFloat("vInput", vInput);
        currentState.UpdateState(this);
    }
    public void SwitchState(MovementBaseState state)
    { 
        currentState = state;
        currentState.EnterState(this);
    }
    //마우스 커서 숨기는 함수
    
    //이동관련 스크립트
    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        Vector3 airDir = Vector3.zero;
        if(!IsGrounded())airDir = transform.forward * vInput + transform.right * hzInput;
        else dir = transform.forward * vInput + transform.right * hzInput;

        controller.Move((dir * currentMoveSpeed + airDir.normalized * airSpeed) * Time.deltaTime);
    }

    public bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y- groundYOffset, transform.position.z);
        if(Physics.CheckSphere(spherePos, controller.radius -0.05f, groundMask))return true;
        return false;
    }
    void Gravity()
    {
        if(!IsGrounded())velocity.y += gravity * Time.deltaTime;
        else if(velocity.y<0) velocity.y = -2f;

        controller.Move(velocity * Time.deltaTime);
    }
    void Falling() => anim.SetBool("Falling", !IsGrounded());
    public void JumpForce() => velocity.y += jumpForce;

    public void Jumped() => jumped = true;
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    //}

}
