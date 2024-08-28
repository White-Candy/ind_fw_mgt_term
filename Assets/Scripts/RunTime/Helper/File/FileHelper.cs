

using System.Runtime.InteropServices;
using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class FileHelper
{
    // ��Windowƽ̨�ļ���Դ������������ѡ���ļ���·��
    public static List<string> OpenFileDialog(string filter, string title, string defExt)
    {
        List<string> filesPath = new List<string>();
        OpenFileDlg ofd = new OpenFileDlg();

        ofd.structSize = Marshal.SizeOf(ofd);
        ofd.filter = filter;
        ofd.file = new string(new char[1024]);
        ofd.maxFile = ofd.file.Length;
        ofd.fileTitle = new string(new char[64]);
        ofd.maxFileTitle = ofd.fileTitle.Length;
        ofd.initialDir = Application.streamingAssetsPath.Replace('/', '\\');  // default path
        ofd.title = title;
        ofd.defExt = defExt;//��ʾ�ļ�����
        //pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        ofd.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;

        if (FileAPI.GetOpenFileName(ofd))
        {
            string[] Splitstr = {"\0"};
            string[] strs = ofd.file.Split(Splitstr, StringSplitOptions.RemoveEmptyEntries);
            string rootPath = "";

            if (strs.Length > 1) 
            {
                rootPath = strs[0];
                for (int i = 1; i < strs.Length; ++i)
                {
                    string filepath = rootPath + "\\" + strs[i];
                    filesPath.Add(filepath);
                }
            }
            else
            {
                filesPath.Add(strs[0]);
            }
        }
        return filesPath;
    }

    // ���ļ���Դ���������ļ�·������
    public static string SaveFileDialog(string filter, string title, string defExt)
    {
        OpenFileDlg pth = new OpenFileDlg();
        pth.structSize = Marshal.SizeOf(pth);

        string filepath = "";

        pth.filter = filter;
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.streamingAssetsPath.Replace('/', '\\');  // default path
        pth.title = title;
        pth.defExt = defExt; //��ʾ�ļ�����
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;

        if (FileAPI.GetSaveFileName(pth))
        {
            filepath = pth.file;//ѡ����ļ�·��;

            DirectoryInfo inf = new DirectoryInfo(filepath);

            //�ϼ�Ŀ¼
            string path = inf.Parent.FullName;//�����ļ����ϼ�Ŀ¼

            string name = Path.GetFileNameWithoutExtension(path);//����·�������һ���ļ�������
        }
        return filepath;
    }

    /// <summary>
    /// ��Դ�ļ��ļ���
    /// </summary>
    /// <param name="path"></param>
    /// <param name="filter"></param>
    /// <param name="title"></param>
    /// <param name="defExt"></param>
    public static void ResourcesFileLoad(ref List<FilePackage> filesInfo, string filter, string defExt, string typeName, string relative = "")
    {
        List<string> filesPath = OpenFileDialog(filter, "�ļ�ѡ��", defExt);

        if (filesPath.Count() <= 0) return;

        foreach (string filePath in filesPath)
        {
            FilePackage info = new FilePackage();
            string[] Split = filePath.Split("\\");
            string fileName = Split[Split.Length - 1];

            info.fileName = typeName + "\\" + fileName;
            info.relativePath = relative;
            info.fileData = FStream2BypeArray(filePath);

            filesInfo.Add(info);
        }
    }

    /// <summary>
    /// �ļ�����ȡתΪByte[]
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static byte[] FStream2BypeArray(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;

        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        lock (fs)
        {
            try
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                return buffer;
            }
            catch { }
            finally { fs?.Close(); }
        }     
        return new byte[0];
    }
}


/// <summary>
/// �ļ���
/// </summary>
public class FilePackage
{
    public string fileName;
    public string relativePath; // ���·��
    public byte[] fileData;
}