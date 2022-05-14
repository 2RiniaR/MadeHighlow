using System;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace General.Adapters
{
    [Serializable]
    public class TextViewer
    {
        [SerializeField] public Mode mode;
        [SerializeField] public Text text;
        [SerializeField] public TMP_Text tmpText;

        public string Content
        {
            get
            {
                switch (mode)
                {
                    case Mode.Text:
                        return text.text;
                    case Mode.TextMeshPro:
                        return tmpText.text;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                switch (mode)
                {
                    case Mode.Text:
                        text.text = value;
                        return;
                    case Mode.TextMeshPro:
                        tmpText.text = value;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public enum Mode
        {
            Text,
            TextMeshPro,
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TextViewer), true)]
    public class TextViewerDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(this.LineRect(position, 0), property.displayName);

            var modeProp = property.FindPropertyRelative("mode");
            var mode = (TextViewer.Mode)modeProp.enumValueIndex;
            mode = (TextViewer.Mode)EditorGUI.EnumPopup(this.LineRect(position, 1, 1), "種類", mode);
            modeProp.enumValueIndex = (int)mode;

            if (mode == TextViewer.Mode.Text)
            {
                var componentProp = property.FindPropertyRelative("text");
                var component = componentProp.objectReferenceValue;
                component = EditorGUI.ObjectField(
                    this.LineRect(position, 2, 1),
                    "コンポーネント",
                    component,
                    typeof(Text),
                    true
                );
                componentProp.objectReferenceValue = component;
            }
            else if (mode == TextViewer.Mode.TextMeshPro)
            {
                var componentProp = property.FindPropertyRelative("tmpText");
                var component = componentProp.objectReferenceValue;
                component = EditorGUI.ObjectField(
                    this.LineRect(position, 2, 1),
                    "コンポーネント",
                    component,
                    typeof(TMP_Text),
                    true
                );
                componentProp.objectReferenceValue = component;
            }
        }
    }
#endif
}
