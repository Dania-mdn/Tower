using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public int Money;
    public TextMeshProUGUI MoneyText;

    public int CubeCount;
    public TextMeshProUGUI CubeCountText;

    public Slider Slider;

    private void OnEnable()
    {
        EventManager.Colission += SetNewFloor;
    }
    private void OnDisable()
    {
        EventManager.Colission -= SetNewFloor;
    }

    private void Start()
    {
        Money = 0;
        MoneyText.text = Money.ToString();
        CubeCount = 0;
        Slider.maxValue = 10;
        Slider.value = 0;
    }

    private void SetNewFloor()
    {
        Money = Money + 5;
        MoneyText.text = Money.ToString();
        CubeCount = CubeCount + 1;
        CubeCountText.text = CubeCount.ToString();
        Slider.value = Slider.value + 1;
    }
}
