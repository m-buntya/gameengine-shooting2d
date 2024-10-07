using UnityEngine;
using UnityEngine.UI;
public class UltButton : MonoBehaviour
{
    public Image fillIImage;
    public float maxDamage = 20f;
    private float currentDamage = 0f;
    private bool isReady = false;

    private void Start()
    {
        // ������Ԃŏ���
        fillIImage.fillAmount = 0f;
        isReady = false;
    }
    public void AddDamage(float damage)
    {
      
        if (!isReady)
        {
            currentDamage += damage;
            fillIImage.fillAmount= currentDamage/maxDamage;
           

            if (currentDamage >= maxDamage )
            {
                isReady = true;
                fillIImage.fillAmount = 1f;
               
            }
        }
        
    }
    public void OnUltButtonPressd()
    {
       
        if (isReady)
        {
            Debug.Log("�K�E�Z���������܂���!");

            // �K�E�Z�̌��ʂ������ɏ���
            ExecuteUltimateAbility();
            //�J�E���g���Z�b�g
            isReady = false;
            currentDamage = 0f;
            fillIImage.fillAmount = 0f;
        }
       
    }
    private void ExecuteUltimateAbility()
    {
        // �K�E�Z�̋�̓I�Ȍ��ʂ������ɏ���
        Debug.Log("�����ŕK�E�Z�̌��ʂ����s���܂��B");

    }
}
