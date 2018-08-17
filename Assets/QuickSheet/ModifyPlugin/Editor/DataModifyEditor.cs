using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace DataModify
{
    public class DataModifyEditor : EditorWindow
    {

        [MenuItem("工具/数据编辑器")]
        public static void OpenModifyPanel()
        {
            GetWindow<DataModifyEditor>("数据编辑器");
        }

        void OnGUI()
        {
            //GUILayout.BeginArea(new Rect(5, 5, 600, 800));
            //GUILayout.Label("数据编辑器", GUIStyleSet.title);
            //GUILayout.EndArea();

            // Title
            //const int MEMBER_WIDTH = 100;
            //using (new GUILayout.HorizontalScope(EditorStyles.label))
            //{
            //    GUILayout.Label("Member", GUILayout.MinWidth(MEMBER_WIDTH));
            //    GUILayout.FlexibleSpace();
            //    string[] names = { "Type", "Array" };
            //    int[] widths = { 55, 40 };
            //    for (int i = 0; i < names.Length; i++)
            //    {
            //        GUILayout.Label(new GUIContent(names[i]), GUILayout.Width(widths[i]));
            //    }
            //}

            MultiColumnHeaderState multiColumnHeaderState = CreateDefaultMultiColumnHeaderState();
            //// Each cells
            //using (new EditorGUILayout.VerticalScope("box"))
            //{
            //    foreach (ColumnHeader header in m.ColumnHeaderList)
            //    {
            //        GUILayout.BeginHorizontal();

            //        // show member field with label, read-only
            //        EditorGUILayout.LabelField(header.name, GUILayout.MinWidth(MEMBER_WIDTH));
            //        GUILayout.FlexibleSpace();

            //        // specify type with enum-popup
            //        header.type = (CellType)EditorGUILayout.EnumPopup(header.type, GUILayout.Width(60));
            //        GUILayout.Space(20);

            //        // array toggle
            //        header.isArray = EditorGUILayout.Toggle(header.isArray, GUILayout.Width(20));
            //        GUILayout.Space(10);
            //        GUILayout.EndHorizontal();
            //    }
            //}
        }

        internal static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState()
        {
            return new MultiColumnHeaderState(GetColumns());
        }
        private static MultiColumnHeaderState.Column[] GetColumns()
        {
            var retVal = new MultiColumnHeaderState.Column[] {
                new MultiColumnHeaderState.Column(),
                new MultiColumnHeaderState.Column(),
                new MultiColumnHeaderState.Column(),
                new MultiColumnHeaderState.Column()
            };
            retVal[0].headerContent = new GUIContent("Asset", "Short name of asset. For full name select asset and see message below");
            retVal[0].minWidth = 50;
            retVal[0].width = 100;
            retVal[0].maxWidth = 300;
            retVal[0].headerTextAlignment = TextAlignment.Left;
            retVal[0].canSort = true;
            retVal[0].autoResize = true;

            retVal[1].headerContent = new GUIContent("Bundle", "Bundle name. 'auto' means asset was pulled in due to dependency");
            retVal[1].minWidth = 50;
            retVal[1].width = 100;
            retVal[1].maxWidth = 300;
            retVal[1].headerTextAlignment = TextAlignment.Left;
            retVal[1].canSort = true;
            retVal[1].autoResize = true;

            retVal[2].headerContent = new GUIContent("Size", "Size on disk");
            retVal[2].minWidth = 30;
            retVal[2].width = 75;
            retVal[2].maxWidth = 100;
            retVal[2].headerTextAlignment = TextAlignment.Left;
            retVal[2].canSort = true;
            retVal[2].autoResize = true;

            retVal[3].headerContent = new GUIContent("!", "Errors, Warnings, or Info");
            retVal[3].minWidth = 16;
            retVal[3].width = 16;
            retVal[3].maxWidth = 16;
            retVal[3].headerTextAlignment = TextAlignment.Left;
            retVal[3].canSort = true;
            retVal[3].autoResize = false;

            return retVal;
        }
    }

}
