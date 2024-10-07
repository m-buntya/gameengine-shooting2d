using UnityEngine;
using System.Collections;

public class Enemy01Contorol : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Entity_Enemy1 enemyData;
    public int enemyIndex = 0;

    private Vector2 screenBounds; // 画面の境界
    private GameObject currentEnemy;
    private bool isMovingX = false; 
    private bool isMovingY = false;
    private float moveSpeedX;
    private float moveSpeedY;
    private int enemyHP;
    void Start()
    {
        // 画面の境界を取得
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        //エネミーデータを取得
        if (enemyData != null && enemyData.sheets.Count > 0)
        {
            var enemyParam = enemyData.sheets[0].list[enemyIndex];  // 指定したインデックスのエネミーデータを取得
            moveSpeedX = (float)enemyParam.SpeedX;                  // X軸の移動速度
            moveSpeedY = (float)enemyParam.SpeedY;                  // Y軸の移動速度
            enemyHP = (int)enemyParam.HP;                           // エネミーのHP
        }
    
        // 最初のエネミー出現
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // エネミーのランダムな出現位置（画面外の上部）
            float randomX = Random.Range(-screenBounds.x, screenBounds.x);
            Vector3 spawnPosition = new Vector3(randomX, screenBounds.y + 1, 0); // 画面外（上部）から出現

            // エネミーを生成
            currentEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
          

            // エネミーのY軸方向への移動を開始（画面内に入る動作）
            yield return StartCoroutine(MoveEnemyIntoScreen(currentEnemy));

            // エネミーのX軸ランダム移動を開始
            isMovingX = true; // X軸の移動を開始
            StartCoroutine(MoveEnemy(currentEnemy));

            // エネミーが破壊されるまで待機
            yield return StartCoroutine(CheckEnemyHP());

            // エネミーを破壊
            Destroy(currentEnemy);
        }
    }

    // エネミーが画面内に入るためのY軸移動処理
    IEnumerator MoveEnemyIntoScreen(GameObject enemy)
    {
        while (enemy != null && enemy.transform.position.y > screenBounds.y * 0.8f) // 画面内に入る位置まで移動
        {
            enemy.transform.Translate(Vector3.down * moveSpeedY * Time.deltaTime);
            yield return null;
        }
    }

    // エネミーがランダムにX軸方向に動く処理

    IEnumerator MoveEnemy(GameObject enemy)
    {
        float randomDirection = Random.Range(-1f, 1f); // 初期のランダムな移動方向

        while (enemy != null && isMovingX)
        {
            randomDirection = Random.Range(-1f, 1f);

            float elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                if (enemy == null) yield break;
                enemy.transform.Translate(new Vector3(randomDirection * moveSpeedX * Time.deltaTime, 0, 0));

                // 移動範囲を制限（画面外に出ないように）
                float clampedX = Mathf.Clamp(enemy.transform.position.x, -screenBounds.x, screenBounds.x);
                enemy.transform.position = new Vector3(clampedX, enemy.transform.position.y, 0);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    // エネミーのHPを監視し、0になったら次の処理へ
    IEnumerator CheckEnemyHP()
    {
        while (currentEnemy != null && enemyHP > 0)
        {
            yield return null;
        }
    }

}