using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LevelCompletedManager : MonoBehaviour
{
    [Header("Target Stats")]
    [SerializeField] private int targetCoins;
    [SerializeField] private int targetStars;
    [SerializeField] private int targetCrowns;

    private int currentCoins;
    private int currentStars;
    private int currentCrowns;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI starsText;
    [SerializeField] private TextMeshProUGUI crownsText;

    [Header("Settings")]
    [SerializeField] private float baseDuration = 1.5f; // total time each counter should roughly take
    [SerializeField] private int minStep = 1;           // smallest possible increment
    [SerializeField] private float minDelay = 0.01f;    // fastest possible delay

    //Start Counting Animation when LevelCompleted is open
    public void StartCounting()
    {
        sparklesParticles.gameObject.SetActive(true);
        starsParticles.gameObject.SetActive(true);

        
        currentCoins = 0;
        currentStars = 0;
        currentCrowns = 0;

        StartCoroutine(CountUp(coinsText, () => currentCoins, val => currentCoins = val, targetCoins));
        StartCoroutine(CountUp(starsText, () => currentStars, val => currentStars = val, targetStars));
        StartCoroutine(CountUp(crownsText, () => currentCrowns, val => currentCrowns = val, targetCrowns));
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
            value += step;
            if (value > target)
                value = target;

            setter(value);
            textElement.text = value.ToString();

            yield return new WaitForSeconds(Mathf.Max(minDelay, delay));
        }
    }
    
    //Close the Level Completed at the end of the disappearing animation
    public void CloseWinWindow()
    {
        UiManager.Instance.StartCounting(targetCoins, targetStars);
        sparklesParticles.Stop();
        starsParticles.Stop();
        sparklesParticles.gameObject.SetActive(false);
        starsParticles.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }
    
    //Particles Animations
    [SerializeField] private ParticleSystem sparklesParticles;
    [SerializeField] private ParticleSystem starsParticles;

    public void StartSparkles()
    {
        sparklesParticles.Play();
    }
    public void StartStars()
    {
        starsParticles.Play();
    }
}
