using UnityEngine;

public class EnemyBom : MonoBehaviour
{
    public Entity_Enemy1 enemyData;  // ScriptableObjectの参照
    public float bulletSpeed = 5f;   // 弾の速度
    public int bulletDamage;    // 弾のダメージ

    void Start()
    {
        // ScriptableObjectから弾の速度とダメージを設定する
        if (enemyData != null && enemyData.sheets.Count > 0)
        {
            var sheet = enemyData.sheets[0];  // シートを取得
            if (sheet.list.Count > 0)
            {
                var enemyParams = sheet.list[0];  // パラメータを取得
                bulletSpeed = (float)enemyParams.BulletSpeed;  // ScriptableObjectからBulletSpeedを取得
                bulletDamage = (int)enemyParams.ATK;           // ScriptableObjectからATK（ダメージ）を取得
               
            }
            else
            {
                Debug.LogWarning("No enemy parameters found in the sheet.");
            }
        }
        else
        {
            Debug.LogWarning("No enemy data found or the sheet is missing.");
        }
    }

    void Update()
    {
        // 弾を移動させる
        transform.Translate(0, -bulletSpeed * Time.deltaTime, 0);

        // 画面外に出たら弾を破壊
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーに当たった場合、ダメージを与える
        if (other.CompareTag("Player"))
        {
            // プレイヤーのHP管理スクリプトにアクセスしてダメージを与える
            HPBarController playerHP = other.GetComponent<HPBarController>();
            if (playerHP != null)
            {
                Debug.Log(bulletDamage);
                playerHP.TakeDamage(bulletDamage);  // ダメージを与える
            }

            // エネミーの弾を破壊
            Destroy(gameObject);
        }

        // プレイヤーの弾と衝突した場合、両方の弾を破壊
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject); // プレイヤーの弾を破壊
            Destroy(gameObject);       // エネミーの弾を破壊
        }
    }
}