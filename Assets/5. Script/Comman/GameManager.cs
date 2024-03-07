using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] int enemyCount;
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
        enemyCountch = enemy.transform.childCount;
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
    void Update()
    {
        if (enemyCountch != enemy.transform.childCount)
        {
            enemyCountch = enemy.transform.childCount;
        }
    }
    
    // Update is called once per frame
    
}
