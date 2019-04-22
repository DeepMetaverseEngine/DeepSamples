using UnityEngine;
using UnityEngine.UI;

public class TextPic : MonoBehaviour {

    public Text m_text = null;
    public Image m_image = null;

    void Start()
    {
        //this.gameObject.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
    }

    public void setXmlText(string str, bool isPlayer)
    {

        string nameTitle = "";
        string picTitle = "";
        string[] titleArray = str.Split(';');
        if (titleArray.Length == 2)
        {
            if (!string.IsNullOrEmpty(titleArray[0]))
                nameTitle = titleArray[0];
            if (!string.IsNullOrEmpty(titleArray[1]))
                picTitle = titleArray[1];
        }

        if (nameTitle.Length <= 0 || picTitle.Length <= 0)
        {
            return;
        }
        int index = int.Parse(picTitle);

       
        string color1 = "";

        if (isPlayer)
            color1 = StaticConfig.main.GetString("unit_playertitlecolor", "#15ecffff");
        else
            color1 = StaticConfig.main.GetString("unit_titlecolor", "#5aceffff");

        string color2 = StaticConfig.main.GetString("unit_titlecolorB", "#2a2a2aff");

        //系统字称号
        if (index == -1)
        {
            m_text.text = nameTitle;
            Color tempC = GameUtil.HexToColor(color1);
            m_text.color = tempC;
            m_text.gameObject.SetActive(true);
            m_image.sprite = null;
        }
        //图片称号
        else
        {
            GameUtil.ConvertToUnityUISpriteFromAtlas(m_image, "#chenghao_fox/output/chenghao.xml|chenghao_cbb|" + index);
            
            m_text.text = "";
        }
    }

    public void initHonor()
    {

    }
    public void clearData()
    {
        m_text.text = "";
        m_image.sprite = null;

    }

    public void ShowNameAndImage(bool show , bool isClear)
    {
        if (show == false)
        {
            if (isClear == true)
            {
                m_text.text = "";
                m_image.sprite = null;
            }
            
            m_text.gameObject.SetActive(show);
            m_image.gameObject.SetActive(show);
        }
        else
        {
            if (m_text.text.Length > 0)
            {
                m_text.gameObject.SetActive(true);
                
            }
            else
            {
                m_text.gameObject.SetActive(false);
            }

            if (m_image.sprite != null)
            {
                m_image.gameObject.SetActive(true);
            }
            else
            {
                m_image.gameObject.SetActive(false);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
