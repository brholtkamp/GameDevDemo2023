using GameDevDemo.Scripts;
using UnityEditor;
using UnityEngine;

namespace GameDevDemo.Editor
{
    [CustomEditor(typeof(LevelTransitionManager))]
    public class LevelTransitionManagerEditor : UnityEditor.Editor
    {
        private string _levelToTestName = "Playground";
        private LevelTransitionManager Target => target as LevelTransitionManager;
    
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.Space(10);

            _levelToTestName = EditorGUILayout.TextField("Level To Test Name", _levelToTestName);

            if (!Application.isPlaying || Target == null)
            {
                return;
            }
        
            if (GUILayout.Button("Test Level Load") && !string.IsNullOrWhiteSpace(_levelToTestName))
            {
                Target.TransitionToLevel(_levelToTestName);
            }
        }
    }
}
