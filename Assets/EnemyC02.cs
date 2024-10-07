using UnityEngine;

public class EnemyC02 : MonoBehaviour
{
    public Entity_Enemy1 enemyData;  // ScriptableObjectの参照
    public GameObject EnemyBom;
    private int enemyHP;
    private float fireRate;
    private float nextFireTime = 0f;
    public GameObject explosionEffect;

    private void Start()
    {
        // ScriptableObjectからデータを設定する
        if (enemyData != null && enemyData.sheets.Count > 0)
        {
            var sheet = enemyData.sheets[0];  // 1つのシートを取得
            if (sheet.list.Count > 0)
            {
                var enemyParams = sheet.list[0];  // 1つのパラメータを取得
                enemyHP = (int)enemyParams.HP;
                fireRate = (float)enemyParams.fireRate; ;
            }
            else
            {
                Debug.LogWarning("No enemy parameters found in the sheet.");
                // デフォルト値を設定する場合
                enemyHP = 3;
                fireRate = 1.5f;
            }
        }
        else
        {
            Debug.LogWarning("No enemy data found or the sheet is missing.");
            // デフォルト値を設定する場合
            enemyHP = 3;
            fireRate = 1.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーの弾に当たった場合
        if (collision.CompareTag("Bullet"))
        {
            // enemyHPを減少させる処理
            enemyHP--;
          

            // エネミーのHPが0になったら破壊する
            if (enemyHP <= 0)
            {
                if (explosionEffect != null)
                {
                    Instantiate(explosionEffect, transform.position, Quaternion.identity);
                }
                Destroy(gameObject); // エネミーを破壊
            }

            // 弾も破壊する
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 一定時間ごとに弾を発射
        if (Time.time >= nextFireTime)
        {
            Instantiate(EnemyBom, transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }
}
