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
    public Kran Kran;
    public TextMeshProUGUI DablCoefText;

    public Toggle TogglAudio;
    private bool isMuteAudio;
    public AudioSource A0;
    public AudioSource A1;
    public AudioSource A2;
    public AudioSource A3;
    public AudioSource A4;
    public AudioSource A5;
    public AudioSource A6;
    public AudioSource A7;
    public AudioSource A8;

    private void OnEnable()
    {
        EventManager.Colission += SetNewFloor;
        EventManager.EndGame += SetEndGame;
        EventManager.MuteAudio += AudioMute;
        EventManager.PlayAudio += AudioPlay;
    }
    private void OnDisable()
    {
        EventManager.Colission -= SetNewFloor;
        EventManager.EndGame -= SetEndGame;
        EventManager.MuteAudio -= AudioMute;
        EventManager.PlayAudio -= AudioPlay;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            Money = PlayerPrefs.GetInt("Money");
        }
        else
        {
            Money = 5000;
        }
        MoneyText.text = Money.ToString();
        CubeCount = 0;
        Slider.maxValue = 10;
        Slider.value = 0;
    }

    public void SetNewFloor()
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
            Kran.floor = floor;
            Kran.SetNewLvl();
        }
        PlayerPrefs.SetInt("Money", Money);
    }
    public void SetDabl(int coin)
    {
        Money = Money + coin;
        MoneyText.text = Money.ToString();
        PlayerPrefs.SetInt("Money", Money);
        DablCoefText.text =  ("DUBLE X" + coin/5).ToString();
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
    public bool Buy(int prece)
    {
        if(Money - prece >= 0)
        {
            Money = Money - prece;
            MoneyText.text = Money.ToString();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void PausaOnn()
    {
        Time.timeScale = 0;
    }
    public void PausaOff()
    {
        Time.timeScale = 1;
    }
    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }
    public void Audio()
    {
        if (isMuteAudio == false)
        {
            isMuteAudio = true;
            EventManager.DoMuteAudio();
            PlayerPrefs.SetInt("MuteAudio", 1);
        }
        else
        {
            isMuteAudio = false;
            EventManager.DoPlayAudio();
            PlayerPrefs.DeleteKey("MuteAudio");
        }
    }
    public void AudioMute()
    {
        A0.mute = true;
        A1.mute = true;
        A2.mute = true;
        A3.mute = true;
        A4.mute = true;
        A5.mute = true;
        A6.mute = true;
        A7.mute = true;
        A8.mute = true;
    }
    public void AudioPlay()
    {
        A0.mute = false;
        A1.mute = false;
        A2.mute = false;
        A4.mute = false;
        A5.mute = false;
        A6.mute = false;
        A7.mute = false;
        A8.mute = false;
    }
}
