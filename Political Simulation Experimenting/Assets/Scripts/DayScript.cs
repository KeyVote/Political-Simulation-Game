using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayScript : MonoBehaviour
{
    public float day;
    public Text textBox;
    public Text startBtnText;

    bool timerActive = false;

    string[] monthsArray = {"January", "February", "March", "April", "May", "June",
                            "July", "August", "September", "October", "November", "December"};
    int[] monthLength = {31, 28, 31, 30, 31, 30,
                         31, 31, 30, 31, 30, 31};
    int month = 0;
    int year = 2019;
    int leapYear = 2020;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = day.ToString() + " January, " + "2019";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Calender()
    {
        if (timerActive)
        {
            day++;

            if (year == leapYear)
            {
                monthLength[1] = 29;
                leapYear += 4;
            } else {
                monthLength[1] = 28;
            }

            if (month == 11 && day == 32)
            {
                year++;
                month = 0;
            }

            if (day == (monthLength[month] + 1) && month != 11)
            {
                day = 1;
                month++;
            }
            textBox.text = day.ToString() + " " + monthsArray[month] + ", " + year.ToString();
        }
    }

    public void timerButton()
    {
        timerActive = !timerActive;
        if (timerActive == true)
        {
            InvokeRepeating("Calender", .01f, .25f);
        }
        if (timerActive == false)
        {
            CancelInvoke();
        }

        startBtnText.text = timerActive ? "Pause" : "Start";
    }
}