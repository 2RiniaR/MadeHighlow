using System;
using Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace General.Adapters
{
    [Serializable]
    public class SpriteViewer
    {
        [SerializeField] public Mode mode;
        [SerializeField] public Image image;
        [SerializeField] public SpriteRenderer spriteRenderer;

        public Sprite Sprite
        {
            get
            {
                switch (mode)
                {
                    case Mode.Image:
                        return image.sprite;
                    case Mode.SpriteRenderer:
                        return spriteRenderer.sprite;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                switch (mode)
                {
                    case Mode.Image:
                        image.sprite = value;
                        return;
                    case Mode.SpriteRenderer:
                        spriteRenderer.sprite = value;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public enum Mode
        {
            Image,
            SpriteRenderer,
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SpriteViewer), true)]
    public class SpriteViewerDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(this.LineRect(position, 0), property.displayName);

            var modeProp = property.FindPropertyRelative("mode");
            var mode = (SpriteViewer.Mode)modeProp.enumValueIndex;
            mode = (SpriteViewer.Mode)EditorGUI.EnumPopup(this.LineRect(position, 1, 1), "種類", mode);
            modeProp.enumValueIndex = (int)mode;

            if (mode == SpriteViewer.Mode.Image)
            {
                var componentProp = property.FindPropertyRelative("image");
                var component = componentProp.objectReferenceValue;
                component = EditorGUI.ObjectField(
                    this.LineRect(position, 2, 1),
                    "コンポーネント",
                    component,
                    typeof(Image),
                    true
                );
                componentProp.objectReferenceValue = component;
            }
            else if (mode == SpriteViewer.Mode.SpriteRenderer)
            {
                var componentProp = property.FindPropertyRelative("spriteRenderer");
                var component = componentProp.objectReferenceValue;
                component = EditorGUI.ObjectField(
                    this.LineRect(position, 2, 1),
                    "コンポーネント",
                    component,
                    typeof(SpriteRenderer),
                    true
                );
                componentProp.objectReferenceValue = component;
            }
        }
    }
#endif
}
