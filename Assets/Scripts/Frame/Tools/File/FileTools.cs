

using System.Runtime.InteropServices;
using System;
using System.IO;
using UnityEngine;

public static class FileTools
{
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileDlg ofd);

    public static string OpenFileDialog()
    {
        OpenFileDlg pth = new OpenFileDlg();
        pth.structSize = System.Runtime.InteropServices.Marshal.SizeOf(pth);

        string filepath = "";

        //pth.filter = "ͼƬ�ļ�(*.jpg*.png)\0*.jpg;*.png";
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.streamingAssetsPath.Replace('/', '\\');  // default path
        pth.title = "ѡ��Excel�ļ�";
        //pth.defExt = "csv";//��ʾ�ļ�����
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (GetOpenFileName(pth))
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

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileDlg
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}