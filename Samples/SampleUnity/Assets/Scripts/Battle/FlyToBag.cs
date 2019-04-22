using System;
using DeepCore.Unity3D.UGUIEditor.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyToBag : MonoBehaviour
{
    private static Transform _canvas;
    private HZCanvas _cvsReward1;
    private HZCanvas _cvsReward2;
    private int _bounsReward;
    
    private float _t = 1;
    private float _moveSpeed = 1;
    private bool _start = false;
    private Vector3 _startPosition;

    private void OnEnable()
    {
        var dailyHud = MenuMgr.Instance.FindMenuByTag("DailyDungeonHud");
        if (dailyHud != null)
        {
            var reward =GameUtil.GetDBData2("DailyDungeonReward","{group_id ="+GameSceneMgr.Instance.GroupId+"}");
            foreach (var rew in reward)
            {
                object min = 0;
                object max = 0;
                rew.TryGetValue("kill_min", out min);
                rew.TryGetValue("kill_max", out max);
                if (Convert.ToInt32(min) <= GameSceneMgr.Instance.Count && GameSceneMgr.Instance.Count <= Convert.ToInt32(max))
                {
                    _bounsReward = Convert.ToInt32(rew["isbouns"]);
                    break;
                }
            }
            _cvsReward1 = dailyHud.FindChildByEditName<HZCanvas>("cvs_reward1");
            _cvsReward2 = dailyHud.FindChildByEditName<HZCanvas>("cvs_reward2");
            _canvas =_bounsReward > 0 ? _cvsReward2.Transform :_cvsReward1.Transform;
        }
    }
    
    private void Update()
    {
        if (_canvas == null)
        {
            _finishCb.Invoke(gameObject);
            return;
        }
        
        if (_start)
        {
            if (_t < 1)
            {
                _t += Time.deltaTime * _moveSpeed;
                //_canvas.position.x+0.23f
                //_canvas.position.y-0.16f
                var pos = Vector3.Slerp(_startPosition,new Vector3(_canvas.position.x+0.1f,_canvas.position.y-0.1f,_canvas.position.z), _t);
                transform.position = pos;
            }
            else
            {
                _start = false;
                if (_finishCb != null)
                {
                    _finishCb.Invoke(gameObject);
                }
            }
        }
    }

    public delegate void FinishCallback(GameObject eff);
    private FinishCallback _finishCb = null;

    public void Fly(FinishCallback callback)
    {
        _finishCb = callback;
        
        if (_canvas == null)
        {
            _finishCb.Invoke(gameObject);
            return;
        }
        
        _t = 0;
        _moveSpeed = Random.Range(0.5f,1.5f);
        _startPosition = transform.position;
        _start = true;
        transform.localScale =Vector3.one;
        gameObject.SetActive(true);
    }
}
