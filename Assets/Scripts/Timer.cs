using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameEnding gameEnding;

    bool stop = false;

    // Update is called once per frame
    void Update()
    {
        if (stop) { return; }

        time -= Time.deltaTime;

        UpdateTimer();

        if (time <= 0) { gameEnding.TimeUp(); stop = true; }
    }

    public void UpdateTimer()
    {
        int currentTime = Mathf.RoundToInt(time);

        timerText.text = string.Format("{00:00}:{01:00}", (currentTime / 60), (currentTime % 60));
    }
}
