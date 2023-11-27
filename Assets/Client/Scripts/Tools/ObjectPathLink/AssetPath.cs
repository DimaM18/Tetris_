using System;

using UnityEngine;


namespace Client.Scripts.Tools.ObjectPathLink
{
    public static class AssetPath
    {
        [AttributeUsage(AttributeTargets.Field)]
        public class Attribute : PropertyAttribute
        {
            public Type Type { get; }

            public Attribute(Type type)
            {
                Type = type;
            }
        }
    
        private const string _resourcesFolderName = "/Resources/";

        private static string ConvertToResourcesPath(string projectPath)
        {
            if (string.IsNullOrEmpty(projectPath))
            {
                return string.Empty;
            }

            int folderIndex = projectPath.IndexOf(_resourcesFolderName, StringComparison.Ordinal);
            if (folderIndex == -1)
            {
                return string.Empty;
            }

            folderIndex += _resourcesFolderName.Length;

            int length = projectPath.Length - folderIndex;
            length -= projectPath.Length - projectPath.LastIndexOf('.');

            string resourcesPath = projectPath.Substring(folderIndex, length);

            return resourcesPath;
        }

        public static T Load<T>(string projectPath) where T : UnityEngine.Object
        {
            if(string.IsNullOrEmpty(projectPath))
            {
                return null; 
            }

            string resourcesPath = ConvertToResourcesPath(projectPath);

            if(!string.IsNullOrEmpty(resourcesPath))
            {
                return Resources.Load<T>(resourcesPath);
            }

#if UNITY_EDITOR
            return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(projectPath);
#else
            return null;
#endif
        }
    }
}