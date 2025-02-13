using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Toggle Musick;
    public static Action OnRewardedVideoSuccess0;

    void Awake()
    {
        GameDistribution.OnResumeGame += OnResumeGame;
        GameDistribution.OnPauseGame += OnPauseGame;
        GameDistribution.OnPreloadRewardedVideo += OnPreloadRewardedVideo;
        GameDistribution.OnRewardedVideoSuccess += OnRewardedVideoSuccess;
        GameDistribution.OnRewardedVideoFailure += OnRewardedVideoFailure;
        GameDistribution.OnRewardGame += OnRewardGame;
        GameDistribution.OnEvent += OnEvent;
    }

    public void OnResumeGame()
    {
        if (Musick.isOn == false)
        {
            EventManager.DoPlayAudio();
            PlayerPrefs.DeleteKey("MuteAudio");
        }
    }

    public void OnPauseGame()
    {
        EventManager.DoMuteAudio();
        PlayerPrefs.SetInt("MuteAudio", 1);
    }
    public void OnRewardGame()
    {

    }
    public void OnRewardedVideoSuccess()
    {

    }

    public void OnRewardedVideoFailure()
    {

    }

    public void OnPreloadRewardedVideo(int loaded)
    {
    
    }
    public void OnEvent(string eventData)
    {
    
    }

    public void ShowAd()
    {
        GameDistribution.Instance.ShowAd();
    }

    public void ShowRewardedAd()
    {
        GameDistribution.Instance.ShowRewardedAd();
    }

    public void PreloadRewardedAd()
    {
        GameDistribution.Instance.PreloadRewardedAd();
    }

    public void SendGameEvent()
    {
        //You can push your data here how ever you want 
        ////////////////////////          Example 1          ////////////////////////
        // int level = Int32.Parse(Regex.Replace(levelText, "[^0-9]", ""));
        // int score = Int32.Parse(Regex.Replace(scoreText, "[^0-9]", ""));
        // var obj = new EventData<GameEventData>();
        // var data = new GameEventData();
        // data.level = level;
        // data.score = score;
        // obj.data = data;
        // obj.eventName = "game_event";
        ////////////////////////          Example 2          ////////////////////////
        var obj = new EventData<MileStoneData>();
        var data = new MileStoneData();
        data.isAuthorized = true;
        data.milestoneDescription = "Test Description";
        obj.data = data;
        obj.eventName = "track-milestone";
        GameDistribution.Instance.SendEvent(JsonUtility.ToJson(obj));
    }
}
