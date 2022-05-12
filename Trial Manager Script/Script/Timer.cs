using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

public class Timer : MonoBehaviour{
    Stopwatch sw =new Stopwatch();
    public static Timer timer;


    // Start is called before the first frame update
    void Start(){
        if(timer && timer != this)
            Destroy(this);
        else
            timer = this;
    }

    public void startTimer(){
        sw.Start();
    }

    public float stopTimer(){
        sw.Stop();
        float res = sw.ElapsedMilliseconds;
        sw.Reset();

        return res;
    }

    public float getElapsedTime(){
        return sw.ElapsedMilliseconds;
    }

    public string currentTime(){
        string dateAndTimeVar = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        return dateAndTimeVar;
    }

    public string currentTime4Filepath(){
        string dateAndTimeVar = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        return dateAndTimeVar;
    }
}
