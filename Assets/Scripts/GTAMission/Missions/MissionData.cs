using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class MissionData : MonoBehaviour
{
    [SerializeField] public string matchingGroup;
    [SerializeField] public int matchingGroupCnt;
    [SerializeField] public int matchingGroupNowCnt;

    public float gatheringTime = 1.6f;
    private float startTime;

    [SerializeField] TMP_Text objcntText;
    [SerializeField] TMP_Text nowcntText;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime < gatheringTime)
        {
            matchingGroupCnt = GameObject.FindGameObjectsWithTag(matchingGroup).Length;
            objcntText.text = matchingGroupCnt.ToString();
        }
        else
        {
            Debug.Log($"프로그램실행후 ${gatheringTime} 이상 지난경우>>");
        }

        matchingGroupNowCnt= GameObject.FindGameObjectsWithTag(matchingGroup).Length;
        nowcntText.text = matchingGroupNowCnt.ToString();
    }
}
