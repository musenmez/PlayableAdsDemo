using System.Collections;
using System.Collections.Generic;
using Game.Runtime;
using UnityEngine;
using UnityEditor;

namespace Game.Editor
{
    [CustomEditor(typeof(PanelBase), true)]
    public class PanelBaseEditor : UnityEditor.Editor
    {

        private const float BUTTON_HEIGHT = 30;
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GUILayout.Space(15);
            
            var panel = (PanelBase)target;
            if(GUILayout.Button("Show Panel", GUILayout.Height(BUTTON_HEIGHT)))
            {
                panel.SetCanvasGroup(true);
            }

            GUILayout.Space(2);
            if (GUILayout.Button("Hide Panel", GUILayout.Height(BUTTON_HEIGHT)))
            {
                panel.SetCanvasGroup(false);
            }
        }
    }
}
