using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missions : MonoBehaviour
{
    public bool Mission1 = false;
    public bool Mission2 = false;
    public bool Mission3 = false;
    public bool Mission4 = false;

    public Text missionText;

    [SerializeField]
    public MissionData[] taskMissions; //해당 시나리오에서 존재하는 임무목록들

    [SerializeField]
    public GameObject[] MissionClearImages;
    private void Update()
    {
        if (Mission1 == false && Mission2 == false && Mission3 == false && Mission4 == false)
        {
            //UI
            missionText.text = "A target remove!!";
        }
        if (Mission1 == true && Mission2 == false && Mission3 == false && Mission4 == false)
        {
            //UI
            missionText.text = "B target remove!!";
            MissionClearImages[0].SetActive(true);
        }
        if (Mission1 == true && Mission2 == true && Mission3 == false && Mission4 == false)
        {
            //UI
            missionText.text = "C target remove!!";
            MissionClearImages[1].SetActive(true);
        }
        if (Mission1 == true && Mission2 == true && Mission3 == true && Mission4 == false)
        {
            //UI
            missionText.text = "Boss Remove!!";
            MissionClearImages[2].SetActive(true);
        }
        if (Mission1 == true && Mission2 == true && Mission3 == true && Mission4 == true)
        {
            //UI
            missionText.text = "All missions completed successfully.";
            MissionClearImages[3].SetActive(true);
        }
    }
}