using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;
public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public FadeInOut fadeInOut;
    [SerializeField] private UIManager uiManager;
    [SerializeField] int enemyCount;
    [SerializeField] public int vacinCount = 0;
    [SerializeField] public int totalvacinCount;
    [SerializeField] public PlayableDirector PlayableDirector;
    [SerializeField] public TimelineAsset timeline;
    [SerializeField] public CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] public GameObject EndingCamePos;
    [SerializeField] public Action endingAction;
    public int enemyCountch
    {
        get { return enemyCount; }
        set
        {
            if (enemyCount == value) return;
            enemyCount = value;
            FindEnemy();
        }
    }
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<GameObject> enemyArray = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //totalvacinCount = 5;
        player = GameObject.Find("Player");
        EndingCamePos = GameObject.Find("EndingCamePos");
        cinemachineVirtualCamera = GameObject.Find("CM_vcam1").GetComponent<CinemachineVirtualCamera>();
        PlayableDirector = GameObject.Find("Director").GetComponent<PlayableDirector>();
        fadeInOut = GameObject.Find("FadeImage").GetComponent<FadeInOut>();
        enemyCountch = enemy.transform.childCount;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void FindEnemy()
    {
        enemyArray.Clear();
        for (int i = 0; i < enemyCount; i++)
        {
            if (enemy.transform.GetChild(i).gameObject.layer != 8)
                return;
            enemyArray.Add(enemy.transform.GetChild(i).gameObject);
        }
    }
    public void Ending()
    {
        endingAction += StartTimeline;
        fadeInOut.FadeIn(2f, endingAction);
    }
    public void StartTimeline()
    {
        player.SetActive(false);
        cinemachineVirtualCamera.Follow = EndingCamePos.transform;
        cinemachineVirtualCamera.LookAt = EndingCamePos.transform;
        PlayableDirector.Play();
    }
    public void ReStartUIOn()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            // 자기 자신의 경우엔 무시 
            // (게임오브젝트명이 다 다르다고 가정했을 때 통하는 코드)
            if (child.name == transform.name)
                return;

            Debug.Log(child.name);
        }
    }
    void Update()
    {
        if (enemyCountch != enemy.transform.childCount)
        {
            enemyCountch = enemy.transform.childCount;
        }
        MouseHide();
    }
    void MouseHide()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    // Update is called once per frame

}
