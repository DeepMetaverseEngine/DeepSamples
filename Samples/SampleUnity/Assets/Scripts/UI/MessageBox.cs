using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class MessageBoxU : MonoBehaviour {

    public bool tips;
    public GameObject tipButton;
    public GameObject okButton;
    public GameObject cancelButton;
    public Text content;
    public Text title;

    private static GameObject mPrefab;
    static GameObject _instance = null;

    private System.Action mOkClick;
    private System.Action mCancelClick;
	void Start () 
    {
        if(mPrefab == null)
        {
            mPrefab = this.gameObject;
            this.gameObject.SetActive(false);
        }
        else
        {
            tipButton.SetActive(this.tips);
            okButton.SetActive(!this.tips);
            cancelButton.SetActive(!this.tips);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void OnOkClick()
    {
        _instance = null;
        if (mOkClick != null)
        {
            mOkClick.Invoke();
        }
        DeepCore.Unity3D.UnityHelper.Destroy(this.gameObject);
    }
   
    public void OnCancelClick()
    {
        _instance = null;
        if (mCancelClick != null)
        {
            mCancelClick.Invoke();
        }
        DeepCore.Unity3D.UnityHelper.Destroy(this.gameObject);  
    }

    public void OnTipsClick()
    {
        _instance = null;
        if (mOkClick != null)
        {
            mOkClick.Invoke();
        }
        DeepCore.Unity3D.UnityHelper.Destroy(this.gameObject);
    }

    public static MessageBoxU Create()
    {
        if (mPrefab != null)
        {
            GameObject obj = (GameObject)GameObject.Instantiate(mPrefab,
                new Vector3(0, 0, 0), Quaternion.identity);

            _instance = obj;
            obj.transform.SetParent(mPrefab.transform.parent);
            obj.transform.localScale = new Vector3(1, 1, 1);
            RectTransform rtf =  obj.GetComponent<RectTransform>();
            rtf.anchoredPosition = new Vector2(0, 0);
            obj.SetActive(true);
            return obj.GetComponent<MessageBoxU>();
        }
        else
        {
            return null;
        }
    }

    public static MessageBoxU ShowTips(string content, System.Action click = null)
    {
        return ShowTips(content, "", click);
    }

    public static MessageBoxU ShowConfirm(string content, System.Action okClick, System.Action cancelClick = null)
    {
        return ShowConfirm(content, "", okClick, cancelClick);
    }

    public static MessageBoxU ShowTips(string content, string title, System.Action click = null)
    {
        return ShowTips(content, title, "", click);
    }

    public static MessageBoxU ShowTips(string content, string title, string okstr, System.Action click = null)
    {
        if (_instance == null)
        {
            MessageBoxU box = Create();
            box.tips = true;
            box.content.text = content;
            box.title.text = title;
            box.mOkClick = click;
            box.tipButton.GetComponentInChildren<Text>().text = okstr;
            return box;
        }
        return null;
    }

    public static MessageBoxU ShowConfirm(string content, string title, System.Action okClick, System.Action cancelClick = null)
    {
        return ShowConfirm(content, title, "", "", okClick, cancelClick);
    }

    public static MessageBoxU ShowConfirm(string content, string title, string okstr, string cancelstr, System.Action okClick, System.Action cancelClick = null)
    {
        if (_instance == null)
        {
            MessageBoxU box = Create();
            box.tips = false;
            box.content.text = content;
            box.title.text = title;
            box.mOkClick = okClick;
            box.mCancelClick = cancelClick;
            box.okButton.GetComponentInChildren<Text>().text = okstr;
            box.cancelButton.GetComponentInChildren<Text>().text = cancelstr;
            return box;
        }
        return null;
    }

}
