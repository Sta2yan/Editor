using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Stanislav.FPSCamera.Editor
{
    [CustomEditor(typeof(FirstPersonCamera))]
    public class FirstPersonCameraEditor : UnityEditor.Editor
    {
        [SerializeField] private Texture _texture;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Type type = typeof(FirstPersonCamera);
            
            FieldInfo rotationXInfo = type.GetField("_rotationX", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo rotationYInfo = type.GetField("_rotationY", BindingFlags.Instance | BindingFlags.NonPublic);

            float rotationX = Convert.ToSingle(rotationXInfo.GetValue(target));
            float rotationY = Convert.ToSingle(rotationYInfo.GetValue(target));

            EditorGUILayout.Space(30);
            EditorGUILayout.LabelField("Helpful information");
            EditorGUILayout.Vector2Field("Camera Rotation:", new Vector2(rotationX, rotationY));

            if (GUILayout.Button(_texture)) 
                ChangeObjectName(type);
        }

        private void ChangeObjectName(Type type)
        {
            MethodInfo methodInfo = type.GetMethod("ChangeObjectName", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo textInfo = type.GetField("_text", BindingFlags.Instance | BindingFlags.NonPublic);
            textInfo.SetValue(target, "NameFromEditorScript");
            methodInfo.Invoke(target, null);
        }
    }
}
