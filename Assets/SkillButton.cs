using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Image fillImage;
    public float cooldownTime = 10f;
    private float currentCooldownTime = 0f;
    private bool isReady = false;
   
    void Update()
    {
        if(!isReady)
        {
            currentCooldownTime += Time.deltaTime;
            fillImage.fillAmount = currentCooldownTime/cooldownTime;

            if(currentCooldownTime>=cooldownTime)
            {
                isReady = true;

                fillImage.fillAmount = 1f;
            }
        }
    }

    public void OnSkillButtonPressd()
    {
        if (isReady)
        {
            Debug.Log("�X�L�������I");
            //�X�L���̌��ʂ�`���@�@�@�@�@�@�@�@�@�@�@�@�@�@�@
            ExecuteUltimateAbility();
            //�J�E���g���Z�b�g
            isReady = false;
            fillImage.fillAmount = 0f;
        }
    }
    private void ExecuteUltimateAbility()
    {
        // �X�L���̋�̓I�Ȍ��ʂ������ɏ���
        Debug.Log("�����ŃX�L���̌��ʂ����s���܂��B");

    }
}
