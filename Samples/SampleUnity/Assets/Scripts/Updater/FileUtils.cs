using UnityEngine;
using System.Collections;
using System.IO;

#if !UNITY_IOS
public class FileUtils {

    public FileUtils()
    {
	
	}

    //private static long copyBytes = 0;
    //private static long totalBytes = 0;
    private static int copyFiles = 0;
    private static int totalFiles = 0;
    private static float progress = 0;
    private static bool isFinish = false;
    private static object lockobj = new object();

    public static bool GetProgress(out long current, out long total, out float progress)
    {
        bool isOk = false;
        lock(lockobj)
        {
            current = copyFiles;
            total = totalFiles;
            isOk = isFinish;
            progress = FileUtils.progress;// (float)((float)copyFiles / (float)totalFiles);
        }
        return isOk;
    }

    public static void TryResetCopy()
    {
        if (totalFiles <= 0)
        {
            lock(lockobj)
            {
                isFinish = true;
            }
        }
    }


    public static int CountFiles(string file)
    {
        Debugger.Log("GameLocal.CountFiles" + file);

        int fileCount = 0;

        try
        {
            DeepCore.SharpZipLib.Zip.ZipInputStream s = new DeepCore.SharpZipLib.Zip.ZipInputStream(File.OpenRead(file));

            DeepCore.SharpZipLib.Zip.ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string directoryName = Path.GetDirectoryName(theEntry.Name);
                string fileName = Path.GetFileName(theEntry.Name);

                int index = directoryName.IndexOf("MPQ/");

                if (index == -1)
                    continue;

                fileCount++;

            }

        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }

        return fileCount;
    }


    public static void CopyZipFiles(string zipfile, 
                                    System.Collections.Generic.List<string> list,
                                    string dir)
    {
        DeepCore.SharpZipLib.Zip.ZipInputStream s = new DeepCore.SharpZipLib.Zip.ZipInputStream(File.OpenRead(zipfile));
        DeepCore.SharpZipLib.Zip.ZipEntry theEntry;
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        lock(lockobj)
        {
            copyFiles = 0;
            totalFiles = list.Count;
            //copyBytes = 0;
            //totalBytes = 100 * list.Count;
            isFinish = false;
        }
        //Debugger.Log("copyFiles " + totalBytes + " " + totalFiles);
        
        new System.Threading.Thread(() =>
        {

            int size = 1024 * 8;
            byte[] data = new byte[size];
            int count = 0;
            int count_pro = 0;
            try
            {
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    foreach (string var in list)
                    {
                        if (theEntry.Name.EndsWith(var))
                        {

                            string directoryName = Path.GetDirectoryName(var);
                            string fileName = Path.GetFileName(var);
                            string dstdir = dir + "/" + directoryName + "/";
                            if (!Directory.Exists(dstdir))
                            {
                                Directory.CreateDirectory(dstdir);
                            }
                            string filePath = dstdir + fileName;
                            long fileSize = theEntry.Size;
                            long writeSize = 0;

                            FileStream streamWriter = File.Create(filePath);
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                                writeSize += size;
                                count_pro++;
                                lock (lockobj)
                                {
                                    //copyBytes = count * 100 + (long)(writeSize * 100f / fileSize);
                                    progress = Mathf.Atan(count_pro / 5000) * 2 / Mathf.PI;
                                }
                            }
                            streamWriter.Close();
                            count++;
                            lock (lockobj)
                            {
                                copyFiles = count;
                                //copyBytes = count * 100;
                            }

                        }

                    }
                }
                lock (lockobj)
                {
                    isFinish = true;
                    //copyBytes = totalBytes;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                lock (lockobj)
                {
                    isFinish = true;
                    //copyBytes = totalBytes;
                }
                s.Close();
            }

            
        }).Start();
    }

    public static void UnpackFiles(string file, string dir)
    {
        Debugger.Log("GameLocal.AndroidLocal UnpackFiles" + file + " " + dir);

        try
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            DeepCore.SharpZipLib.Zip.ZipInputStream s = new DeepCore.SharpZipLib.Zip.ZipInputStream(File.OpenRead(file));

            DeepCore.SharpZipLib.Zip.ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {

                string directoryName = Path.GetDirectoryName(theEntry.Name);
                string fileName = Path.GetFileName(theEntry.Name);

                int index = directoryName.IndexOf("/MPQ");

                if (index == -1)
                    continue;

                Debugger.Log("theEntry.Name: " + theEntry.Name);
                Debugger.Log("MPQ: " + directoryName + " fileName: " + fileName);
                directoryName = directoryName.Substring(index + 1);

                string writePath = dir + directoryName;
                if (!Directory.Exists(writePath))
                {
                    Debugger.Log("create dir: " + dir + directoryName);
                    Directory.CreateDirectory(dir + directoryName);
                }

                if (fileName != string.Empty)
                {
                    index = theEntry.Name.IndexOf("MPQ/");
                    if (index == -1)
                        continue;

                    string sFileEntryName = theEntry.Name.Substring(index);

                    string filePath = dir + sFileEntryName;
                    if (File.Exists(filePath))
                    {
                        continue;
                    }

                    Debugger.Log("write file: " + filePath);

                    FileStream streamWriter = File.Create(filePath);

                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    streamWriter.Close();
                }
            }
            s.Close();
        }
        catch (System.Exception e)
        {
            Debugger.LogException(e);
        }
    }

}
#endif