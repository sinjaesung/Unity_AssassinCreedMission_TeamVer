using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond; //���� ������ 100�� = ���� ������ 1��

    [SerializeField] private float fogDensityCalc; //������ ����

    [SerializeField] private float nightFogDensity; //�� ������ Fog �е�
    [SerializeField] private float dayFogDensity; //�� ������ Fog �е�
    [SerializeField] private float currentFogDensity;
    [SerializeField] private float currentFogDensityCalc; //���

    private float WaterFogDensityAmount = 0.2f;

    [SerializeField] private Color originNightColor; //�� �⺻����(�ٱ�)

    [SerializeField] private float transformEulerAnglesX;

    public GameManager gamemanager;

    private void Start()
    {
        dayFogDensity = RenderSettings.fogDensity;
        currentFogDensity = dayFogDensity;
    }

    private void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime); //x�� ��� ȸ����Ų��.

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
                //0.02~0.2���� ���̸� �Դٰ����Ѵ�. 0.02~0.2���� �ִ� ������Ŵ.
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
                //0.2~0.02���� fog�� �ּ� 0.02���� ���ҽ�Ŵ.
                currentFogDensityCalc -= 0.1f * fogDensityCalc * Time.deltaTime;
                currentFogDensity = currentFogDensityCalc;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
    }
}
