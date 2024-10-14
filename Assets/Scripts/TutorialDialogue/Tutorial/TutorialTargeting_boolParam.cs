using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTargeting_boolParam : MonoBehaviour
{
    [SerializeField] public TutorialBase targetTutorial;
    [SerializeField] private bool isCompletedDelete = false;

    [SerializeField] public GameObject SpriteRenderConversation;
    private void Awake()
    {

    }
    public void LinearActiveConversation()
    {
        SpriteRenderConversation.SetActive(true);
    }
    public void SetTargetTut(TutorialBase target)
    {
        Debug.Log("TutorialTargeting_boolParam" + target.transform.name);
        targetTutorial = target;
    }
    
   public void OnClicked()
    {
        //Ŭ�������� Ÿ�� Ʃ�丮�� ������ true�� �� ���
        targetTutorial.IsEnd = true;

        if (isCompletedDelete)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (targetTutorial != null)
        {
            if (!targetTutorial.IsEnd)
            {
                if (other.CompareTag("Player"))
                {
                    targetTutorial.IsEnd = true;
                }
            }
        }
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
