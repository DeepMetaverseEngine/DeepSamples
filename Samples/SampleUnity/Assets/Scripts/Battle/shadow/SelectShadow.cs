using UnityEngine;

public class SelectShadow : MonoBehaviour
{

    public GameObject green;
    public GameObject red;
    public GameObject bule;

    private GameObject mSelect;

    void Start()
    {
        if (green != null)
            green.SetActive(false);
        if (red != null)
            red.SetActive(false);
        if (bule != null)
            bule.SetActive(false);
    }

    public void Init(bool selfForce)
    {
        if (selfForce)
        {
            mSelect = green;
            red.SetActive(false);
        }
        else
        {
            mSelect = red;
            green.SetActive(false);
        }
    }

    public void DoSelect(bool isSelect)
    {
        if (mSelect != null)
            mSelect.SetActive(isSelect);
    }

}
