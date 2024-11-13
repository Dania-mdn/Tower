using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coob : MonoBehaviour
{
    private Quaternion initialRotation; // ��������� ������� �������
    public float rotationThreshold = 50f; // ���� � �������� ��� ��������

    void Start()
    {
        // ��������� ��������� ������� �������
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // ������������ ���� ����� ������� � ��������� ���������
        float angleDifference = Quaternion.Angle(initialRotation, transform.rotation);

        // ���������, ��������� �� ������� � ����� �������� ��������
        if (angleDifference >= rotationThreshold)
        {
            EventManager.DoEndGame();
        }
    }
}
