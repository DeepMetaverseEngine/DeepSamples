using DeepEditor.Plugin;
using DeepEditor.Plugin.Win32._3D.BattleClient;
using System;
using System.IO;
using System.Windows.Forms;
using ThreeLives.Battle.Test;

namespace SampleBattleWin32
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length >= 2)
            {
                new HZDSB();
                Application.Run(new FormBattleView3D(new DirectoryInfo(args[0]), int.Parse(args[1]), new ExecuteConfig()
                {
                    show_terrain = true,
                    use_3d = true,
                    use_mmp = true,
                }));
            }
            else
            {
                MessageBox.Show("错误的参数");
            }
        }
    }
}
