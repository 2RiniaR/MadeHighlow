using System;
using Extensions;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace General.Adapters
{
    [Serializable]
    public class PulseTrigger
    {
        [SerializeField] public Mode mode;
        [SerializeField] public Button button;

        public IObservable<Unit> OnTriggeredObservable()
        {
            return mode switch
            {
                Mode.ButtonClick => button.OnClickAsObservable(),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        public enum Mode
        {
            ButtonClick,
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(PulseTrigger), true)]
    public class PulseTriggerDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(this.LineRect(position, 0), property.displayName);

            var modeProp = property.FindPropertyRelative("mode");
            var mode = (PulseTrigger.Mode)modeProp.enumValueIndex;
            mode = (PulseTrigger.Mode)EditorGUI.EnumPopup(this.LineRect(position, 1, 1), "種類", mode);
            modeProp.enumValueIndex = (int)mode;

            if (mode == PulseTrigger.Mode.ButtonClick)
            {
                var componentProp = property.FindPropertyRelative("button");
                var component = componentProp.objectReferenceValue;
                component = EditorGUI.ObjectField(
                    this.LineRect(position, 2, 1),
                    "コンポーネント",
                    component,
                    typeof(Button),
                    true
                );
                componentProp.objectReferenceValue = component;
            }
        }
    }
#endif
}
