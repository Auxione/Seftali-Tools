#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Seftali.Mech.Part {
    [CustomEditor(typeof(MechPart))]
    [ExecuteInEditMode]
    public class MechPartEditor : Editor {
        private MechPart MechPart;
        private bool showEvents;

        private SerializedProperty OnActivationEvent;
        private SerializedProperty OnTargetSetEvent;
        private SerializedProperty OnDamageReceiveEvent;
        private SerializedProperty OnDestroyedEvent;
        private SerializedProperty OnRepairEvent;
        private SerializedProperty MountSlots;

        private void OnEnable() {
            this.MechPart = this.target as MechPart;
            this.OnActivationEvent = this.serializedObject.FindProperty("OnActivation");
            this.OnTargetSetEvent = this.serializedObject.FindProperty("OnTargetSet");
            this.OnDamageReceiveEvent = this.serializedObject.FindProperty("OnDamageReceive");
            this.OnDestroyedEvent = this.serializedObject.FindProperty("OnDestroyed");
            this.OnRepairEvent = this.serializedObject.FindProperty("OnRepair");

            this.MountSlots = this.serializedObject.FindProperty("MountSlots");
        }

        public override void OnInspectorGUI() {
            //base.OnInspectorGUI();

            this.MechPart.Type = (PartType) EditorGUILayout.EnumPopup("Type", this.MechPart.Type);

            if(this.MechPart.Type == 0) {
                EditorGUILayout.HelpBox("Please select a type.", MessageType.Error);
            }

            //MechPart.Prefix = (PartPrefix) EditorGUILayout.EnumPopup("Prefix", MechPart.Prefix);
            this.MechPart.PartName = EditorGUILayout.TextField("Name", this.MechPart.PartName);

            if(this.MechPart.PartName.Length == 0) {
                EditorGUILayout.HelpBox("Name cannot be empty", MessageType.Error);
            }

            EditorGUILayout.LabelField("DisplayName", this.MechPart.DisplayName);

            GUIStyle style = new GUIStyle(EditorStyles.textArea);
            style.wordWrap = true;
            EditorGUILayout.PrefixLabel("Description");
            this.MechPart.Description = EditorGUILayout.TextArea(this.MechPart.Description, style);

            this.MechPart.MaxHealth = EditorGUILayout.IntField("MaxHealth", this.MechPart.MaxHealth);
            this.MechPart.BasePrice = EditorGUILayout.IntField("BasePrice", this.MechPart.BasePrice);
            this.MechPart.PowerGeneration = EditorGUILayout.IntField("Power Generation", this.MechPart.PowerGeneration);
            this.MechPart.PowerConsumption = EditorGUILayout.IntField("Power Consumption", this.MechPart.PowerConsumption);
            this.MechPart.Activatable = EditorGUILayout.Toggle("Activatable", this.MechPart.Activatable);

            if(this.MechPart.Activatable) {
                EditorGUILayout.PropertyField(this.OnActivationEvent);
            }
            EditorGUILayout.PropertyField(this.MountSlots);


            this.showEvents = EditorGUILayout.Foldout(this.showEvents, "Show Events");

            if(this.showEvents) {
                EditorGUILayout.PropertyField(this.OnTargetSetEvent);
                EditorGUILayout.PropertyField(this.OnDamageReceiveEvent);
                EditorGUILayout.PropertyField(this.OnDestroyedEvent);
                EditorGUILayout.PropertyField(this.OnRepairEvent);
            }
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
