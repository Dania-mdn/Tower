using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kran : MonoBehaviour
{
    public UI UI;

    public Transform Camera;

    public float speed;
    public GameObject KranGO;
    private Transform transformKran;
    public bool isStart = false;
    private int direction = 0;
    public Transform leftborder;
    public Transform rightborder;
    public int floor;
    private int DablCoef = 1;

    public GameObject BazeCoob;
    public GameObject[] CoobPrefab;
    public GameObject PositionNewFloor;
    public GameObject ReadyCoob;
    private GameObject LastCoob;
    private Vector3 targetPosition;
    private GameObject[] CoobArray;
    private GameObject FixedCoob;
    private bool isHold = false;

    public Animation Duble;
    public Animation MoneyAnimation;
    public Animation CameraAnimation;
    public Animation KranAnimation;

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
        CoobArray = new GameObject[10];
        FixedCoob = LastCoob;
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
            KranAnimation.Play("Scene");
            if (Mathf.Abs(LastCoob.transform.position.x - ReadyCoob.transform.position.x) <= 0.1f)
            {
                Duble.Play();
                UI.SetDabl(5 * DablCoef);
                DablCoef = DablCoef + 1;
            }
            else
            {
                DablCoef = 1;
            }

            LastCoob = ReadyCoob;
            ReadyCoob.transform.parent = null;
            ReadyCoob.GetComponent<Rigidbody>().isKinematic = false;
            ReadyCoob = null;

            for (int i = 0; i < CoobArray.Length; i++)
            {
                if(CoobArray[i] == null)
                {
                    CoobArray[i] = LastCoob;
                    return;
                }
            }

        }
    }
    public void SetNewCoob()
    {
        if(ReadyCoob == null)
        {
            if (floor < CoobPrefab.Length-1)
            {
                KranAnimation.Play("Scene 1");
                ReadyCoob = Instantiate(CoobPrefab[floor], PositionNewFloor.transform.position, Quaternion.identity, PositionNewFloor.transform);
            }
            else
            {
                KranAnimation.Play("Scene 1");
                ReadyCoob = Instantiate(CoobPrefab[CoobPrefab.Length-1], PositionNewFloor.transform.position, Quaternion.identity, PositionNewFloor.transform);
            }
        }
    }
    public void SetMoveCamera()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        MoneyAnimation.Play();
        CameraAnimation.Play();

        if(isHold == true)
        {
            for (int i = 0; i < CoobArray.Length; i++)
            {
                if(CoobArray[i] != null)
                    CoobArray[i].GetComponent<coob>().targetCube = null;
            }
            isHold = false;
        }
    }
    public void SetNewLvl()
    {
        LastCoob.GetComponent<coob>().Vertikal();
        LastCoob.GetComponent<Rigidbody>().isKinematic = true;
        speed = speed + (speed / 3);
        FixedCoob = LastCoob;

        for (int i = 0; i < CoobArray.Length; i++)
        {
            CoobArray[i] = null;
        }
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
            for (int i = 0; i < CoobArray.Length; i++)
            {
                if (CoobArray[i] != null)
                {
                    CoobArray[i].GetComponent<coob>().targetCube = FixedCoob.transform;
                }
            }
            isHold = true;
        }
    }
    public void SetFisxed()
    {
        if (UI.Buy(700))
        {
            LastCoob.GetComponent<coob>().Vertikal();
            LastCoob.GetComponent<Rigidbody>().isKinematic = true;
            FixedCoob = LastCoob;

            for (int i = 0; i < CoobArray.Length; i++)
            {
                CoobArray[i] = null;
            }
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
