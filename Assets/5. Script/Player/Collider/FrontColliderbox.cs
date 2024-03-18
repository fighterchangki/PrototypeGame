using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class FrontColliderbox : MonoBehaviour
{
    public MultiAimConstraint headIK;
    public MultiAimConstraint bodyIK;
    public MultiAimConstraint rHnadIK;
    [SerializeField] LayerMask aimMask;
    public BoxCollider frontBoxCollider;
    public bool isWall;
    // Start is called before the first frame update
    void Start()
    {
        frontBoxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            headIK.weight = Mathf.Lerp(headIK.weight, 0, 5f * Time.deltaTime);
            bodyIK.weight = Mathf.Lerp(bodyIK.weight, 0, 5f * Time.deltaTime);
            rHnadIK.weight = Mathf.Lerp(rHnadIK.weight, 0, 3f * Time.deltaTime);
            isWall = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("∫Æ æ»¥Í¿”");
            headIK.weight = Mathf.Lerp(headIK.weight, 1, 50f * Time.deltaTime);
            bodyIK.weight = Mathf.Lerp(bodyIK.weight, 1, 50f * Time.deltaTime);
            rHnadIK.weight = Mathf.Lerp(rHnadIK.weight, 1, 50f * Time.deltaTime);
            isWall = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);

        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);
        gameObject.transform.position = ray.GetPoint(2);
        //if (Physics.Raycast(ray, out RaycastHit hit, 10, aimMask))
        //{
            
        //    //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, hit.point, 20 * Time.deltaTime);
        //}
        
    }
}
