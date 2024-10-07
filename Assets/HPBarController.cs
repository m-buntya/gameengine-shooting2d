using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class HPBarController : MonoBehaviour
{
    public Image hpBar; // HPバーのImageコンポーネント
    public int maxHP = 100; // 最大HP
    private int currentHP; // 現在のHP

    void Start()
    {
        // 初期HPを最大値に設定
        currentHP = maxHP;
        UpdateHPBar();
    }

    // ダメージを受けたときに呼び出す
    public void TakeDamage(int damage)
    {
        Debug.Log(damage);
        currentHP -= damage; // ダメージを受ける
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); // HPが0未満にならないようにする
        UpdateHPBar(); // HPバーを更新
 //HP0で画面遷移
     if(currentHP==0)
    {
         GameOver();
    }
    
    }
   
   

    // HPバーを更新する
    private void UpdateHPBar()
    {
        // fillAmountは0.0から1.0の間の値で表現する
        hpBar.fillAmount = (float)currentHP / maxHP;
    }
    // ゲームオーバー画面に遷移する処理
    private void GameOver()
    {
        SceneManager.LoadScene("GameOverScene"); // ゲームオーバーシーンに遷移
    }
}
