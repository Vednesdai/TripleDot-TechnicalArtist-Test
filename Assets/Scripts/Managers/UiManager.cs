using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public event EventHandler OnSettingsOpen;
    public static UiManager Instance { get; private set; }

    [Header("PlayerStats")]
    [SerializeField] private int playerCoin;
    [SerializeField] private int playerHealth;
    [SerializeField] private int playerStars;
    
    [Header("PlayerStatsTexts")]
    [SerializeField] private TextMeshProUGUI playerCoinText;
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI playerStarsText;

    
    [Header("Settings")]
    [SerializeField] private GameObject settingsPopUp;
    [SerializeField] private GameObject settingsButton;
    
    [Header("Win Window")]
    [SerializeField] private GameObject winWindow;
    [SerializeField] private GameObject winButton;
    
    [Header("Settings Counting")]
    [SerializeField] private float baseDuration = 1.5f; // total time each counter should roughly take
    [SerializeField] private int minStep = 1;           // smallest possible increment
    [SerializeField] private float minDelay = 0.01f;    // fastest possible delay

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var culture = new CultureInfo("en-US"); // Forces commas for thousands

        playerCoinText.text = playerCoin.ToString("N0", culture);
        playerStarsText.text = playerStars.ToString("N0", culture);
    }
    private int GetPlayerStars()
    {
        return  playerStars;
    }
    private int GetPlayerCoin()
    {
        return  playerCoin;
    }
    private TextMeshProUGUI GetPlayerCoinText()
    {
        return  playerCoinText;
    }
    private TextMeshProUGUI GetPlayerStarsText()
    {
        return  playerStarsText;
    }
    public void OpenWinWindow()
    {
        winWindow.SetActive(true);
        winWindow.GetComponent<LevelCompletedManager>().StartCounting();
        winWindow.GetComponent<Animator>().Play("MissionCompleted_Appear");
    }
    public void CloseWinWindow()
    {
        winWindow.GetComponent<Animator>().Play("MissionCompleted_Disappear");
    }
    public void OpenSettings()
    {
        settingsPopUp.SetActive(true);
        OnSettingsOpen?.Invoke(this, EventArgs.Empty);
        settingsPopUp.GetComponent<Animator>().Play("Popup_Appear");
        settingsButton.GetComponent<Animator>().Play("SettingsButton_Pressed");
    }

    public void CloseSettings()
    {
        settingsPopUp.GetComponent<Animator>().Play("Popup_Disappear");

    }
    //Hovering Settings Button
    public void HoverButton(bool isHovering)
    {
        settingsButton.GetComponent<Animator>().Play(isHovering ? "SettingsButton_Hover" : "SettingsButton_Idle");
    }
    
    //Start Animation counting when earning gold and stars
    public void StartCounting(int targetCoins, int targetStars)
    {
        StartCoroutine(CountUp(GetPlayerCoinText(), () => playerCoin, val => playerCoin = val, GetPlayerCoin()+targetCoins));
        StartCoroutine(CountUp(GetPlayerStarsText(), () => playerStars, val => playerStars = val, GetPlayerStars()+targetStars));
    }
    private IEnumerator CountUp(TextMeshProUGUI textElement, Func<int> getter, Action<int> setter, int target)
    {
        int value = getter();
        if (target <= 0)
        {
            textElement.text = "0";
            yield break;
        }

        // Calculate step and delay based on the target value
        float stepsCount = Mathf.Clamp(target, 20, 200f); // Limit how many visual steps we’ll show
        float delay = baseDuration / stepsCount;
        int step = Mathf.Max(minStep, Mathf.CeilToInt(target / stepsCount));

        while (value < target)
        {
            if (value >= 9999)
            {
                break;
            }
            value += step;
            if (value > target)
                value = target;

            setter(value);
            textElement.text = value.ToString("###,###");

            yield return new WaitForSeconds(Mathf.Max(minDelay, delay));
        }
    }
}
