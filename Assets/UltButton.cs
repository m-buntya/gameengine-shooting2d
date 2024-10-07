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
        // 初期状態で消灯
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
            Debug.Log("必殺技が発動しました!");

            // 必殺技の効果をここに書く
            ExecuteUltimateAbility();
            //カウントリセット
            isReady = false;
            currentDamage = 0f;
            fillIImage.fillAmount = 0f;
        }
       
    }
    private void ExecuteUltimateAbility()
    {
        // 必殺技の具体的な効果をここに書く
        Debug.Log("ここで必殺技の効果を実行します。");

    }
}
