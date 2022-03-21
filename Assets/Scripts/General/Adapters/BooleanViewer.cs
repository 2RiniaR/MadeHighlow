using System;
using Extensions;
using UnityEditor;
using UnityEngine;

namespace General.Adapters
{
    [Serializable]
    public class BooleanViewer
    {
        [SerializeField] public Mode mode;
        [SerializeField] public Animator animator;
        [SerializeField] public string animatorKey;

        public bool Value
        {
            get => mode switch
            {
                Mode.AnimationValue => animator.GetBool(animatorKey),
                _ => throw new ArgumentOutOfRangeException()
            };
            set
            {
                switch (mode)
                {
                    case Mode.AnimationValue:
                        animator.SetBool(animatorKey, value);
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public enum Mode
        {
            AnimationValue
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(BooleanViewer), true)]
    public class BooleanViewerDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight * 4;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(this.LineRect(position, 0), property.displayName);

            var modeProp = property.FindPropertyRelative("mode");
            var mode = (BooleanViewer.Mode)modeProp.enumValueIndex;
            mode = (BooleanViewer.Mode)EditorGUI.EnumPopup(this.LineRect(position, 1, 1), "種類", mode);
            modeProp.enumValueIndex = (int)mode;

            if (mode == BooleanViewer.Mode.AnimationValue)
            {
                var componentProp = property.FindPropertyRelative("animator");
                var component = componentProp.objectReferenceValue;
                component = EditorGUI.ObjectField(this.LineRect(position, 2, 1), "コンポーネント", component, typeof(Animator), true);
                componentProp.objectReferenceValue = component;

                var keyProp = property.FindPropertyRelative("animatorKey");
                var key = keyProp.stringValue;
                key = EditorGUI.TextField(this.LineRect(position, 3, 1), "トリガー名", key);
                keyProp.stringValue = key;
            }
        }
    }
#endif
}