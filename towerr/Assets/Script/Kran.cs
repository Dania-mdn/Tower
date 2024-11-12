using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kran : MonoBehaviour
{
    public Transform Camera;

    public float speed = 0.05f;
    public GameObject KranGO;
    private Transform transformKran;
    public bool isStart = false;
    private int direction = 0;
    public Transform leftborder;
    public Transform rightborder;

    public GameObject CoobPrefab;
    public GameObject PositionNewFloor;
    public GameObject ReadyCoob;
    private Vector3 targetPosition;

    public Animation MoneyAnimation;

    private void OnEnable()
    {
        EventManager.Colission += SetMoveCamera;
    }
    private void OnDisable()
    {
        EventManager.Colission -= SetMoveCamera;
    }

    private void Start()
    {
        transformKran = KranGO.GetComponent<Transform>();
        targetPosition = transform.position;
    }
    private void Update()
    {
        if (isStart == true)
        {
            direction = -1;
            isStart = false;
        }

        if(leftborder.position.x > transformKran.position.x)
        { 
            direction = 1; 
            SetNewCoob();
        }
        else if (transformKran.position.x > rightborder.position.x)
        {
            direction = -1;
            SetNewCoob();
        }

        if (direction == -1)
        {
            transformKran.position = new Vector3(transformKran.position.x - speed, transformKran.position.y, transformKran.position.z);
        }
        else if(direction == 1)
        {
            transformKran.position = new Vector3(transformKran.position.x + speed, transformKran.position.y, transformKran.position.z);
        }

        if(transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.06f);
        }
    }
    public void Build()
    {
        ReadyCoob.transform.parent = null;
        ReadyCoob.GetComponent<Rigidbody>().isKinematic = false;
        ReadyCoob = null;
    }
    public void SetNewCoob()
    {
        if(ReadyCoob == null)
        {
            ReadyCoob = Instantiate(CoobPrefab, PositionNewFloor.transform.position, Quaternion.identity, PositionNewFloor.transform);
        }
    }
    public void SetMoveCamera()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        MoneyAnimation.Play();
    }
    public void SetStart() => isStart = true;
}
