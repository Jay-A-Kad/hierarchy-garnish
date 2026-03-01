using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions.Must;


namespace HierarchyGarnish
{
    [InitializeOnLoad]
    public static class HierarchyGarnish
    {
        //cache guis 
        private static GUIStyle HeaderStyle;
        private static GUIStyle BadgeStyle;

        static HierarchyGarnish()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyItemGUI;
            EditorApplication.playModeStateChanged += _ => RepaintHierarchy();
            RepaintHierarchy();

        }

        private static void EnsureStyle()
        {
            if (HeaderStyle == null)
            {
                HeaderStyle = new GUIStyle(EditorStyles.boldLabel);
                HeaderStyle.normal.textColor = Color.cyan;
            }
            if (BadgeStyle == null)
            {
                BadgeStyle = new GUIStyle(EditorStyles.miniLabel);
                BadgeStyle.normal.textColor = Color.white;
                BadgeStyle.alignment = TextAnchor.MiddleCenter;
                BadgeStyle.padding = new RectOffset(4, 4, 2, 2);
                BadgeStyle.margin = new RectOffset(0, 0, 0, 0);
            }
        }

        private static void OnHierarchyItemGUI(int InstanceId, Rect selectionRect)
        {
            EnsureStyle();

        }

        private static void RepaintHierarchy()
        {
            EditorApplication.RepaintHierarchyWindow();
            var settings = HGSettings.GetOrCreate();
            if (!settings.Enabled) return;
            var obj = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            if (obj == null) return;

            string name = obj.name;

            //draw bg full row
            DrawRowBG(settings, name, selectionRect);

            if (IsHeader(name, out var headerKind))
            {
                DrawHeader(settings name, headerKind, selectionRect);
                return;
            }



        }

        private static void DrawRowBG(HGSettings settings, string name, Rect rect)
        {

        }

        private static bool IsHeader(string name, out HeaderKind kind)
        {
            kind = HeaderKind.None;

            if (name.StartsWith("===") && name.EndsWith("==="))
            {
                kind = HeaderKind.Equals;
                return true;
            }

            if (name.StartsWith("---") && name.EndsWith("---"))
            {
                kind = HeaderKind.Dashes;
                return true;
            }
            return false;
        }
    }
}


