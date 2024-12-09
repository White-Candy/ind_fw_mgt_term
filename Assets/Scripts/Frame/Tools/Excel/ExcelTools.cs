using Cysharp.Threading.Tasks;
using OfficeOpenXml;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class ExcelTools
{
    // Excel������ת��Userinfo��ʽ���ڴ�
    public static async UniTask<List<UserInfo>> Excel2UserInfos(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;

        List<UserInfo> list = new List<UserInfo>();

        await UniTask.RunOnThreadPool(() =>
        {
            // get excel file info.
            FileInfo f_inf = new FileInfo(path);

            // Open excel file by file info.
            using (ExcelPackage excelPkg = new ExcelPackage(f_inf))
            {
                // get first sheet of excel file.
                // Notice: The first index of 'excelPkg.Workbook.Worksheets' starts from 1.
                ExcelWorksheet sheet = excelPkg.Workbook.Worksheets[1];
                for (int i = 2; i < sheet.Cells.Rows; ++i)
                {
                    if (sheet.Cells[i, 1].Value == null && sheet.Cells[i, 2].Value == null && sheet.Cells[i, 3].Value == null && sheet.Cells[i, 4].Value == null 
                        && sheet.Cells[i, 5].Value == null && sheet.Cells[i, 6].Value == null && sheet.Cells[i, 7].Value == null && sheet.Cells[i, 8].Value == null)
                    {
                        break;
                    }

                    UserInfo inf = new UserInfo
                    {
                        userName = sheet.Cells[i, 1].Value?.ToString(),
                        Name = sheet.Cells[i, 2].Value?.ToString(),
                        Gender = sheet.Cells[i, 3].Value?.ToString(),
                        idCoder = sheet.Cells[i, 4].Value?.ToString(),
                        Age = sheet.Cells[i, 5].Value?.ToString(),
                        Identity = sheet.Cells[i, 6].Value?.ToString(),
                        Contact = sheet.Cells[i, 7].Value?.ToString(),
                        // className = sheet.Cells[i, 8].Value?.ToString()
                    };
                    
                    if (list.FindIndex(x => x.userName == inf.userName) == -1) list.Add(inf);
                }
                //excelPkg.Save();
            }
        });
        return list;
    }

    // ����excel�ļ�
    public static async UniTask CreateExcelFile(string savePath)
    {
        if (string.IsNullOrEmpty(savePath) || File.Exists(savePath)) return;

        await UniTask.RunOnThreadPool(() =>
        {
            FileInfo inf = new FileInfo(savePath);

            using (ExcelPackage excelPkg = new ExcelPackage(inf)) 
            {
                excelPkg.Workbook.Worksheets.Add("Sheet1");
                excelPkg.Save();
            }
        });
    }

    // ��Usersinfo����д���ⲿExcel����
    public static async UniTask WriteUserinfo2Excel(List<UserInfo> usinf, string path)
    {
        if (!File.Exists(path) || string.IsNullOrEmpty(path)) return;

        await UniTask.RunOnThreadPool(() =>
        {
            // get excel file info.
            FileInfo f_inf = new FileInfo(path);

            // Open excel file by file info.
            using (ExcelPackage excelPkg = new ExcelPackage(f_inf))
            {
                // get first sheet of excel file.
                // Notice: The first index of 'excelPkg.Workbook.Worksheets' starts from 1.
                ExcelWorksheet sheet = excelPkg.Workbook.Worksheets[1];
                
                List<string> title = new List<string>()
                { 
                    "�û���", "����", "�Ա�", "���֤��",
                    "����",  "���", "��ϵ��ʽ"
                };
                for (int col = 1; col <= 7; ++col)
                    sheet.Cells[1, col].Value = title[col - 1];

                for (int i = 0; i < usinf.Count; ++i)
                {
                    var inf = usinf[i];
                    sheet.Cells[i + 2, 1].Value = Tools.Unicode2String(inf.userName);
                    sheet.Cells[i + 2, 2].Value = Tools.Unicode2String(inf.Name);
                    sheet.Cells[i + 2, 3].Value = Tools.Unicode2String(inf.Gender);
                    sheet.Cells[i + 2, 4].Value = Tools.Unicode2String(inf.idCoder);
                    sheet.Cells[i + 2, 5].Value = Tools.Unicode2String(inf.Age);
                    sheet.Cells[i + 2, 6].Value = Tools.Unicode2String(inf.Identity);
                    sheet.Cells[i + 2, 7].Value = Tools.Unicode2String(inf.Contact);
                    // sheet.Cells[i + 2, 8].Value = inf.className;
                }

                excelPkg.Save();
            }
        });
    }


 
    // ��ScoresInf����д���ⲿExcel����
    public static async UniTask WriteScoresInf2Excel(List<ScoreInfo> scoresInf, string path)
    {
        if (!File.Exists(path) || string.IsNullOrEmpty(path)) return;

        await UniTask.RunOnThreadPool(() =>
        {
            // get excel file info.
            FileInfo f_inf = new FileInfo(path);

            // Open excel file by file info.
            using (ExcelPackage excelPkg = new ExcelPackage(f_inf))
            {
                // get first sheet of excel file.
                // Notice: The first index of 'excelPkg.Workbook.Worksheets' starts from 1.
                ExcelWorksheet sheet = excelPkg.Workbook.Worksheets[1];
                
                List<string> title = new List<string>()
                { 
                    "�û���", "����", "�༶", "��Ŀ",
                    "�γ�",  "���۳ɼ�", "ʵѵ�ɼ�", "�ܳɼ�"
                };
                for (int col = 1; col <= 8; ++col)
                    sheet.Cells[1, col].Value = title[col - 1];

                for (int i = 0; i < scoresInf.Count; ++i)
                {
                    var inf = scoresInf[i];
                    sheet.Cells[i + 2, 1].Value = inf.userName;
                    sheet.Cells[i + 2, 2].Value = inf.Name;
                    sheet.Cells[i + 2, 3].Value = inf.className;
                    sheet.Cells[i + 2, 4].Value = inf.columnsName;
                    sheet.Cells[i + 2, 5].Value = inf.courseName;
                    sheet.Cells[i + 2, 6].Value = inf.theoryScore;
                    sheet.Cells[i + 2, 7].Value = inf.trainingScore;
                    sheet.Cells[i + 2, 8].Value = (float.Parse(inf.theoryScore) + float.Parse(inf.trainingScore)).ToString();
                }

                excelPkg.Save();
            }
        });
    }   
}
