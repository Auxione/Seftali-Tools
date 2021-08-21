#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Seftali.StateMachine {
    [ExecuteInEditMode]
    [CustomEditor(typeof(StateMachine))]
    public class StateMachineEditor : Editor {

        private StateMachine StateMachine;
        private SerializedProperty OnStateChangeProperty;
        private SerializedProperty StateObjectsProperty;

        private void OnEnable() {
            this.StateMachine = this.target as StateMachine;
            this.OnStateChangeProperty = this.serializedObject.FindProperty("OnStateChange");
            this.StateObjectsProperty = this.serializedObject.FindProperty("StateObjects");
        }
        private bool eventFoldout = true;
        private bool selectionFoldout = true;
        public override void OnInspectorGUI() {
            //base.OnInspectorGUI();
            StateMachine.ControlGameObject = EditorGUILayout.Toggle("Control GameObject", StateMachine.ControlGameObject);
            EditorGUILayout.PropertyField(this.StateObjectsProperty);

            eventFoldout = EditorGUILayout.Foldout(eventFoldout, "Events");
            if(eventFoldout) {
                EditorGUILayout.PropertyField(this.OnStateChangeProperty);
            }

            selectionFoldout = EditorGUILayout.Foldout(selectionFoldout, "State Selection");
            if(selectionFoldout) {

                EditorGUILayout.BeginHorizontal();
                if(GUILayout.Button("Previous")) {
                    this.StateMachine.Previous();
                }
                if(GUILayout.Button("Reset")) {
                    this.StateMachine.ResetState();
                }
                if(GUILayout.Button("Next")) {
                    this.StateMachine.Next();
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginVertical();
                for(int i = 0; i < this.StateMachine.Count; i++) {
                    if(this.StateMachine[i] == null) { continue; }
                    if(GUILayout.Button(this.StateMachine[i].gameObject.name)) {
                        this.StateMachine.ChangeTo(i);
                    }
                }
                EditorGUILayout.EndVertical();
            } 


            this.serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif