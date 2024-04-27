using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private float maxHp = 100;
    [SerializeField] private float curHp = 100;
    
    [SerializeField] private PostProcessing postProcessing;
    [SerializeField] private RagdollManager ragdollManager;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MovementStateManager movementStateManager;
    [SerializeField] private AimStateManager aimStateManager;
    float hpBarP;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody= GetComponent<Rigidbody>();
        aimStateManager = GetComponent<AimStateManager>();
        movementStateManager = GetComponent<MovementStateManager>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ragdollManager = GameObject.Find("Root").GetComponent<RagdollManager>();
        postProcessing = GameObject.FindWithTag("Volume").GetComponent<PostProcessing>();
        hpBar.value = (float)curHp / (float)maxHp;
    }
    private void Update()
    {
        if (curHp <= 0)
        {
            Dead();
        }
    }
    // Update is called once per frame
    public void Damege(int damege)
    {
        if (curHp > 0)
        {
            curHp -= damege;
            StartCoroutine(postProcessing.TakeDamageEffect());
        }
        else
        {
            Dead();
            curHp = 0;
        }
        hpBarP = (float)curHp / (float)maxHp;
        HandleHp();
    }
    public void Dead()
    {
        movementStateManager.currentMoveSpeed = 0;
        movementStateManager.walkSpeed = 0;
        movementStateManager.walkbackSpeed = 0;
        movementStateManager.CrouchSpeed = 0;
        movementStateManager.CrouchbackSpeed = 0;
        movementStateManager.airSpeed = 0;
        movementStateManager.jumpForce = 0;
        animator.enabled = false;
        rigidbody.isKinematic = true;
        aimStateManager.enabled = false;
        ragdollManager.TriggerRagdoll();
        gameManager.DeadEnding();
    }
    private void HandleHp()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, (float)curHp / (float)maxHp,Time.deltaTime*10);
    }


}
