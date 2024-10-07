using UnityEngine;
using System.Collections;

public class BulletC : MonoBehaviour
{
    public float bulletSpeed = 5f; // �e�̑��x

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �G�l�~�[�̒e�ƏՓ˂����ꍇ�A�����̒e������
        if (other.gameObject.tag == "EnemyBullet")
        {
            Destroy(other.gameObject); // �G�l�~�[�̒e������
            Destroy(gameObject); // �v���C���[�̒e������
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
