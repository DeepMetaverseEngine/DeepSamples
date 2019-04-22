using DeepCore.GameData.Zone;
using TLBattle.Common.Plugins;
using UnityEngine;



public class SkillTargetSelect : MonoBehaviour
{
    public Transform CircleRange;
    public Transform CircleDamageRange;
    public Transform ArrowDamageRange;
    public Transform FanDamageRange;

    public Color NormalColor1;
    public Color NormalColor2;
    public Color CancelColor;

    public Material CircleRangeMat;
    public Material CircleDamageRangeMat;
    public Material ArrowDamageRangeMat;
    public Material FanDamageRangeMat;
    public Material RockerMat;

    public Quaternion Direction;

    private SkillTemplate Skill;
    private bool DalayShowRange = false;
    private float TimeCount = 0;
    private float TimeMax = 0.3f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DalayShowRange)
        {
            TimeCount += Time.deltaTime;
            if (TimeCount >= TimeMax)
            {
                CircleRange.gameObject.SetActive(true);
                TimeCount = 0;
                DalayShowRange = false;
            }
        }
    }

    public void SetActive(bool b)
    {
        if (!b)
        {
            DalayShowRange = false;
        }
        gameObject.SetActive(b);
    }


    public static bool IsNormalAtk(GameSkill.TLSkillType skilltype)
    {
        return skilltype == GameSkill.TLSkillType.normalAtk;
    }
    //public void Reset(SkillTemplate state, Quaternion direction, Vector3 position)
    //{
    //    Direction = direction;
    //    Skill = state;
    //    TLSkillProperties zsp = (TLSkillProperties)state.Properties;
    //    if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_Arrow)
    //    {
    //        CircleRange.gameObject.SetActive(false);
    //        CircleDamageRange.gameObject.SetActive(false);
    //        FanDamageRange.gameObject.SetActive(false);
    //        ArrowDamageRange.gameObject.SetActive(true);
    //        Transform t = ArrowDamageRange.GetChild(0);
    //        t.localScale = new Vector3(zsp.LaunchModeData.Shape_Width, 1, zsp.LaunchModeData.Shape_Radius);
    //        t.localPosition = new Vector3(0, 0, zsp.LaunchModeData.Shape_LaunchCenterOffset + zsp.LaunchModeData.Shape_Radius / 2);
    //        transform.rotation = direction;
    //        HudManager.Instance.SkillBar.cancelSkillBtn.gameObject.SetActive(true);
    //        ArrowDamageRangeMat.SetColor("_Color", NormalColor2);
    //        //ArrowDamageRangeMat.SetTextureOffset("_Offset", new Vector2(0, -0.01f));
    //    }
    //    else if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_Fan)
    //    {
    //        CircleRange.gameObject.SetActive(false);
    //        CircleDamageRange.gameObject.SetActive(false);
    //        FanDamageRange.gameObject.SetActive(true);
    //        ArrowDamageRange.gameObject.SetActive(false);
    //        Transform t = FanDamageRange.GetChild(0);
    //        t.localScale = Vector3.one;
    //        t.localPosition = new Vector3(0, 0.5f, zsp.LaunchModeData.Shape_LaunchCenterOffset);// + zsp.LaunchModeData.Shape_Radius / 2);
    //        CircularSectorMeshRenderer sector = t.GetComponent<CircularSectorMeshRenderer>();
    //        sector.radiusMin = zsp.LaunchModeData.Shape_LaunchCenterOffset;
    //        sector.radius = zsp.LaunchModeData.Shape_LaunchCenterOffset + zsp.LaunchModeData.Shape_Radius;
    //        sector.degree = zsp.LaunchModeData.Shape_Angle;
    //        transform.rotation = direction;
    //        sector.UpdateMesh();
    //        HudManager.Instance.SkillBar.cancelSkillBtn.gameObject.SetActive(true);
    //        FanDamageRangeMat.SetColor("_Color", NormalColor2);
    //    }
    //    else if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_Area)
    //    {
    //        CircleRange.gameObject.SetActive(true);
    //        CircleDamageRange.gameObject.SetActive(true);
    //        FanDamageRange.gameObject.SetActive(false);
    //        ArrowDamageRange.gameObject.SetActive(false);
    //        transform.localRotation = Quaternion.identity;
    //        CircleRange.transform.localScale = Vector3.one * state.AttackRange * 2;
    //        CircleDamageRange.transform.localScale = Vector3.one * zsp.LaunchModeData.Shape_Radius * 2;
    //        Vector3 pos;
    //        if (position == Vector3.zero)
    //        {
    //            pos = Vector3.zero;
    //            pos.y += 0.5f;
    //            CircleDamageRange.transform.localPosition = pos;
    //        }
    //        else
    //        {
    //            pos = position;
    //            pos.y += 0.5f;
    //            CircleDamageRange.transform.position = pos;
    //        }
    //        HudManager.Instance.SkillBar.cancelSkillBtn.gameObject.SetActive(true);
    //        CircleRangeMat.SetColor("_Color", NormalColor1);
    //        CircleDamageRangeMat.SetColor("_Color", NormalColor2);
    //    }
    //    else if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_None)
    //    {
    //        CircleRange.gameObject.SetActive(false);
    //        CircleDamageRange.gameObject.SetActive(false);
    //        FanDamageRange.gameObject.SetActive(false);
    //        ArrowDamageRange.gameObject.SetActive(false);
    //        transform.localRotation = Quaternion.identity;
    //        CircleRange.transform.localScale = Vector3.one * state.AttackRange * 2;
    //        DalayShowRange = true;
    //        TimeCount = 0;
    //        if (zsp.SkillType != GameSkill.TLSkillType.normalAtk)//普攻不显示取消按钮.
    //        {
    //            HudManager.Instance.SkillBar.cancelSkillBtn.gameObject.SetActive(true);
    //        }
    //        CircleRangeMat.SetColor("_Color", NormalColor1);
    //    }
    //    RockerMat.SetColor("_Color", NormalColor2);
    //}

    public void Reset(SkillTemplate state, Quaternion direction, Vector3 position)
    {
        Direction = direction;
        Skill = state;
        TLSkillProperties zsp = (TLSkillProperties)state.Properties;
        if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_Arrow)
        {
            CircleRange.gameObject.SetActive(false);
            CircleDamageRange.gameObject.SetActive(false);
            FanDamageRange.gameObject.SetActive(false);
            ArrowDamageRange.gameObject.SetActive(true);
            Transform t = ArrowDamageRange.GetChild(0);
            Projector p = t.GetChild(1).GetComponent<Projector>();
            p.orthographicSize = zsp.LaunchModeData.Shape_Radius;
            p.aspectRatio = zsp.LaunchModeData.Shape_Width / zsp.LaunchModeData.Shape_Radius;
            t.localPosition = new Vector3(0, 0, zsp.LaunchModeData.Shape_LaunchCenterOffset + zsp.LaunchModeData.Shape_Radius / 2);
            transform.rotation = direction;
            HudManager.Instance.SkillBar.cancelSkillBtn.gameObject.SetActive(true);
            ArrowDamageRangeMat.SetColor("_Color", NormalColor2);
        }
        else if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_Fan)
        {
            CircleRange.gameObject.SetActive(false);
            CircleDamageRange.gameObject.SetActive(false);
            FanDamageRange.gameObject.SetActive(true);
            ArrowDamageRange.gameObject.SetActive(false);
            Transform t = ArrowDamageRange.GetChild(1);
            t.localScale = Vector3.one;
            t.localPosition = new Vector3(0, 0.5f, zsp.LaunchModeData.Shape_LaunchCenterOffset);// + zsp.LaunchModeData.Shape_Radius / 2);
            CircularSectorMeshRenderer sector = t.GetComponent<CircularSectorMeshRenderer>();
            sector.radiusMin = zsp.LaunchModeData.Shape_LaunchCenterOffset;
            sector.radius = zsp.LaunchModeData.Shape_LaunchCenterOffset + zsp.LaunchModeData.Shape_Radius;
            sector.degree = zsp.LaunchModeData.Shape_Angle;
            transform.rotation = direction;
            sector.UpdateMesh();
            HudManager.Instance.SkillBar.cancelSkillBtn.gameObject.SetActive(true);
            FanDamageRangeMat.SetColor("_Color", NormalColor2);
        }
        else if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_Area)
        {
            CircleRange.gameObject.SetActive(true);
            CircleDamageRange.gameObject.SetActive(true);
            FanDamageRange.gameObject.SetActive(false);
            ArrowDamageRange.gameObject.SetActive(false);
            transform.localRotation = Quaternion.identity;

            Transform t = CircleRange.GetChild(1);
            Projector p = t.GetComponent<Projector>();
            p.orthographicSize = state.AttackRange;
            t = CircleDamageRange.GetChild(1);
            p = t.GetComponent<Projector>();
            p.orthographicSize = zsp.LaunchModeData.Shape_Radius;
            
            Vector3 pos;
            if (position == Vector3.zero)
            {
                pos = Vector3.zero;
                pos.y += 0.5f;
                CircleDamageRange.transform.localPosition = pos;
            }
            else
            {
                pos = position;
                pos.y += 0.5f;
                CircleDamageRange.transform.position = pos;
            }
            HudManager.Instance.SkillBar.cancelSkillBtn.gameObject.SetActive(true);
            CircleRangeMat.SetColor("_Color", NormalColor1);
            CircleDamageRangeMat.SetColor("_Color", NormalColor2);
        }
        else if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_None)
        {
            CircleRange.gameObject.SetActive(false);
            CircleDamageRange.gameObject.SetActive(false);
            FanDamageRange.gameObject.SetActive(false);
            ArrowDamageRange.gameObject.SetActive(false);
            transform.localRotation = Quaternion.identity;

            Transform t = CircleRange.GetChild(1);
            Projector p = t.GetComponent<Projector>();
            p.orthographicSize = state.AttackRange * 2;
            
            DalayShowRange = true;
            TimeCount = 0;
            if (!IsNormalAtk(zsp.SkillType))//普攻不显示取消按钮.
            {
                HudManager.Instance.SkillBar.cancelSkillBtn.gameObject.SetActive(true);
            }
            CircleRangeMat.SetColor("_Color", NormalColor1);
        }
        RockerMat.SetColor("_Color", NormalColor2);
    }

    public void Update(Vector3 target_pos, Vector3 self_pos, bool isCancel)
    {
        target_pos.y += 0.5f;
        self_pos.y += 0.5f;
        Color circleRangeColor = isCancel ? CancelColor : NormalColor1;
        Color othersColor = isCancel ? CancelColor : NormalColor2;
        TLSkillProperties zsp = (TLSkillProperties)Skill.Properties;
        if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_Arrow ||
            zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_Fan)
        {
            Vector3 dir = target_pos - self_pos;
            transform.forward = dir;
            ArrowDamageRangeMat.SetColor("_Color", othersColor);
            FanDamageRangeMat.SetColor("_Color", othersColor);
        }
        else if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_Area)
        {
            //transform.forward = target_pos - self_pos;
            CircleDamageRange.transform.position = target_pos;
            CircleRangeMat.SetColor("_Color", circleRangeColor);
            CircleDamageRangeMat.SetColor("_Color", othersColor);
        }
        else if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_None)
        {
            CircleRangeMat.SetColor("_Color", circleRangeColor);
        }
        RockerMat.SetColor("_Color", othersColor);
    }
}
