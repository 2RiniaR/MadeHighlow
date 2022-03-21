using System;
using Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace General.Adapters
{
    [Serializable]
    public class PercentViewer
    {
        [SerializeField] public Mode mode;
        [SerializeField] public Image image;

        public float Percent
        {
            get => mode switch
            {
                Mode.ImageFill => image.fillAmount,
                _ => throw new ArgumentOutOfRangeException()
            };
            set
            {
                switch (mode)
                {
                    case Mode.ImageFill:
                        image.fillAmount = value;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public enum Mode
        {
            ImageFill
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(PercentViewer), true)]
    public class PercentViewerDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight * 3;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(this.LineRect(position, 0), property.displayName);

            var modeProp = property.FindPropertyRelative("mode");
            var mode = (PercentViewer.Mode)modeProp.enumValueIndex;
            mode = (PercentViewer.Mode)EditorGUI.EnumPopup(this.LineRect(position, 1, 1), "種類", mode);
            modeProp.enumValueIndex = (int)mode;

            if (mode == PercentViewer.Mode.ImageFill)
            {
                var componentProp = property.FindPropertyRelative("image");
                var component = componentProp.objectReferenceValue;
                component = EditorGUI.ObjectField(this.LineRect(position, 2, 1), "コンポーネント", component, typeof(Image), true);
                componentProp.objectReferenceValue = component;
            }
        }
    }
#endif
}