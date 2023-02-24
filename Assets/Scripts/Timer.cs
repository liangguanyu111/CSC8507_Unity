using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public float time;
    private Text timeText;

    private void Start()
    {
        timeText = this.GetComponent<Text>();
        StartCoroutine("timer");
    }
    private void Update()
    {
        int sec = (int)time % 60;
        int min = (int)time / 60;
        timeText.text = "Time: " + min.ToString() + ":" + sec.ToString();
    }
    IEnumerator timer()
    {
        time -= 1;
        yield return new WaitForSeconds(1);
        StartCoroutine("timer");
    }
}
