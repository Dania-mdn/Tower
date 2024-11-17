using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepVertical : MonoBehaviour
{
    void LateUpdate()
    {
        // —бросить наклон объекта, оставив текущую позицию
        Vector3 currentPosition = transform.position;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.position = currentPosition;
    }
}
