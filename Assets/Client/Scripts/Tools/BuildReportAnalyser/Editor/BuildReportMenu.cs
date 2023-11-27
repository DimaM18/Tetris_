using System;
using System.IO;

using UnityEditor;
using UnityEditor.Build.Reporting;

using UnityEngine;

namespace Client.Scripts.Tools.BuildReportAnalyser.Editor
{
    public static class BuildReportMenu
    {
        [MenuItem("Tools/BuildReport/Show Build Report Window")]
        private static void ShowWindow()
        {
            EditorWindow.GetWindow<BuildReportWindow>().Show();
        }

        public static BuildReport LoadBuildReport()
        {
            const string buildReportDir = "Assets/BuildReports";
            if (Directory.Exists(buildReportDir) == false)
            {
                Directory.CreateDirectory(buildReportDir);
            }

            string assetPath = buildReportDir + "/New Report.buildreport";
            try
            {
                File.Copy("Library/LastBuild.buildreport", assetPath, true);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Не удалось получить свежий отчёт о сборке. Используется старый.");
                Debug.LogException(e);
            }
            AssetDatabase.ImportAsset(assetPath);

            var report = AssetDatabase.LoadAssetAtPath<BuildReport>(assetPath);
            return report;
        }
    }
}