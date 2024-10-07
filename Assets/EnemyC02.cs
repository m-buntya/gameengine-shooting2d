using UnityEngine;

public class EnemyC02 : MonoBehaviour
{
    public Entity_Enemy1 enemyData;  // ScriptableObject�̎Q��
    public GameObject EnemyBom;
    private int enemyHP;
    private float fireRate;
    private float nextFireTime = 0f;
    public GameObject explosionEffect;

    private void Start()
    {
        // ScriptableObject����f�[�^��ݒ肷��
        if (enemyData != null && enemyData.sheets.Count > 0)
        {
            var sheet = enemyData.sheets[0];  // 1�̃V�[�g���擾
            if (sheet.list.Count > 0)
            {
                var enemyParams = sheet.list[0];  // 1�̃p�����[�^���擾
                enemyHP = (int)enemyParams.HP;
                fireRate = (float)enemyParams.fireRate; ;
            }
            else
            {
                Debug.LogWarning("No enemy parameters found in the sheet.");
                // �f�t�H���g�l��ݒ肷��ꍇ
                enemyHP = 3;
                fireRate = 1.5f;
            }
        }
        else
        {
            Debug.LogWarning("No enemy data found or the sheet is missing.");
            // �f�t�H���g�l��ݒ肷��ꍇ
            enemyHP = 3;
            fireRate = 1.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �v���C���[�̒e�ɓ��������ꍇ
        if (collision.CompareTag("Bullet"))
        {
            // enemyHP�����������鏈��
            enemyHP--;
          

            // �G�l�~�[��HP��0�ɂȂ�����j�󂷂�
            if (enemyHP <= 0)
            {
                if (explosionEffect != null)
                {
                    Instantiate(explosionEffect, transform.position, Quaternion.identity);
                }
                Destroy(gameObject); // �G�l�~�[��j��
            }

            // �e���j�󂷂�
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ��莞�Ԃ��Ƃɒe�𔭎�
        if (Time.time >= nextFireTime)
        {
            Instantiate(EnemyBom, transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }
}
