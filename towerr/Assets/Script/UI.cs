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

    public TextMeshProUGUI CubeCountEndGameText;
    public TextMeshProUGUI BestCubeCountText;
    public GameObject EndGame;

    public int floor = 0;

    private void OnEnable()
    {
        EventManager.Colission += SetNewFloor;
        EventManager.EndGame += SetEndGame;
    }
    private void OnDisable()
    {
        EventManager.Colission -= SetNewFloor;
        EventManager.EndGame -= SetEndGame;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            Money = PlayerPrefs.GetInt("Money");
        }
        else
        {
            Money = 0;
        }
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
        if(Slider.value < 10)
        {
            Slider.value = Slider.value + 1;
        }
        else
        {
            Slider.value = 0;
            floor++;
        }
        PlayerPrefs.SetInt("Money", Money);
    }
    private void SetEndGame()
    {
        EndGame.SetActive(true);
        CubeCountEndGameText.text = "Score: " + CubeCount.ToString();

        if (PlayerPrefs.HasKey("CubeCount"))
        {
            if (PlayerPrefs.GetInt("CubeCount") < CubeCount)
            {
                PlayerPrefs.SetInt("CubeCount", CubeCount);
            }
        }
        else
        {
            PlayerPrefs.SetInt("CubeCount", CubeCount);
        }

        BestCubeCountText.text = "BeastScore: " + PlayerPrefs.GetInt("CubeCount").ToString();
    }
}
