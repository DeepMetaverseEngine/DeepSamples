

using DeepCore.GameData.Zone;
using DeepCore.Unity3D.Utils;
using System.Collections;
using UnityEngine;

public interface IUnit
{
    UnitInfo Info { get; }
    uint ObjectID { get; }
    float X { get; }
    float Y { get; }
    Vector3 Position { get; }
    System.Action<Vector3> OnPositonChange { get; set; }
    //float GetAnimLength(string name);
    bool PlayAnim(string name, bool crossFade, WrapMode wrapMode = WrapMode.Once
        , float speed = 1f);
    int GetAnimLength(string name);
    Vector3 EulerAngles();
    Vector3 Forward { get; set; }
    DummyNode GetDummyNode(string name);
    Vector3 TransformDirection(Vector3 v);
    //void ITweenMoveTo(Hashtable args);
    //void iTweenRotateTo(Hashtable args);
    void AddBubbleTalkInfo(string content, string TalkActionType,int keepTimeMS);
}