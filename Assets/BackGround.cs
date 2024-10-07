using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    private const float k_maxLength = 1f;
    private const string k_propName = "_MainTex";

    [SerializeField]
    private Vector2 m_offsetspeed;
    private Material m_material;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if(GetComponent<Image>() is Image i)
        {
            m_material=i.material;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(m_material)
        {
            //xÇ∆yÇÃä‘Ç≈ÉäÉsÅ[Ég
            var x = Mathf.Repeat(Time.time * m_offsetspeed.x, k_maxLength);
            var y = Mathf.Repeat(Time.time * m_offsetspeed.y, k_maxLength);
            var offset=new Vector2(x, y);
            m_material.SetTextureOffset(k_propName, offset);
        }
    }

    private void OnDestroy()
    {
        if (m_material)
        {
            m_material.SetTextureOffset(k_propName,Vector2.zero);
        }
    }
}
