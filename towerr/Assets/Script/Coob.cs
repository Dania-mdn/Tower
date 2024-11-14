using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coob : MonoBehaviour
{
    public GameObject Rule;

    private Quaternion initialRotation; // ��������� ������� �������
    public float rotationThreshold = 50f; // ���� � �������� ��� ��������

    private void OnEnable()
    {
        EventManager.Colission += OffRule;
    }
    private void OnDisable()
    {
        EventManager.Colission += OffRule;
    }

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
    public void SetRule(bool active)
    {
        Rule.SetActive(active);
    }
    public void OffRule()
    {
        SetRule(false);
    }
}
