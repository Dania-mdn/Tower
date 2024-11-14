using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kran : MonoBehaviour
{
    public UI UI;

    public Transform Camera;

    public float speed = 0.05f;
    public GameObject KranGO;
    private Transform transformKran;
    public bool isStart = false;
    private int direction = 0;
    public Transform leftborder;
    public Transform rightborder;
    public int floor;

    public Animation Duble;
    public GameObject BazeCoob;
    public GameObject[] CoobPrefab;
    public GameObject PositionNewFloor;
    public GameObject ReadyCoob;
    private GameObject LastCoob;
    private Vector3 targetPosition;

    public Animation MoneyAnimation;
    public Animation CameraAnimation;

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
        LastCoob = BazeCoob;
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
            transformKran.position = new Vector3(transformKran.position.x - (speed * Time.deltaTime), transformKran.position.y, transformKran.position.z);
        }
        else if(direction == 1)
        {
            transformKran.position = new Vector3(transformKran.position.x + (speed * Time.deltaTime), transformKran.position.y, transformKran.position.z);
        }

        if(transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.06f);
        }
    }
    public void Build()
    {
        if (ReadyCoob != null)
        {
            if(Mathf.Abs(LastCoob.transform.position.x - ReadyCoob.transform.position.x) <= 0.1f)
            {
                Duble.Play();
                UI.SetNewFloor();
            }

            LastCoob = ReadyCoob;
            ReadyCoob.transform.parent = null;
            ReadyCoob.GetComponent<Rigidbody>().isKinematic = false;
            ReadyCoob = null;
        }
    }
    public void SetNewCoob()
    {
        if(ReadyCoob == null)
        {
            if(floor < CoobPrefab.Length-1)
            {
                ReadyCoob = Instantiate(CoobPrefab[floor], PositionNewFloor.transform.position, Quaternion.identity, PositionNewFloor.transform);
            }
            else
            {
                ReadyCoob = Instantiate(CoobPrefab[CoobPrefab.Length-1], PositionNewFloor.transform.position, Quaternion.identity, PositionNewFloor.transform);
            }
        }
    }
    public void SetMoveCamera()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        MoneyAnimation.Play();
        CameraAnimation.Play();
    }
    public void SetNewLvl()
    {
        LastCoob.GetComponent<Rigidbody>().isKinematic = true;
    }
    public void SetRule()
    {
        if (UI.Buy(200))
        {
            LastCoob.GetComponent<coob>().SetRule(true);
        }
    }
    public void SetHold()
    {

        if (UI.Buy(400))
        {

        }
    }
    public void SetFisxed()
    {
        if (UI.Buy(700))
        {
            LastCoob.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    public void SetTimeSlou()
    {
        if (UI.Buy(900))
        {
            speed = speed / 2;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void SetStart() => isStart = true;
}
