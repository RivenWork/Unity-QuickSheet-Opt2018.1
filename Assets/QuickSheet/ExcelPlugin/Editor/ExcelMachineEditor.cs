///////////////////////////////////////////////////////////////////////////////
///
/// ExcelMachineEditor.cs
///
/// (c)2014 Kim, Hyoun Woo
///
///////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace UnityQuickSheet
{
    /// <summary>
    /// Custom editor script class for excel file setting.
    /// </summary>
    [CustomEditor(typeof(ExcelMachine))]
    public class ExcelMachineEditor : BaseMachineEditor
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            machine = target as ExcelMachine;
            if (machine != null && ExcelSettings.Instance != null)
            {
                if (string.IsNullOrEmpty(ExcelSettings.Instance.RuntimePath) == false)
                    machine.RuntimeClassPath = ExcelSettings.Instance.RuntimePath;
                if (string.IsNullOrEmpty(ExcelSettings.Instance.EditorPath) == false)
                    machine.EditorClassPath = ExcelSettings.Instance.EditorPath;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ExcelMachine machine = target as ExcelMachine;

            GUILayout.Label("Excel Spreadsheet Settings:", headerStyle);

            GUILayout.BeginHorizontal();
            GUILayout.Label("File:", GUILayout.Width(50));

            string path = string.Empty;
            if (string.IsNullOrEmpty(machine.excelFilePath))
                path = Application.dataPath;
            else
                path = machine.excelFilePath;

            machine.excelFilePath = GUILayout.TextField(path, GUILayout.Width(250));
            if (GUILayout.Button("...", GUILayout.Width(20)))
            {
                string folder = Path.GetDirectoryName(path);
#if UNITY_EDITOR_WIN
                path = EditorUtility.OpenFilePanel("Open Excel file", folder, "excel files;*.xls;*.xlsx");
#else // for UNITY_EDITOR_OSX
                path = EditorUtility.OpenFilePanel("Open Excel file", folder, "xls");
#endif
                if (path.Length != 0)
                {
                    machine.SpreadSheetName = Path.GetFileName(path);

                    // the path should be relative not absolute one to make it work on any platform.
                    int index = path.IndexOf("Assets");
                    if (index >= 0)
                    {
                        // set relative path
                        machine.excelFilePath = path.Substring(index);

                        // pass absolute path
                        machine.SheetNames = new ExcelQuery(path).GetSheetNames();
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Error",
                            @"Wrong folder is selected.
                        Set a folder under the 'Assets' folder! \n
                        The excel file should be anywhere under  the 'Assets' folder", "OK");
                        return;
                    }
                }
            }
            GUILayout.EndHorizontal();

            // Failed to get sheet name so we just return not to make editor on going.
            if (machine.SheetNames.Length == 0)
            {
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Error: Failed to retrieve the specified excel file.");
                EditorGUILayout.LabelField("If the excel file is opened, close it then reopen it again.");
                return;
            }

            // spreadsheet name should be read-only
            EditorGUILayout.TextField("Spreadsheet File: ", machine.SpreadSheetName);

            EditorGUILayout.Space();

            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("Worksheet: ", GUILayout.Width(100));
                machine.CurrentSheetIndex = EditorGUILayout.Popup(machine.CurrentSheetIndex, machine.SheetNames);
                if (machine.SheetNames != null)
                    machine.WorkSheetName = machine.SheetNames[machine.CurrentSheetIndex];

                if (GUILayout.Button("Refresh", GUILayout.Width(60)))
                {
                    // reopen the excel file e.g) new worksheet is added so need to reopen.
                    machine.SheetNames = new ExcelQuery(machine.excelFilePath).GetSheetNames();

                    // one of worksheet was removed, so reset the selected worksheet index
                    // to prevent the index out of range error.
                    if (machine.SheetNames.Length <= machine.CurrentSheetIndex)
                    {
                        machine.CurrentSheetIndex = 0;

                        string message = "Worksheet was changed. Check the 'Worksheet' and 'Update' it again if it is necessary.";
                        EditorUtility.DisplayDialog("Info", message, "OK");
                    }
                }
            }

            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();

            if (machine.HasColumnHeader())
            {
                if (GUILayout.Button("Update"))
                    Import();
                if (GUILayout.Button("Reimport"))
                    Import(true);
            }
            else
            {
                if (GUILayout.Button("Import"))
                    Import();
            }

            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            DrawHeaderSetting(machine);

            EditorGUILayout.Separator();

            GUILayout.Label("Path Settings:", headerStyle);
            GUILayout.BeginHorizontal();
            machine.TemplatePath = EditorGUILayout.TextField("Template:", machine.TemplatePath);
            if (GUILayout.Button("Browse", GUILayout.Width(60)))
            {
                var p = EditorUtility.OpenFolderPanel("Template:", machine.TemplatePath, string.Empty);
                if (!string.IsNullOrEmpty(p))
                    machine.TemplatePath = p.Substring(p.IndexOf("/Assets/") + "/Assets/".Length);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            machine.RuntimeClassPath = EditorGUILayout.TextField("Runtime:", machine.RuntimeClassPath);
            if (GUILayout.Button("Browse", GUILayout.Width(60)))
            {
                var p = EditorUtility.OpenFolderPanel("Runtime:", machine.RuntimeClassPath, string.Empty);
                if (!string.IsNullOrEmpty(p))
                    machine.RuntimeClassPath = p.Substring(p.IndexOf("/Assets/") + "/Assets/".Length);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            machine.EditorClassPath = EditorGUILayout.TextField("Editor:", machine.EditorClassPath);
            if (GUILayout.Button("Browse", GUILayout.Width(60)))
            {
                var p = EditorUtility.OpenFolderPanel("Editor:", machine.EditorClassPath, string.Empty);
                if (!string.IsNullOrEmpty(p))
                    machine.EditorClassPath = p.Substring(p.IndexOf("/Assets/") + "/Assets/".Length);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            machine.DataFilePath = EditorGUILayout.TextField("Data:", machine.DataFilePath);
            if (GUILayout.Button("Browse", GUILayout.Width(60)))
            {
                var p = EditorUtility.OpenFolderPanel("Data:", machine.DataFilePath, string.Empty);
                if (!string.IsNullOrEmpty(p))
                    machine.DataFilePath = p.Substring(p.IndexOf("/Assets/") + "/Assets/".Length);
            }
            GUILayout.EndHorizontal();

            machine.onlyCreateDataClass = EditorGUILayout.Toggle("Only DataClass", machine.onlyCreateDataClass);

            EditorGUILayout.Separator();

            if (GUILayout.Button("Generate"))
            {
                if (string.IsNullOrEmpty(machine.SpreadSheetName) || string.IsNullOrEmpty(machine.WorkSheetName))
                {
                    Debug.LogWarning("No spreadsheet or worksheet is specified.");
                    return;
                }

	            Directory.CreateDirectory(Application.dataPath + Path.DirectorySeparatorChar + machine.RuntimeClassPath);
	            Directory.CreateDirectory(Application.dataPath + Path.DirectorySeparatorChar + machine.EditorClassPath);

                ScriptPrescription sp = Generate(machine);
                if (sp != null)
                {
                    Debug.Log("Successfully generated!");
                }
                else
                    Debug.LogError("Failed to create a script from excel.");
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(machine);
            }
        }

        /// <summary>
        /// Import the specified excel file and prepare to set type of each cell.
        /// </summary>
        protected override void Import(bool reimport = false)
        {
            ExcelMachine machine = target as ExcelMachine;

            string path = machine.excelFilePath;
            string sheet = machine.WorkSheetName;

            if (string.IsNullOrEmpty(path))
            {
                string msg = "You should specify spreadsheet file first!";
                EditorUtility.DisplayDialog("Error", msg, "OK");
                return;
            }

            if (!File.Exists(path))
            {
                string msg = string.Format("File at {0} does not exist.",path);
                EditorUtility.DisplayDialog("Error", msg, "OK");
                return;
            }

            string error = string.Empty;
            var titleDic = new ExcelQuery(path, sheet).GetTitle(ref error);
            if (titleDic == null || !string.IsNullOrEmpty(error))
            {
                EditorUtility.DisplayDialog("Error", error, "OK");
                return;
            }
            else
            {
                // check the column header is valid
                foreach(var column in titleDic)
                {
                    if (!IsValidHeader(column.Key))
                    {
                        error = string.Format(@"Invalid column header name {0}. Any c# keyword should not be used for column header. Note it is not case sensitive.", column);
                        EditorUtility.DisplayDialog("Error", error, "OK");
                        return;
                    }
                }
            }

            var titleList = titleDic.Keys.ToList();
            if (machine.HasColumnHeader() && reimport == false)
            {
                var headerDic = machine.ColumnHeaderList.ToDictionary(header => header.name);

                // collect non-changed column headers
                var exist = titleDic.Where(e => headerDic.ContainsKey(e.Key) && headerDic[e.Key].type == e.Value)
                    .Select(t => new ColumnHeader { name = t.Key, type = t.Value, isArray = headerDic[t.Key].isArray, OrderNO = headerDic[t.Key].OrderNO });

                
                // collect newly added or changed column headers
                var changed = titleDic.Where(e => !headerDic.ContainsKey(e.Key) || headerDic[e.Key].type != e.Value)
                    .Select(t => ParseColumnHeader(t.Key, t.Value, titleList.IndexOf(t.Key)));

                // merge two list via LINQ
                var merged = exist.Union(changed).OrderBy(x => x.OrderNO);

                machine.ColumnHeaderList.Clear();
                machine.ColumnHeaderList = merged.ToList();
            }
            else
            {
                machine.ColumnHeaderList.Clear();
                if (titleDic.Count > 0)
                {
                    int order = 0;
                    machine.ColumnHeaderList = titleDic.Select(e => ParseColumnHeader(e.Key, e.Value, order++)).ToList();
                }
                else
                {
                    string msg = string.Format("An empty workhheet: [{0}] ", sheet);
                    Debug.LogWarning(msg);
                }
            }

            EditorUtility.SetDirty(machine);
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// Generate AssetPostprocessor editor script file.
        /// </summary>
        protected override void CreateAssetCreationScript(BaseMachine m, ScriptPrescription sp)
        {
            ExcelMachine machine = target as ExcelMachine;

            sp.className = machine.WorkSheetName;
            sp.dataClassName = machine.WorkSheetName + "Data";
            sp.worksheetClassName = machine.WorkSheetName;

            // where the imported excel file is.
            sp.importedFilePath = machine.excelFilePath;

            // path where the .asset file will be created.
            string path = machine.DataFilePath;
            path += "/" + machine.WorkSheetName + ".asset";
            sp.assetFilepath = path;
            sp.assetPostprocessorClass = machine.WorkSheetName + "AssetPostprocessor";
            sp.template = GetTemplate("PostProcessor");

            // write a script to the given folder.
            using (var writer = new StreamWriter(TargetPathForAssetPostProcessorFile(machine.WorkSheetName)))
            {
                writer.Write(new ScriptGenerator(sp).ToString());
                writer.Close();
            }
        }
    }
}