using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coob : MonoBehaviour
{
    private Quaternion initialRotation; // Начальный поворот объекта
    public float rotationThreshold = 50f; // Угол в градусах для проверки

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
}
