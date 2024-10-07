using UnityEngine;
using System.Collections;

public class Enemy01Contorol : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Entity_Enemy1 enemyData;
    public int enemyIndex = 0;

    private Vector2 screenBounds; // ��ʂ̋��E
    private GameObject currentEnemy;
    private bool isMovingX = false; 
    private bool isMovingY = false;
    private float moveSpeedX;
    private float moveSpeedY;
    private int enemyHP;
    void Start()
    {
        // ��ʂ̋��E���擾
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        //�G�l�~�[�f�[�^���擾
        if (enemyData != null && enemyData.sheets.Count > 0)
        {
            var enemyParam = enemyData.sheets[0].list[enemyIndex];  // �w�肵���C���f�b�N�X�̃G�l�~�[�f�[�^���擾
            moveSpeedX = (float)enemyParam.SpeedX;                  // X���̈ړ����x
            moveSpeedY = (float)enemyParam.SpeedY;                  // Y���̈ړ����x
            enemyHP = (int)enemyParam.HP;                           // �G�l�~�[��HP
        }
    
        // �ŏ��̃G�l�~�[�o��
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // �G�l�~�[�̃����_���ȏo���ʒu�i��ʊO�̏㕔�j
            float randomX = Random.Range(-screenBounds.x, screenBounds.x);
            Vector3 spawnPosition = new Vector3(randomX, screenBounds.y + 1, 0); // ��ʊO�i�㕔�j����o��

            // �G�l�~�[�𐶐�
            currentEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
          

            // �G�l�~�[��Y�������ւ̈ړ����J�n�i��ʓ��ɓ��铮��j
            yield return StartCoroutine(MoveEnemyIntoScreen(currentEnemy));

            // �G�l�~�[��X�������_���ړ����J�n
            isMovingX = true; // X���̈ړ����J�n
            StartCoroutine(MoveEnemy(currentEnemy));

            // �G�l�~�[���j�󂳂��܂őҋ@
            yield return StartCoroutine(CheckEnemyHP());

            // �G�l�~�[��j��
            Destroy(currentEnemy);
        }
    }

    // �G�l�~�[����ʓ��ɓ��邽�߂�Y���ړ�����
    IEnumerator MoveEnemyIntoScreen(GameObject enemy)
    {
        while (enemy != null && enemy.transform.position.y > screenBounds.y * 0.8f) // ��ʓ��ɓ���ʒu�܂ňړ�
        {
            enemy.transform.Translate(Vector3.down * moveSpeedY * Time.deltaTime);
            yield return null;
        }
    }

    // �G�l�~�[�������_����X�������ɓ�������

    IEnumerator MoveEnemy(GameObject enemy)
    {
        float randomDirection = Random.Range(-1f, 1f); // �����̃����_���Ȉړ�����

        while (enemy != null && isMovingX)
        {
            randomDirection = Random.Range(-1f, 1f);

            float elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                if (enemy == null) yield break;
                enemy.transform.Translate(new Vector3(randomDirection * moveSpeedX * Time.deltaTime, 0, 0));

                // �ړ��͈͂𐧌��i��ʊO�ɏo�Ȃ��悤�Ɂj
                float clampedX = Mathf.Clamp(enemy.transform.position.x, -screenBounds.x, screenBounds.x);
                enemy.transform.position = new Vector3(clampedX, enemy.transform.position.y, 0);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    // �G�l�~�[��HP���Ď����A0�ɂȂ����玟�̏�����
    IEnumerator CheckEnemyHP()
    {
        while (currentEnemy != null && enemyHP > 0)
        {
            yield return null;
        }
    }

}