using UnityEditor;
using UnityEngine;

namespace PixelCrew.Model.Data.Properties
{

    [CustomPropertyDrawer(typeof(HideInDerivedAttribute))]
    public class HideInDerivedDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Если объект не является именно тем типом, где поле объявлено — скрываем
            var declaringType = fieldInfo.DeclaringType;
            var targetType = property.serializedObject.targetObject.GetType();

            if (targetType != declaringType)
            {
                return; // ничего не рисуем
            }

            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var declaringType = fieldInfo.DeclaringType;
            var targetType = property.serializedObject.targetObject.GetType();

            if (targetType != declaringType)
            {
                return 0; // высота 0 = поле не видно
            }

            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }

}