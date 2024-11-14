using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coob : MonoBehaviour
{
    public GameObject Rule;

    private Quaternion initialRotation; // Начальный поворот объекта
    public float rotationThreshold = 50f; // Угол в градусах для проверки

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
        // Сохраняем начальный поворот объекта
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Рассчитываем угол между текущим и начальным поворотом
        float angleDifference = Quaternion.Angle(initialRotation, transform.rotation);

        // Проверяем, превышает ли разница в углах заданное значение
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
