
namespace DeepCore.Unity3D.Game.Battle
{
    public interface IDebugCameraInterface
    {
        void DoNear();
        void DoFar();
        void DoSub_H();
        void DoAdd_H();
        void DoSub_RX();
        void DoAdd_RX();
        void DoSub_RY();
        void DoAdd_RY();
        void ShowBodySize(bool flag);
        void ShowGuard(bool flag);
        void ShowAttack(bool flag);
    }
}
