

using System.Runtime.InteropServices;
using System;
using System.IO;
using UnityEngine;

public static class FileTools
{
    // 打开Window平台文件资源管理器，返回选择文件的路径
    public static string OpenFileDialog()
    {
        OpenFileDlg pth = new OpenFileDlg();
        pth.structSize = Marshal.SizeOf(pth);

        string filepath = "";

        pth.filter = "Excel文件(*.xlsx)" + '\0' + "*.xlsx";
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.streamingAssetsPath.Replace('/', '\\');  // default path
        pth.title = "选择Excel文件";
        pth.defExt = "XLSX";//显示文件类型
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;

        if (FileApi.GetOpenFileName(pth))
        {
            filepath = pth.file;//选择的文件路径;

            DirectoryInfo inf = new DirectoryInfo(filepath);

            //上级目录
            string path = inf.Parent.FullName;//返回文件的上级目录

            string name = Path.GetFileNameWithoutExtension(path);//返回路径的最后一个文件夹名称
        }
        return filepath;
    }

    // 打开文件资源管理器，文件路径返回
    public static string SaveFileDialog()
    {
        OpenFileDlg pth = new OpenFileDlg();
        pth.structSize = Marshal.SizeOf(pth);

        string filepath = "";

        pth.filter = "Excel文件(*.xlsx)" + '\0' + "*.xlsx\0\0";
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.streamingAssetsPath.Replace('/', '\\');  // default path
        pth.title = "选择Excel文件";
        pth.defExt = "XLSX";//显示文件类型
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;

        if (FileApi.GetSaveFileName(pth))
        {
            filepath = pth.file;//选择的文件路径;

            DirectoryInfo inf = new DirectoryInfo(filepath);

            //上级目录
            string path = inf.Parent.FullName;//返回文件的上级目录

            string name = Path.GetFileNameWithoutExtension(path);//返回路径的最后一个文件夹名称
        }
        return filepath;
    }
}