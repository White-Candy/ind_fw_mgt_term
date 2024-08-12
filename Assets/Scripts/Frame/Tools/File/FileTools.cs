

using System.Runtime.InteropServices;
using System;
using System.IO;
using UnityEngine;

public static class FileTools
{
    // ��Windowƽ̨�ļ���Դ������������ѡ���ļ���·��
    public static string OpenFileDialog()
    {
        OpenFileDlg pth = new OpenFileDlg();
        pth.structSize = Marshal.SizeOf(pth);

        string filepath = "";

        pth.filter = "Excel�ļ�(*.xlsx)" + '\0' + "*.xlsx";
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.streamingAssetsPath.Replace('/', '\\');  // default path
        pth.title = "ѡ��Excel�ļ�";
        pth.defExt = "XLSX";//��ʾ�ļ�����
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;

        if (FileApi.GetOpenFileName(pth))
        {
            filepath = pth.file;//ѡ����ļ�·��;

            DirectoryInfo inf = new DirectoryInfo(filepath);

            //�ϼ�Ŀ¼
            string path = inf.Parent.FullName;//�����ļ����ϼ�Ŀ¼

            string name = Path.GetFileNameWithoutExtension(path);//����·�������һ���ļ�������
        }
        return filepath;
    }

    // ���ļ���Դ���������ļ�·������
    public static string SaveFileDialog()
    {
        OpenFileDlg pth = new OpenFileDlg();
        pth.structSize = Marshal.SizeOf(pth);

        string filepath = "";

        pth.filter = "Excel�ļ�(*.xlsx)" + '\0' + "*.xlsx\0\0";
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.streamingAssetsPath.Replace('/', '\\');  // default path
        pth.title = "ѡ��Excel�ļ�";
        pth.defExt = "XLSX";//��ʾ�ļ�����
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;

        if (FileApi.GetSaveFileName(pth))
        {
            filepath = pth.file;//ѡ����ļ�·��;

            DirectoryInfo inf = new DirectoryInfo(filepath);

            //�ϼ�Ŀ¼
            string path = inf.Parent.FullName;//�����ļ����ϼ�Ŀ¼

            string name = Path.GetFileNameWithoutExtension(path);//����·�������һ���ļ�������
        }
        return filepath;
    }
}