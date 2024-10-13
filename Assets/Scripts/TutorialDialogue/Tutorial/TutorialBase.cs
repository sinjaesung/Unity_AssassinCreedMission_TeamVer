using UnityEngine;

public abstract class TutorialBase : MonoBehaviour
{
    public bool IsEnd = false;
    //�ش� Ʃ�丮�� ������ ������ �� 1ȸ ȣ��
    public abstract void Enter();

    //�ش� Ʃ�丮�� ������ �����ϴ� ���� �� ������ ȣ��
    public abstract void Execute(TutorialController controller);

    //�ش� Ʃ�丮�� ������ ������ �� 1ȸ ȣ��
    public abstract void Exit();
}