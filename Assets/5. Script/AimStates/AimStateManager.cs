using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class AimStateManager : MonoBehaviour
{
    [Header("State")]
    public AimBaseState currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();
    [Header("Camera")]
    public Cinemachine.AxisState xAxis,yAxis;
    [SerializeField] Transform camFollowPos;
    [HideInInspector] public CinemachineVirtualCamera vCam;
    public float hipFov;
    public float currentFov;
    public float adsFov = 40;
    public float fovSmoothSpeed = 10;

    [HideInInspector] public Animator anim;
    [SerializeField] public Transform aimPos;
    [HideInInspector] public Vector3 actualAimPos;
    [SerializeField] float aimSmoothSpeed = 20;
    [SerializeField] LayerMask aimMask;
    [Header("StateCamera")]
    float xFollowPos;
    float yFollowPos, ogYPos;
    [SerializeField] float crouchCamHeight = 0.6f;
    [SerializeField] float shoulderSwapSpeed = 10;
    MovementStateManager moving;
    [Header("IK")]
    public TwoBoneIKConstraint lHandIk;
    [Header("Weapon")]
    public WeaponClassManager weaponClassManager;
    // Start is called before the first frame update
    void Start()
    {
        moving = GetComponent<MovementStateManager>();
        weaponClassManager = GetComponent<WeaponClassManager>();
        xFollowPos = camFollowPos.localPosition.x;
        ogYPos = camFollowPos.localPosition.y;
        yFollowPos = ogYPos;
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        anim = GetComponent<Animator>();
        hipFov = vCam.m_Lens.FieldOfView;
        SwitchState(Hip);
    }
    void AimController()//카메라 에임
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
    }
    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    // Update is called once per frame
    void Update()
    {
        AimController();
        
        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }
        MoveCamera();//카메라이동
        currentState.UpdateState(this);
    }
    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis.Value,camFollowPos.localEulerAngles.y,camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,xAxis.Value,transform.eulerAngles.z);
    }
    void MoveCamera()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))xFollowPos = -xFollowPos;//어깨바꿈
        if (moving.currentState == moving.Crouch) yFollowPos = crouchCamHeight;//앉을때 카메라 내림
        else yFollowPos = ogYPos;//y축을 원래로 돌림
        Vector3 newFollowPos = new Vector3(xFollowPos, yFollowPos, camFollowPos.localPosition.z);
        camFollowPos.localPosition = Vector3.Lerp(camFollowPos.localPosition, newFollowPos, shoulderSwapSpeed * Time.deltaTime);
    }
    
}
