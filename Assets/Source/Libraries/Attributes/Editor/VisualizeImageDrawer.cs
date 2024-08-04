using UnityEditor;
using UnityEngine;

namespace Source.Libraries.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(VisualizeImage))]
    public class VisualizeImageDrawer : DecoratorDrawer
    {
        private const float Width = 100f;
        private const float Height = 100f;
        
        public override void OnGUI(Rect position)
        {
            base.OnGUI(position);
            Object texture2D = ((VisualizeImage) attribute).Texture2D;
            EditorGUI.ObjectField(new Rect(position.x, position.y + base.GetHeight(), Width, Height), texture2D, typeof(Texture2D), false);
        }

        public override float GetHeight()
        {
            return Height + base.GetHeight();
        }
    }
}