using System;
using System.Runtime.InteropServices;

public class FileAPI
{
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileDlg ofn);

    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] OpenFileDlg ofd);
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct OpenFileDlg
{
     public int structSize ;       //结构的内存大小
    public IntPtr dlgOwner;       //设置对话框的句柄
    public IntPtr instance;       //根据flags标志的设置，确定instance是谁的句柄，不设置则忽略
    public string filter;         //调取文件的过滤方式
    public string customFilter ;  //一个静态缓冲区 用来保存用户选择的筛选器模式
    public int maxCustFilter;     //缓冲区的大小
    public int filterIndex ;                 //指向的缓冲区包含定义过滤器的字符串对
    public string file ;                  //存储调取文件路径
    public int maxFile ;                     //存储调取文件路径的最大长度 至少256
    public string fileTitle ;             //调取的文件名带拓展名
    public int maxFileTitle ;                //调取文件名最大长度
    public string initialDir ;            //最初目录
    public string title ;                 //打开窗口的名字
    public int flags;                       //初始化对话框的一组位标志  参数类型和作用查阅官方API
    public short fileOffset ;                //文件名前的长度
    public short fileExtension ;             //拓展名前的长度
    public string defExt;                //默认的拓展名
    public IntPtr custData;       //传递给lpfnHook成员标识的钩子子程的应用程序定义的数据
    public IntPtr hook;           //指向钩子的指针。除非Flags成员包含OFN_ENABLEHOOK标志，否则该成员将被忽略。
    public string templateName;          //模块中由hInstance成员标识的对话框模板资源的名称
    public IntPtr reservedPtr;
    public int reservedInt;
    public int flagsEx;                     //可用于初始化对话框的一组位标志
}
