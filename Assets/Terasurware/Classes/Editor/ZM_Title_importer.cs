using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class ZM_Title_importer : AssetPostprocessor
{
    private static readonly string filePath = "Assets/Exel/ZM_Title.xls";
    private static readonly string[] sheetNames = { "Title", };
    
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets)
        {
            if (!filePath.Equals(asset))
                continue;

            using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}

                foreach (string sheetName in sheetNames)
                {
                    var exportPath = "Assets/Exel/" + sheetName + ".asset";
                    
                    // check scriptable object
                    var data = (Lv1)AssetDatabase.LoadAssetAtPath(exportPath, typeof(Lv1));
                    if (data == null)
                    {
                        data = ScriptableObject.CreateInstance<Lv1>();
                        AssetDatabase.CreateAsset((ScriptableObject)data, exportPath);
                        data.hideFlags = HideFlags.NotEditable;
                    }
                    data.param.Clear();

					// check sheet
                    var sheet = book.GetSheet(sheetName);
                    if (sheet == null)
                    {
                        Debug.LogError("[QuestData] sheet not found:" + sheetName);
                        continue;
                    }

                	// add infomation
                    for (int i=1; i<= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        ICell cell = null;
                        
                        var p = new Lv1.Param();
			
					cell = row.GetCell(0); p.tuujou = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.kusukusu = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.kantan = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.komaru = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(4); p.ureshii = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(5); p.sagesumi = (cell == null ? "" : cell.StringCellValue);

                        data.param.Add(p);
                    }
                    
                    // save scriptable object
                    ScriptableObject obj = AssetDatabase.LoadAssetAtPath(exportPath, typeof(ScriptableObject)) as ScriptableObject;
                    EditorUtility.SetDirty(obj);
                }
            }

        }
    }
}
