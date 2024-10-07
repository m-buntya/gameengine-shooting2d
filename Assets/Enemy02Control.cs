using UnityEngine;
using System.Collections;

public class Enemy02Control : MonoBehaviour
{
    public GameObject enemyPrefab;  // エネミーのプレハブ
    public Entity_Enemy1 enemyData; // エネミーデータ
    public int enemyIndex = 0;      // 使用するエネミーデータのインデックス

    private Vector2 screenBounds;   // 画面の境界
    private GameObject currentEnemy; // 現在のエネミー
    private float moveSpeedX;
    private float moveSpeedY;
    private int enemyHP;

    void Start()
    {
        // 画面の境界を取得
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // エネミーデータを取得
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
            // エネミーの出現位置
            Vector3 spawnPosition = new Vector3(-3, -1, 0); // 画面外（左下）から出現

            // エネミーを生成
            currentEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // エネミーの斜め方向への移動を開始（画面内に入る動作）
            yield return StartCoroutine(MoveEnemyIntoScreen(currentEnemy));

            // エネミーの斜めランダム移動を開始
            StartCoroutine(MoveEnemy(currentEnemy));

            // エネミーが破壊されるまで待機
            yield return StartCoroutine(CheckEnemyHP());

            // エネミーを破壊
            Destroy(currentEnemy);
        }
    }

    // エネミーが画面内に入るための斜め移動処理
    IEnumerator MoveEnemyIntoScreen(GameObject enemy)
    {
        float moveDuration = 2f;  // 2秒かけて画面内に入る
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            if (enemy == null) yield break;

            // 斜め方向に移動（X軸とY軸の移動速度）
            enemy.transform.Translate(new Vector3(moveSpeedX, moveSpeedY, 0) * Time.deltaTime);

            // 画面内に入ったらループ終了
            if (enemy.transform.position.x > -screenBounds.x && enemy.transform.position.y > -screenBounds.y)
            {
                break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    // エネミーがランダムに斜め方向に動く処理
    IEnumerator MoveEnemy(GameObject enemy)
    {
        float randomDirectionInterval = 1f;  // ランダムに方向を変える間隔

        while (enemy != null)
        {
            // ランダムな方向
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            Vector3 randomDirection = new Vector3(randomX, randomY, 0).normalized;

            float directionChangeElapsed = 0f;
            while (directionChangeElapsed < randomDirectionInterval)
            {
                if (enemy == null) yield break;

                // エネミーをランダム方向に移動
                enemy.transform.Translate(randomDirection * moveSpeedX * Time.deltaTime);

                // 移動範囲を制限（画面外に出ないように）
                float clampedX = Mathf.Clamp(enemy.transform.position.x, -screenBounds.x, screenBounds.x);
                float clampedY = Mathf.Clamp(enemy.transform.position.y, -screenBounds.y, screenBounds.y);
                enemy.transform.position = new Vector3(clampedX, clampedY, 0);

                directionChangeElapsed += Time.deltaTime;
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