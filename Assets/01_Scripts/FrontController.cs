using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontController : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(Timer(5,0));
    }

    //타이머 시스템, mode 0:시계 mode 1: 손님 판단 타이머
     private IEnumerator Timer(int time, int mode)
    {
        while(time > 0)
        {
            Debug.Log(time);
            yield return new WaitForSeconds(1f);
            time--;
        } 
        switch(mode)
        {
            case 0:
                Clock();
                break;
            case 1:
                Out();
                break;
        }
    }

    private void Out()
    {
        Debug.Log("Out!");
    }

    private void Clock()
    {
        Debug.Log("Clock");
    }

}
