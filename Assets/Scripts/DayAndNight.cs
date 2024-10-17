using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond; //게임 세계의 100초 = 현실 세계의 1초

    [SerializeField] private float fogDensityCalc; //증감량 비율

    [SerializeField] private float nightFogDensity; //밤 상태의 Fog 밀도
    [SerializeField] private float dayFogDensity; //낮 상태의 Fog 밀도
    [SerializeField] private float currentFogDensity;
    [SerializeField] private float currentFogDensityCalc; //계산

    private float WaterFogDensityAmount = 0.2f;

    [SerializeField] private Color originNightColor; //밤 기본색상(바깥)

    [SerializeField] private float transformEulerAnglesX;

    public GameManager gamemanager;

    private void Start()
    {
        dayFogDensity = RenderSettings.fogDensity;
        currentFogDensity = dayFogDensity;
    }

    private void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime); //x축 계속 회전시킨다.

        transformEulerAnglesX = transform.eulerAngles.x;

        if (transform.eulerAngles.x >= 270 && transform.eulerAngles.x < 360)
            gamemanager.isNight = true;
        else if (transform.eulerAngles.x >= 0 && transform.eulerAngles.x < 90)
            gamemanager.isNight = false;

        if (gamemanager.isNight)
        {
            RenderSettings.fogColor = originNightColor;
            if(currentFogDensityCalc <= nightFogDensity)
            {
                //0.02~0.2범위 사이를 왔다갔다한다. 0.02~0.2까지 최대 증가시킴.
                currentFogDensityCalc += 0.1f * fogDensityCalc * Time.deltaTime;
                currentFogDensity = currentFogDensityCalc;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
        else
        {
            RenderSettings.fogColor = originNightColor;
            if(currentFogDensityCalc >= dayFogDensity)
            {
                //0.2~0.02까지 fog를 최소 0.02까지 감소시킴.
                currentFogDensityCalc -= 0.1f * fogDensityCalc * Time.deltaTime;
                currentFogDensity = currentFogDensityCalc;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
    }
}
