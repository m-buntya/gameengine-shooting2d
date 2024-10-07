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
            Destroy(other.gameObject); // 弾を消す
        }
    }
    void Update()
    {
        //スワイプによる移動処理
        if(Input.GetMouseButtonDown(0))
        {
            previousPos=Input.mousePosition;
           
        }
        if(Input.GetMouseButton(0))
        {
            //スワイプによる移動距離を取得
            currentPos=Input.mousePosition;
            float diffDistance=(currentPos.x-previousPos.x)/Screen.width*LOAD_WIDTH;
            diffDistance *= sensititivity;

            //次のローカルx座標を設定（画面外に出ないように）
            float newX = Mathf.Clamp(transform.localPosition.x + diffDistance, -MOVE_MAX, MOVE_MAX);
            transform.localPosition=new Vector3(newX, -2, 0);
            //タップ位置を更新
            previousPos =currentPos;
            //スワイプ中は一定時間ごとに弾を自動発射
            if(Time.time >= nextFireTime)
            {
                Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                nextFireTime= Time.time+fireRate;
            }
            
        }
    }

}


