using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Missions : MonoBehaviour
{
    public bool Mission1 = false;
    public bool Mission2 = false;
    public bool Mission3 = false;
    public bool Mission4 = false;


    [SerializeField]
    public MissionData[] taskMissions; //해당 시나리오에서 존재하는 임무목록들

    [SerializeField]
    public GameObject[] MissionClearImages;

    [SerializeField]
    public Image MissionBg;
    [SerializeField]
    public Text missionText;
    [SerializeField]
    public TextMeshProUGUI missionDetail;
    [SerializeField]
    public string[] missionTitles;
    [TextArea]
    public string[] missionDetails;

    [SerializeField]
    public Animator[] MissionSpaceAnims;

    public bool IsOpen = false;
    
    public void GetMissionDetails()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            MissionBg.enabled = true;
            missionDetail.gameObject.SetActive(true);
        }
        else
        {
            IsOpen = false;
            MissionBg.enabled = false;
            missionDetail.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Mission1 == false && Mission2 == false && Mission3 == false && Mission4 == false)
        {
            //UI
            missionText.text = missionTitles[0];
            missionDetail.text = missionDetails[0];

            MissionSpaceAnims[0].SetBool("Active", true);
            MissionSpaceAnims[1].SetBool("Active", false);
            MissionSpaceAnims[2].SetBool("Active", false);
            MissionSpaceAnims[3].SetBool("Active", false);
        }
        if (Mission1 == true && Mission2 == false && Mission3 == false && Mission4 == false)
        {
            //UI
            missionText.text = missionTitles[1];
            missionDetail.text = missionDetails[1];

            MissionClearImages[0].SetActive(true);

            MissionSpaceAnims[0].SetBool("Active", false);
            MissionSpaceAnims[1].SetBool("Active", true);
            MissionSpaceAnims[2].SetBool("Active", false);
            MissionSpaceAnims[3].SetBool("Active", false);
        }
        if (Mission1 == true && Mission2 == true && Mission3 == false && Mission4 == false)
        {
            //UI
            missionText.text = missionTitles[2];
            missionDetail.text = missionDetails[2];

            MissionClearImages[1].SetActive(true);

            MissionSpaceAnims[0].SetBool("Active", false);
            MissionSpaceAnims[1].SetBool("Active", false);
            MissionSpaceAnims[2].SetBool("Active", true);
            MissionSpaceAnims[3].SetBool("Active", false);
        }
        if (Mission1 == true && Mission2 == true && Mission3 == true && Mission4 == false)
        {
            //UI
            missionText.text = missionTitles[3];
            missionDetail.text = missionDetails[3];

            MissionClearImages[2].SetActive(true);

            MissionSpaceAnims[0].SetBool("Active", false);
            MissionSpaceAnims[1].SetBool("Active", false);
            MissionSpaceAnims[2].SetBool("Active", false);
            MissionSpaceAnims[3].SetBool("Active", true);
        }
        if (Mission1 == true && Mission2 == true && Mission3 == true && Mission4 == true)
        {
            //UI
            missionText.text = missionTitles[4];
            missionDetail.text = missionDetails[4];

            MissionClearImages[3].SetActive(true);

            MissionSpaceAnims[0].SetBool("Active", false);
            MissionSpaceAnims[1].SetBool("Active", false);
            MissionSpaceAnims[2].SetBool("Active", false);
            MissionSpaceAnims[3].SetBool("Active", false);
        }
    }
}