using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTargeting_boolParam : MonoBehaviour
{
    [SerializeField] public TutorialBase targetTutorial;
    [SerializeField] private bool isCompletedDelete = false;

    [SerializeField] public GameObject SpriteRenderConversation;

    public LayerMask playerLayer;
    public bool IsChildAnimActives = false;
    public string BoolParameterNames;
    public Animator childAnim;
    public bool IsCheck_;

    private void Awake()
    {
        if (IsChildAnimActives)
        {
            for (int e = 0; e< GetComponentsInChildren<Animator>().Length; e++)
            {
                var item = GetComponentsInChildren<Animator>()[e];
                if(item != this)
                {
                    //�ڱ��ڽ��ΰ��� �ƴ�.
                    childAnim = item;
                }
            }
        }
        //StartCoroutine(ColliderChecker());
    }
    void Update()
    {
        var IsCheck = false;
        
        Collider[] hitElement = Physics.OverlapSphere(transform.position, 6, playerLayer);

        foreach (Collider Item in hitElement)
        {
            Debug.Log("NpcCharacter OverlapSphere CHECK [[MeleeHitinfo]]:" + Item.transform.name);

            if (Item.CompareTag("Player"))
            {
                IsCheck = true;
                IsCheck_ = IsCheck;

                if (targetTutorial != null)
                {
                    if (!targetTutorial.IsEnd)
                    {
                        targetTutorial.IsEnd = true;
                    }
                }
                break;
            }
        }        
    }
    public void LinearActiveConversation()
    {
        SpriteRenderConversation.SetActive(true);
    }
    public void SetTargetTut(TutorialBase target)
    {
        Debug.Log("TutorialTargeting_boolParam" + target.transform.name);
        targetTutorial = target;

        if (IsChildAnimActives)
        {
            childAnim.SetBool(BoolParameterNames, true);
        }
    }
    public void ActiveEffectClear()
    {
        if (IsChildAnimActives)
        {
            //var anim = GetComponentInChildren<Animator>();
            childAnim.SetBool(BoolParameterNames, false);
        }
        if (SpriteRenderConversation != null)
        {
            SpriteRenderConversation.SetActive(false);
        }
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
   /* private void OnTriggerEnter(Collider other)
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
    }*/
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
