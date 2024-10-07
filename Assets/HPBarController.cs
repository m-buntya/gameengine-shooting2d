using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class HPBarController : MonoBehaviour
{
    public Image hpBar; // HP�o�[��Image�R���|�[�l���g
    public int maxHP = 100; // �ő�HP
    private int currentHP; // ���݂�HP

    void Start()
    {
        // ����HP���ő�l�ɐݒ�
        currentHP = maxHP;
        UpdateHPBar();
    }

    // �_���[�W���󂯂��Ƃ��ɌĂяo��
    public void TakeDamage(int damage)
    {
        Debug.Log(damage);
        currentHP -= damage; // �_���[�W���󂯂�
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); // HP��0�����ɂȂ�Ȃ��悤�ɂ���
        UpdateHPBar(); // HP�o�[���X�V
 //HP0�ŉ�ʑJ��
     if(currentHP==0)
    {
         GameOver();
    }
    
    }
   
   

    // HP�o�[���X�V����
    private void UpdateHPBar()
    {
        // fillAmount��0.0����1.0�̊Ԃ̒l�ŕ\������
        hpBar.fillAmount = (float)currentHP / maxHP;
    }
    // �Q�[���I�[�o�[��ʂɑJ�ڂ��鏈��
    private void GameOver()
    {
        SceneManager.LoadScene("GameOverScene"); // �Q�[���I�[�o�[�V�[���ɑJ��
    }
}
