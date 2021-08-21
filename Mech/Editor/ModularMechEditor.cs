#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Seftali.Mech {
    [CustomEditor(typeof(Mech))]
    [ExecuteInEditMode]
    public class ModularMechEditor : Editor {
        private Mech ModularMech;
        private bool showEvents;

        private SerializedProperty OnPartDamagedEvent;
        private SerializedProperty OnPartDestroyedEvent;
        private SerializedProperty OnPartRepairedEvent;
        private SerializedProperty Parts;

        private void OnEnable() {
            this.ModularMech = this.target as Mech;

            this.OnPartDamagedEvent = this.serializedObject.FindProperty("OnPartDamaged");
            this.OnPartDestroyedEvent = this.serializedObject.FindProperty("OnPartDestroyed");
            this.OnPartRepairedEvent = this.serializedObject.FindProperty("OnPartRepaired");
            this.Parts = this.serializedObject.FindProperty("Parts");
        }

        public override void OnInspectorGUI() {
            EditorGUILayout.PropertyField(this.Parts);

            this.showEvents = EditorGUILayout.Foldout(this.showEvents, "Show Events");

            if(this.showEvents) {
                EditorGUILayout.PropertyField(this.OnPartDamagedEvent);
                EditorGUILayout.PropertyField(this.OnPartDestroyedEvent);
                EditorGUILayout.PropertyField(this.OnPartRepairedEvent);
            }

            this.serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif