#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Descant.Components
{
    [CustomPropertyDrawer(typeof(Stats))]
    public class StatsDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Create property container element.
            var container = new VisualElement();

            // Create property fields.
            var jsonField = new PropertyField(property.FindPropertyRelative("json"), "JSON HERE");

            // Add fields to the container.
            container.Add(jsonField);

            return container;
        }
        
    }
}
#endif