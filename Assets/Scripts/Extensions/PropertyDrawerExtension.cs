using UnityEditor;
using UnityEngine;

namespace Extensions
{
    public static class PropertyDrawerExtension
    {
        private const int IndentWidth = 20;

        public static Rect LineRect(this PropertyDrawer drawer, Rect origin, int line, int indent = 0)
        {
            return new Rect(
                origin.x + IndentWidth * indent,
                origin.y + EditorGUIUtility.singleLineHeight * line,
                origin.width - IndentWidth * indent,
                EditorGUIUtility.singleLineHeight
            );
        }
    }
}