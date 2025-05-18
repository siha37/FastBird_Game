#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace MyFolder._01.Script._03.UI.ButtonAction
{
    [CustomEditor(typeof(UIButtonActionRunner))]
    public class UIButtonActionRunnerEditor : Editor
    {
        private ReorderableList actionList;
        private SerializedProperty actionsProp;

        private static Dictionary<string, Type> actionTypes;

        private void OnEnable()
        {
            actionsProp = serializedObject.FindProperty("actions");

            actionList = new ReorderableList(serializedObject, actionsProp, true, true, true, true);
            actionList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Button Actions");
            
            actionList.elementHeightCallback = index =>
            {
                var element = actionsProp.GetArrayElementAtIndex(index);
                float baseHeight = EditorGUI.GetPropertyHeight(element, true);
                float labelHeight = EditorGUIUtility.singleLineHeight + 4;
                return baseHeight + labelHeight;
            };

            actionList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = actionsProp.GetArrayElementAtIndex(index);
                if (element == null) return;

                var typeName = element.managedReferenceFullTypename.Split(' ').Last().Split('.').Last();

                var labelRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.LabelField(labelRect, $"[{typeName}]", EditorStyles.boldLabel);

                rect.y += EditorGUIUtility.singleLineHeight + 2;
                EditorGUI.PropertyField(rect, element, GUIContent.none, true);
            };
            actionList.onAddDropdownCallback = (buttonRect, list) =>
            {
                if (actionTypes == null)
                    CacheActionTypes();

                GenericMenu menu = new GenericMenu();
                foreach (var pair in actionTypes)
                {
                    menu.AddItem(new GUIContent(pair.Key), false, () =>
                    {
                        var instance = Activator.CreateInstance(pair.Value);
                        actionsProp.arraySize++;
                        var element = actionsProp.GetArrayElementAtIndex(actionsProp.arraySize - 1);
                        element.managedReferenceValue = instance;
                        serializedObject.ApplyModifiedProperties();
                    });
                }

                menu.ShowAsContext();
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            actionList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        private void CacheActionTypes()
        {
            actionTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(asm => asm.GetTypes())
                .Where(t => typeof(UIButtonAction).IsAssignableFrom(t) && !t.IsAbstract)
                .ToDictionary(t => t.Name, t => t);
        }
    }
}
#endif