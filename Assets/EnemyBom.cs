using UnityEngine;

public class EnemyBom : MonoBehaviour
{
    public Entity_Enemy1 enemyData;  // ScriptableObject�̎Q��
    public float bulletSpeed = 5f;   // �e�̑��x
    public int bulletDamage;    // �e�̃_���[�W

    void Start()
    {
        // ScriptableObject����e�̑��x�ƃ_���[�W��ݒ肷��
        if (enemyData != null && enemyData.sheets.Count > 0)
        {
            var sheet = enemyData.sheets[0];  // �V�[�g���擾
            if (sheet.list.Count > 0)
            {
                var enemyParams = sheet.list[0];  // �p�����[�^���擾
                bulletSpeed = (float)enemyParams.BulletSpeed;  // ScriptableObject����BulletSpeed���擾
                bulletDamage = (int)enemyParams.ATK;           // ScriptableObject����ATK�i�_���[�W�j���擾
               
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
        // �e���ړ�������
        transform.Translate(0, -bulletSpeed * Time.deltaTime, 0);

        // ��ʊO�ɏo����e��j��
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �v���C���[�ɓ��������ꍇ�A�_���[�W��^����
        if (other.CompareTag("Player"))
        {
            // �v���C���[��HP�Ǘ��X�N���v�g�ɃA�N�Z�X���ă_���[�W��^����
            HPBarController playerHP = other.GetComponent<HPBarController>();
            if (playerHP != null)
            {
                Debug.Log(bulletDamage);
                playerHP.TakeDamage(bulletDamage);  // �_���[�W��^����
            }

            // �G�l�~�[�̒e��j��
            Destroy(gameObject);
        }

        // �v���C���[�̒e�ƏՓ˂����ꍇ�A�����̒e��j��
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject); // �v���C���[�̒e��j��
            Destroy(gameObject);       // �G�l�~�[�̒e��j��
        }
    }
}