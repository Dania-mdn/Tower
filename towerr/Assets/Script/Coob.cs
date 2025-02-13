using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coob : MonoBehaviour
{
    public GameObject Rule;
    private bool isReady = true;

    private Quaternion initialRotation; // Начальный поворот объекта
    public float rotationThreshold = 50f; // Угол в градусах для проверки

    public Transform targetCube; // Целевой объект для выравнивания
    public float smoothSpeed = 5f; // Скорость выравнивания

    public Animation Fixation;
    public AudioSource boom;
    public ParticleSystem PS;

    private void OnEnable()
    {
        EventManager.Colission += OffRule;
        EventManager.MuteAudio += AudioMute;
        EventManager.PlayAudio += AudioPlay;
    }
    private void OnDisable()
    {
        EventManager.Colission -= OffRule;
        EventManager.MuteAudio -= AudioMute;
        EventManager.PlayAudio -= AudioPlay;
    }

    void Start()
    {
        // Сохраняем начальный поворот объекта
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("MuteAudio") == 1)
        {
            AudioMute();
        }
        else
        {
            AudioPlay();
        }
        // Рассчитываем угол между текущим и начальным поворотом
        float angleDifference = Quaternion.Angle(initialRotation, transform.rotation);

        // Проверяем, превышает ли разница в углах заданное значение
        if (angleDifference >= rotationThreshold)
        {
            EventManager.DoEndGame();
        }

        if (targetCube != null)
        {
            // Текущая позиция
            Vector3 currentPosition = transform.position;

            // Целевая позиция (по X и Z как у targetCube, Y остается неизменным)
            Vector3 targetPosition = new Vector3(targetCube.position.x, currentPosition.y, targetCube.position.z);

            // Плавный переход
            transform.position = Vector3.Lerp(currentPosition, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
    public void Vertikal()
    {
        // Сбросить наклон объекта, оставив текущую позицию
        Vector3 currentPosition = transform.position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = currentPosition;
        Fixation.Play();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isReady)
        {
            boom.Play();
            if(PS != null)
                PS.Play();
            isReady = false;
            if (collision.gameObject.tag == "Respawn")
            {
                EventManager.DoEndGame();
            }
            else
            {
                EventManager.DoColission();
            }
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
    public void AudioMute()
    {
        boom.mute = true;
    }
    public void AudioPlay()
    {
        boom.mute = false;
    }
}
