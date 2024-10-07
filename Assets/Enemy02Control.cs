using UnityEngine;
using System.Collections;

public class Enemy02Control : MonoBehaviour
{
    public GameObject enemyPrefab;  // �G�l�~�[�̃v���n�u
    public Entity_Enemy1 enemyData; // �G�l�~�[�f�[�^
    public int enemyIndex = 0;      // �g�p����G�l�~�[�f�[�^�̃C���f�b�N�X

    private Vector2 screenBounds;   // ��ʂ̋��E
    private GameObject currentEnemy; // ���݂̃G�l�~�[
    private float moveSpeedX;
    private float moveSpeedY;
    private int enemyHP;

    void Start()
    {
        // ��ʂ̋��E���擾
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // �G�l�~�[�f�[�^���擾
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
            // �G�l�~�[�̏o���ʒu
            Vector3 spawnPosition = new Vector3(-3, -1, 0); // ��ʊO�i�����j����o��

            // �G�l�~�[�𐶐�
            currentEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // �G�l�~�[�̎΂ߕ����ւ̈ړ����J�n�i��ʓ��ɓ��铮��j
            yield return StartCoroutine(MoveEnemyIntoScreen(currentEnemy));

            // �G�l�~�[�̎΂߃����_���ړ����J�n
            StartCoroutine(MoveEnemy(currentEnemy));

            // �G�l�~�[���j�󂳂��܂őҋ@
            yield return StartCoroutine(CheckEnemyHP());

            // �G�l�~�[��j��
            Destroy(currentEnemy);
        }
    }

    // �G�l�~�[����ʓ��ɓ��邽�߂̎΂߈ړ�����
    IEnumerator MoveEnemyIntoScreen(GameObject enemy)
    {
        float moveDuration = 2f;  // 2�b�����ĉ�ʓ��ɓ���
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            if (enemy == null) yield break;

            // �΂ߕ����Ɉړ��iX����Y���̈ړ����x�j
            enemy.transform.Translate(new Vector3(moveSpeedX, moveSpeedY, 0) * Time.deltaTime);

            // ��ʓ��ɓ������烋�[�v�I��
            if (enemy.transform.position.x > -screenBounds.x && enemy.transform.position.y > -screenBounds.y)
            {
                break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    // �G�l�~�[�������_���Ɏ΂ߕ����ɓ�������
    IEnumerator MoveEnemy(GameObject enemy)
    {
        float randomDirectionInterval = 1f;  // �����_���ɕ�����ς���Ԋu

        while (enemy != null)
        {
            // �����_���ȕ���
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            Vector3 randomDirection = new Vector3(randomX, randomY, 0).normalized;

            float directionChangeElapsed = 0f;
            while (directionChangeElapsed < randomDirectionInterval)
            {
                if (enemy == null) yield break;

                // �G�l�~�[�������_�������Ɉړ�
                enemy.transform.Translate(randomDirection * moveSpeedX * Time.deltaTime);

                // �ړ��͈͂𐧌��i��ʊO�ɏo�Ȃ��悤�Ɂj
                float clampedX = Mathf.Clamp(enemy.transform.position.x, -screenBounds.x, screenBounds.x);
                float clampedY = Mathf.Clamp(enemy.transform.position.y, -screenBounds.y, screenBounds.y);
                enemy.transform.position = new Vector3(clampedX, clampedY, 0);

                directionChangeElapsed += Time.deltaTime;
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