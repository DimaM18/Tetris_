using System;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

using Object = UnityEngine.Object;


namespace Client.Scripts.Tools.ObjectPathLink.Editor
{
    [CustomPropertyDrawer(typeof(AssetPath.Attribute))]
    public class AssetPathDrawer : PropertyDrawer
    {
        private const string _invalidTypeLabel = "Attribute invalid for type ";

        private readonly IDictionary<string, Object> _references;
        
        public AssetPathDrawer()
        {
            _references = new Dictionary<string, Object>();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property = GetProperty(property);
            if (property.propertyType != SerializedPropertyType.String)
            {
                Rect labelPosition = position;
                labelPosition.width = EditorGUIUtility.labelWidth;
                
                GUI.Label(labelPosition, label);
                
                Rect contentPosition = position;
                contentPosition.x += labelPosition.width;
                contentPosition.width -= labelPosition.width;
                
                EditorGUI.HelpBox(contentPosition, _invalidTypeLabel + fieldInfo.FieldType.Name, MessageType.Error);
            }
            else
            {
                HandleObjectReference(position, property, label);
            }
        }

        protected virtual SerializedProperty GetProperty(SerializedProperty rootProperty)
        {
            return rootProperty;
        }

        protected virtual Type ObjectType()
        {
            return ((AssetPath.Attribute)attribute).Type;
        }

        private void HandleObjectReference(Rect position, SerializedProperty property, GUIContent label)
        {
            Type objectType = ObjectType();
            Object propertyValue = null;
            string assetPath = property.stringValue;
            
            if (_references.ContainsKey(property.propertyPath))
            {
                propertyValue = _references[property.propertyPath];
            }
            if (propertyValue == null && !string.IsNullOrEmpty(assetPath))
            {
                propertyValue = AssetDatabase.LoadAssetAtPath(assetPath, objectType);

                if (propertyValue != null)
                {
                    _references[property.propertyPath] = propertyValue;
                }
            }

            EditorGUI.BeginChangeCheck();
            
            propertyValue = EditorGUI.ObjectField(position, label, propertyValue, objectType, false);
            
            if (EditorGUI.EndChangeCheck())
            {
                OnSelectionMade(propertyValue, property);
            }
        }

        protected virtual void OnSelectionMade(Object newSelection, SerializedProperty property)
        {
            string assetPath = string.Empty;

            if (newSelection != null)
            {
                assetPath = AssetDatabase.GetAssetPath(newSelection);
            }

            _references[property.propertyPath] = newSelection;
            property.stringValue = assetPath;
        }
    }
}
