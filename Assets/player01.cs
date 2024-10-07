using UnityEngine;
using System.Collections;
using System.Threading;

public class player01:MonoBehaviour
{
    public Entity_Enemy1 enemyData;
    public GameObject BulletPrefab;
    public HPBarController hpBarController;
    public float sensititivity = 1f;
    const float LOAD_WIDTH = 6f;
    const float MOVE_MAX = 2f;
    private float fireRate = 0.25f;
    private float nextFireTime = 0f;
    private Rigidbody2D rb;
    public int bulletDamage;

    Vector3 previousPos, currentPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        if(enemyData!=null&&enemyData.sheets.Count>0)
        {
            var sheet = enemyData.sheets[0];
            if(sheet.list.Count>0)
            {
                var enemyParams = sheet.list[0];
                bulletDamage = (int)enemyParams.ATK;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="EnemyBullet")
        {
           hpBarController.TakeDamage(bulletDamage);
            Destroy(other.gameObject); // �e������
        }
    }
    void Update()
    {
        //�X���C�v�ɂ��ړ�����
        if(Input.GetMouseButtonDown(0))
        {
            previousPos=Input.mousePosition;
           
        }
        if(Input.GetMouseButton(0))
        {
            //�X���C�v�ɂ��ړ��������擾
            currentPos=Input.mousePosition;
            float diffDistance=(currentPos.x-previousPos.x)/Screen.width*LOAD_WIDTH;
            diffDistance *= sensititivity;

            //���̃��[�J��x���W��ݒ�i��ʊO�ɏo�Ȃ��悤�Ɂj
            float newX = Mathf.Clamp(transform.localPosition.x + diffDistance, -MOVE_MAX, MOVE_MAX);
            transform.localPosition=new Vector3(newX, -2, 0);
            //�^�b�v�ʒu���X�V
            previousPos =currentPos;
            //�X���C�v���͈�莞�Ԃ��Ƃɒe����������
            if(Time.time >= nextFireTime)
            {
                Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                nextFireTime= Time.time+fireRate;
            }
            
        }
    }

}


