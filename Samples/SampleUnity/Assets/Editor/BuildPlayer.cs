
using UnityEditor;
using System.Diagnostics;
using UnityEngine;

public class BuildPlayer
{
    [MenuItem("Build/Windows Build Game")]
    public static void BuildStandalone()
    {
        // Get filename.
        string path = Application.dataPath + "/../Output/";
        string[] levels = new string[] { "Assets/Scenes/Battle.unity"};

        // Build player.
        BuildPipeline.BuildPlayer(levels, path + "/U3DSceneRun.exe", BuildTarget.StandaloneWindows, BuildOptions.None);

        // Copy a file from the project folder to the build folder, alongside the built game.
        //FileUtil.CopyFileOrDirectory("Assets/Templates/Readme.txt", path + "Readme.txt");

        // Run the game (Process class from System.Diagnostics).
        //Process proc = new Process();
        //proc.StartInfo.FileName = path + "/BuiltGame.exe";
        //proc.Start();
    }
}