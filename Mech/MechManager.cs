using System;
using System.Collections.Generic;
using System.IO;
using Seftali.Mech.Part;
using UnityEngine;

namespace Seftali.Mech {

    [CreateAssetMenu(menuName = "ModularMech/Manager")]
    public class MechManager : ScriptableObject {
        public static string DataPath { get => Application.dataPath; }
        [Header("Mech Instantiation")]
        public Mech MechPrefab;

        [Header("Data Handling")]
        public string MechDataPath = "Mechs/";
        public string MechDataExtension = ".mech";

        public List<MechPart> PartList;

        public void OnEnable() {
            this.RefreshDirectory();
        }

        #region Data Handling

        private FileInfo[] dataFiles;
        private DirectoryInfo directoryInfo;
        public string[] FileNames { private set; get; }

        [ContextMenu("RefreshDirectory")]
        public void RefreshDirectory() {
            this.directoryInfo = new DirectoryInfo(DataPath + "/" + this.MechDataPath);
            if(!this.directoryInfo.Exists) {
                this.directoryInfo.Create();
            }

            this.dataFiles = this.directoryInfo.GetFiles("*" + this.MechDataExtension);

            this.FileNames = new string[this.dataFiles.Length];
            for(int i = 0; i < this.FileNames.Length; i++) {
                this.FileNames[i] = this.dataFiles[i].Name;
            }
        }
        public MechData Load(int index) {
            return SerializationManager.Load(this.dataFiles[index].FullName) as MechData;
            // string data = File.ReadAllText(this.dataFiles[index].FullName);
            // return JsonUtility.FromJson<MechData>(data);
        }

        public void Save(MechData mechData) {
            SerializationManager.Save(DataPath + "/" + this.MechDataPath + mechData.DisplayName + this.MechDataExtension, mechData);

            // string data = JsonUtility.ToJson(mechData);
            // File.WriteAllText(DataPath + MechDataPath + mechData.DisplayName + MechDataExtension, data);
        }

        public void Delete(int index) {
            this.dataFiles[index].Delete();
        }
        #endregion

        #region Mech Instantiation
        public GameObject Construct(MechData MechData, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), bool spawnActive = false) {
            if(!this.ValidateMechData(MechData)) {
                return null;
            }
            GameObject spawnedGameObject = Instantiate(this.MechPrefab.gameObject, position, rotation);
            spawnedGameObject.SetActive(spawnActive);
            spawnedGameObject.name = MechData.DisplayName;

            this.SpawnPartRecursively(spawnedGameObject.transform, MechData.RootPartData);

            //spawn parts to its mountslot on part
            //legs
            //--body
            //----limb1
            //------weap1
            //----limb2
            //------weap2

            spawnedGameObject.GetComponent<Mech>().Initialize();
            //return spawned mech
            return spawnedGameObject;
        }
        private void SpawnPartRecursively(Transform parent, PartData PartData) {
            // Instantiate partID from prefablist and add it to spawnedlist    
            var spawnedMechPart = Instantiate(this.PartList[PartData.ID].gameObject, parent);
            var component = spawnedMechPart.GetComponent<MechPart>();
            component.Prefix = PartData.Prefix;
            spawnedMechPart.name = component.PartName;

            // do it for every child
            for(int i = 0; i < PartData.Length; i++) {
                if(PartData[i] != null) {
                    var partParent = spawnedMechPart.GetComponent<MechPart>().MountSlots[i].transform;
                    this.SpawnPartRecursively(partParent, PartData[i]);
                }
            }
        }

        public bool ValidateMechData(MechData mechData) {
            if(mechData.RootPartData == null) {
                Debug.LogError("MechData is corrupted!");
                return true;
            }
            return this.ValidateRecursively(null, mechData.RootPartData, 0);
        }

        private bool ValidateRecursively(PartData root, PartData current, int mountindex) {
            if(root != null) {
                var rootPart = this.PartList[root.ID];
                var currentPart = this.PartList[current.ID];

                if(!rootPart.MountSlots[mountindex].Compatible(currentPart)) {
                    Debug.LogError("Incompatible part on mount index: " + mountindex);
                    return false;
                }
            }

            for(int i = 0; i < current.Length; i++) {
                if(current[i] == null)
                    continue;
                if(!this.ValidateRecursively(current, current[i], i)) {
                    return false;
                }
            }
            return true;
        }

        public MechData Deconstruct(Mech mech) {
            throw new NotImplementedException();
            // MechPart rootpart = mech.transform.GetChild(0).GetComponent<MechPart>();

            // MechData mechdata = new MechData();
            // mechdata.DisplayName = modularMech.name;


            // return mechdata;
        }

        #endregion

        #region Debugging
        [ContextMenu("Debug/Create And Save DebugMech")]
        public void debugCreateSave() {
            MechData mechData = new MechData();
            mechData.DisplayName = "DebugMecha";
            mechData.RootPartData = new PartData(0, PartPrefix.None, 2);
            mechData.RootPartData[0] = new PartData(1, PartPrefix.Simple, 0);
            mechData.RootPartData[1] = new PartData(1, PartPrefix.Rusted, 0);

            this.Save(mechData);
        }

        [ContextMenu("Debug/Load And Construct DebugMech")]
        public void debugLoadBuilt() {
            var mechdata = this.Load(0);
            this.Construct(mechdata);
        }
        #endregion

    }
}
