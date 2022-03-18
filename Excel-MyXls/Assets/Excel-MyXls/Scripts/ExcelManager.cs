namespace UnityFramework
{
    using org.in2bits.MyXls;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    public class ExcelManager : MonoBehaviour
    {
        public string m_ExcelPath; //Excel路径
        public string m_ExcelName; //Excel名称
        public string[] m_ColNames; //列名称
        public List<string[]> m_Datas = new List<string[]>(); //行数据

        void Awake()
        {
            m_ExcelPath = Application.streamingAssetsPath;
            m_ExcelName = "终极一班学生信息表";
            m_ColNames = new string[] {"学生证号", "姓名", "年龄", "性别"};
            m_Datas.Add(new string[] {"20181130001", "张三", "18", "男"});
            m_Datas.Add(new string[] {"20181130001", "张三", "2000", "男"});
            m_Datas.Add(new string[] {"20181130001", "张三", "20", "男"});
            m_Datas.Add(new string[] {"20181130001", "张三", "20", "男"});
            m_Datas.Add(new string[] {"20181130001", "张三", "18", "男"});
        }

        void Start()
        {
            Save2Excel(m_ExcelPath, m_ExcelName, m_ColNames, m_Datas);
        }

        public void Save2Excel(string folderPath, string excelName, string[] colNames, List<string[]> datas)
        {
            XlsDocument xls = new XlsDocument();
            xls.FileName = excelName;
            xls.SummaryInformation.Author = "ZoJet";

            string sheetName = name;
            Worksheet sheet = xls.Workbook.Worksheets.Add(sheetName);
            Cells cells = sheet.Cells;

            int colNameCount = colNames.Length;
            int dataCount = datas.Count;

            for (int i = 0; i < dataCount + 1; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < colNameCount; j++)
                    {
                        cells.Add(1, j + 1, colNames[j]);
                    }
                }
                else
                {
                    for (int j = 0; j < colNameCount; j++)
                    {
                        cells.Add(i + 1, j + 1, datas[i - 1][j]);
                    }
                }
            }

            xls.Save(Application.streamingAssetsPath, true);
        }
    }
}