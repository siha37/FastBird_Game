
#if UNITY_EDITOR
using System;
using UnityEngine;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute),true)]
    public class ReadOnlyAttributeDrawer :PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label,true);
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Disable the property field
            GUI.enabled = !Application.isPlaying && ((ReadOnlyAttribute)attribute).runtimeOnly;
            // Draw the property field
            EditorGUI.PropertyField(position, property, label, true);
            // Re-enable the property field
            GUI.enabled = true;
        }
    }
}
#endif

[AttributeUsage(AttributeTargets.Field)]
public class ReadOnlyAttribute : PropertyAttribute
{
    public readonly bool runtimeOnly;
    public ReadOnlyAttribute(bool runtimeOnly = false)
    {
        this.runtimeOnly = runtimeOnly;
    }
}