using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayScript : MonoBehaviour
{
    public int day;
    public float dayUpdateSpeed = 1f;
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

    public IEnumerator DateCounter()
    {
        if (timerActive)
        {
            while (true)
            {
                yield return new WaitForSeconds(dayUpdateSpeed);
                Debug.Log("Day is " + day);
                if (year == leapYear)
                {
                    monthLength[1] = 29;
                    leapYear += 4;
                }
                else
                {
                    monthLength[1] = 28;
                }

                if (month == 11 && day == 31)
                {
                    year++;
                    month = 0;
                    day = 0;
                }

                if (day == (monthLength[month] + 1) && month != 11)
                {
                    Debug.Log("MONTH LENGTH IS: " + (monthLength[month] + 1));
                    day = 0;
                    month++;
                }
                day++;
                textBox.text = day.ToString() + " " + monthsArray[month] + ", " + year.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void timerButton()
    {
        timerActive = !timerActive;
        if (timerActive == true)
        {
            StartCoroutine("DateCounter");
        }
        if (timerActive == false)
        {
            StopCoroutine("DateCounter");
        }

        startBtnText.text = timerActive ? "Pause" : "Start";
    }

    public void moreSpeed()
    {
        if (dayUpdateSpeed > 0.06125f)
        {
            dayUpdateSpeed -= 0.06125f;
        }
    }

    public void lessSpeed()
    {
        if (dayUpdateSpeed < 2f)
        {
            dayUpdateSpeed += 0.125f;
        }
    }
}