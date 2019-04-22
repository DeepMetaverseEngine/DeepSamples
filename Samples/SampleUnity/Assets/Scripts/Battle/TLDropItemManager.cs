using Assets.Scripts;
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using DeepCore.Unity3D.Utils;
using System;
using TLProtocol.Data;
using TLProtocol.Protocol.Client;
using UnityEngine;

 
public class TLDropItemData 
{
    public int TemplateID;
    public string Name;
    public string FileName;
    public int Quality;
    public int QualityEffect;
    public int itemType;
    public int pileNum;
    public int pileMax; 
}

/// <summary>
/// 场景内掉落物品.
/// </summary>
public class TLDropItemManager
{
    private bool lazyInit = false;
    public void InitNetWork()
    {
        TLNetManage.Instance.Listen<DropItemsNotify>(OnDropItemsNotify);
    }

    //private string effName = "/res/effect/item/{0}.assetbundles";

    private void OnDropItemsNotify(DropItemsNotify msg)
    {
        if(TLBattleScene.Instance == null || TLBattleScene.Instance.Actor == null)
        {
            //只是显示，玩家没加载的时候出来这玩意直接return;
            return;
        }

        var data = msg.info;
        int posX = data.posX;
        int posY = data.posY;
 
        
        ZoneObject ZObj = TLBattleScene.Instance.Actor.ZActor;
        var posZ = ZObj.Z;
        var H = ZObj.Parent.TerrainSrc.TotalHeight - 0.5f;

        UnitInfo info = ZObj.Templates.GetUnit(data.monsterId);
        float R = 1;
        if(info != null)
        {
            R = info.BodySize + 2f;
        }       

        float delay = 0;
        float t = 0;

        var title = HZLanguageManager.Instance.GetString("common_get");
        //阿基米德螺线

        //if (data.exp > 0)
        //{ 
             //经验统一有华仔那边处理
            //var expName = HZLanguageManager.Instance.GetString("get_exp");
            //uint argb = GameUtil.GetQualityColorARGB(3);
            //var message = string.Format(title, argb.ToString("x16"), expName, data.exp);
            //GameAlertManager.Instance.ShowFloatingTips(message);
        //}
 
        if (data.dropItemList == null || data.dropItemList.Count == 0)
        {
            return;
        }
 
        foreach (var itemNetData in data.dropItemList)
        {
            var Qty = itemNetData.Qty;

            var map = GameUtil.GetDBData("Item", itemNetData.TemplateID);
            if(map == null)
            {
                // 出bug了
                continue;
            }

            var Name = Convert.ToString(map["name"]);
            var FileName = Convert.ToString(map["item_resource"]);

            if (string.IsNullOrEmpty(FileName))
            {
                //var itemName = HZLanguageManager.Instance.GetString(Name);
                //var message = string.Format(title, itemName, itemNetData.Qty);
                //GameAlertManager.Instance.ShowFloatingTips(message);
                continue;
            }


            TLDropItemData item = new TLDropItemData();
            item.TemplateID = itemNetData.TemplateID;
            item.pileNum = itemNetData.pileNum;
            item.pileMax = itemNetData.pileMax;
            item.Name = Name;
            item.FileName = FileName;
       
            item.Quality = Convert.ToInt32(map["quality"]);
            item.QualityEffect = Convert.ToInt32(map["quality_effect"]);
            item.itemType = Convert.ToInt32(map["item_type"]);



            if (item.pileNum >0 && item.pileMax > 0 && Qty > item.pileNum)
            {
                long itemNum = 0;
                for(int i = 0; i < item.pileMax; i++)
                {
                    if(Qty > item.pileNum && (i < item.pileNum - 1))
                    {
                        itemNum = item.pileNum;
                        Qty -= itemNum;

                        float x1 = 0;
                        float y1 = 0;
                        UpdateT(R, ref delay, ref t, ref x1, ref y1);
                        AddItem(posX, posY, posX + x1, posY + y1, posZ, H, delay, item, itemNum);

                    }
                    else
                    {
                        itemNum = Qty;

                        float x1 = 0;
                        float y1 = 0;
                        UpdateT(R, ref delay, ref t, ref x1, ref y1);
                        AddItem(posX, posY, posX + x1, posY + y1, posZ, H, delay, item, itemNum);
                        break;
                    }
  
                    //var message = string.Format(title, itemName, item.Qty);
                    //GameAlertManager.Instance.ShowFloatingTips(message);

                }
            }
            else
            {
                //var message = string.Format(title, itemName, Qty);
                //GameAlertManager.Instance.ShowFloatingTips(message);

                float x1 = 0;
                float y1 = 0;

                UpdateT(R, ref delay, ref t, ref x1, ref y1);
                AddItem(posX, posY,posX + x1,posY + y1, posZ, H, delay, item, Qty);
            }
 
          
        }
        

    }


    void UpdateT(float R,ref float delay,ref float t,ref float x1,ref float y1)
    {

        //Debugger.LogError("Update value:" + value);
        //R = R + value;

        float d = UnityEngine.Random.Range(0, 720);
        //Debugger.LogError("Update d:" + d);
        float r = UnityEngine.Random.Range(1.0f, 4.5f);
        //Debugger.LogError("Update r:" + r);

        x1 = r * Mathf.Cos(Mathf.Deg2Rad * t * d);
        y1 = r * Mathf.Sin(Mathf.Deg2Rad * t * d);

        //x1 = (float)(r * Math.Cos(2 * Math.PI * t * rx));
        //y1 = (float)(r * Math.Sin(2 * Math.PI * t * ry));

        //老王说一起掉，不要延迟处理
        //wbb又给家回来了，视觉效果还不错  2018.2.09
       delay += 0.05f;

        if (t < 1)
        {
            t += 1 / 3f;
        }
        else if (t < 2)
        {
            t += 1 / 6f;
        }
        else if (t < 3)
        {
            t += 1 / 12f;
        }
        else
        {
            t += 1 / 24f;
        }
    }
 
    private void AddItem(float posX, float posY, float x,float y, float posZ, float H,float delay, TLDropItemData itemData,long realQty)
    {
        
        GameGlobal.Instance.StartCoroutine(GameGlobal.WaitForSeconds(delay, () =>
        {
            TLDropItemUnit unit = new TLDropItemUnit(TLBattleScene.Instance, itemData, realQty);
            unit.h = H;
            unit.posX = posX;
            unit.posY = posY;

            unit.x = x;
            unit.y = y;
            unit.z = posZ;

            unit.Load();

        }));
    }


}