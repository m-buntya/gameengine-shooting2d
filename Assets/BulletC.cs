using UnityEngine;
using System.Collections;

public class BulletC : MonoBehaviour
{
    public float bulletSpeed = 5f; // 弾の速度

    private void OnTriggerEnter2D(Collider2D other)
    {
        // エネミーの弾と衝突した場合、両方の弾を消す
        if (other.gameObject.tag == "EnemyBullet")
        {
            Destroy(other.gameObject); // エネミーの弾を消す
            Destroy(gameObject); // プレイヤーの弾も消す
        }
    }
    void Update()
    {
        transform.Translate(0, 0.1f, 0);
        if(transform.position.y > 5)
        {
            Destroy(gameObject);
        }
    }
}
