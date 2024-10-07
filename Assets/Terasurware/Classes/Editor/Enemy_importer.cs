using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class Enemy_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Enemy.xlsx";
	private static readonly string exportPath = "Assets/Enemy.asset";
	private static readonly string[] sheetNames = { "Enemy1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			Entity_Enemy1 data = (Entity_Enemy1)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Entity_Enemy1));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Entity_Enemy1> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					Entity_Enemy1.Sheet s = new Entity_Enemy1.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Entity_Enemy1.Param p = new Entity_Enemy1.Param ();
						
					cell = row.GetCell(0); p.No = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.Key = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.Nama_JP = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.HP = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.ATK = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.SpeedX = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.SpeedY = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.BulletSpeed = (cell == null ? 0.0 : cell.NumericCellValue);
                    cell = row.GetCell(8); p.fireRate = (cell == null ? 0.0 : cell.NumericCellValue);
                    cell = row.GetCell(9); p.BOSS = (cell == null ? false : cell.BooleanCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
