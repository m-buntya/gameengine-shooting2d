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
            Debug.Log("スキル発動！");
            //スキルの効果を描く　　　　　　　　　　　　　　　
            ExecuteUltimateAbility();
            //カウントリセット
            isReady = false;
            fillImage.fillAmount = 0f;
        }
    }
    private void ExecuteUltimateAbility()
    {
        // スキルの具体的な効果をここに書く
        Debug.Log("ここでスキルの効果を実行します。");

    }
}
