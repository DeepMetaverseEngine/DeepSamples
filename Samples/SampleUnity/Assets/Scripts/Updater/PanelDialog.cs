using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PanelDialog : MonoBehaviour {

    public Button btn_ok;
    public Button btn_cancel;
    public Text txt_title;
    public Text txt_message;

    public Action callbackOK;
    public Action callbackCancel;

    public static PanelDialog Create(string title, string message, Action cbOK, Action cbCancel = null)
    {
        var prefab = Resources.Load<PanelDialog>("ui/PanelDialog");
        var dialog = GameObject.Instantiate<PanelDialog>(prefab);

        dialog.txt_title.text = title;
        dialog.txt_message.text = message;
        dialog.callbackOK = cbOK;
        dialog.callbackCancel = cbCancel;
        dialog.btn_cancel.gameObject.SetActive(cbCancel != null);

        dialog.Show(GameObject.FindObjectOfType<Canvas>().transform);

        return dialog;
    }

    public void Show(Transform parent)
    {
        this.transform.SetParent(parent, false);
    }

	// Use this for initialization
	void Start () {
        btn_ok.onClick.AddListener(OnBtnOkClick);
        btn_cancel.onClick.AddListener(OnBtnCanelClick);
    }

    void OnBtnOkClick()
    {
        callbackOK();
        DeepCore.Unity3D.UnityHelper.Destroy(this.gameObject);
    }

    void OnBtnCanelClick()
    {
        callbackCancel();
        DeepCore.Unity3D.UnityHelper.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
