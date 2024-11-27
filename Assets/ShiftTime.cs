using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShiftTime : MonoBehaviour
{

    private TextMeshProUGUI timeText;
    private Canvas canvas;
    private void Awake()
    {
        timeText = GetComponentInChildren<TextMeshProUGUI>();
        canvas = GetComponent<Canvas>();
    }
    
    private float targetTime; // current time of timer
    private int night = 1; // what night it is currently
    
    // *---- OPTIONS ----* //
    
    [Tooltip("Length of each hour in seconds.")]
    [Range(30, 270)]
    [SerializeField] private int hourLength = 90;

    [Tooltip("Number of hours in a shift/night.")]
    [SerializeField] private int hours = 8;

    void Start()
    {
        targetTime = hourLength * hours;
    }
    
    void Update()
    {
        // Update clock display
        int dif = ((hourLength*hours)-(int)targetTime);
        if (dif % hourLength == 0)
        {
            timeText.text = (string.Format("{0:00}:00 AM", dif / hourLength));
        }
        
        // Timer goes down
        targetTime -= Time.deltaTime;
        
        //  Timer runs out
        if (targetTime<=0.0f)
        {
            EndDay();
        }
    }

    // Player lasted the shift successfully
    void EndDay()
    {
        Debug.Log("SHIFT OVER - SUCCESS");
        
    }
    
}
