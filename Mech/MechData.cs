using System.Runtime.Serialization;
using Seftali.Mech.Part;

namespace Seftali.Mech {
    public class MechData {
        //each child data represents part in mech
        //first part Data must be mech movement unit

        public PartData RootPartData;
        public string DisplayName;
    }

    public class MechDataSurrogate : ISerializationSurrogate {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) {
            MechData data = (MechData) obj;
            info.AddValue("MechName", data.DisplayName);
            info.AddValue("RootPartData", data.RootPartData);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {
            MechData data = (MechData) obj;
            data.DisplayName = (string) info.GetValue("MechName", typeof(string));
            data.RootPartData = (PartData) info.GetValue("RootPartData", typeof(PartData));
            return data;
        }
    }

    public class PartData {
        public int ID;
        public PartPrefix Prefix;
        public PartData[] Childs;
        public int Length => this.Childs.Length;

        public PartData(int ID, PartPrefix prefix, int childCount) {
            this.ID = ID;
            this.Prefix = prefix;
            this.Childs = new PartData[childCount];
        }

        public PartData this[int index] {
            get => this.Childs[index];
            set => this.Childs[index] = value;
        }
    }

    public class PartDataSurrogate : ISerializationSurrogate {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) {
            PartData data = (PartData) obj;
            info.AddValue("ID", data.ID);
            info.AddValue("Prefix", data.Prefix);
            info.AddValue("Childs", data.Childs);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {
            PartData data = (PartData) obj;
            data.ID = (int) info.GetValue("ID", typeof(int));
            data.Prefix = (PartPrefix) info.GetValue("Prefix", typeof(PartPrefix));
            data.Childs = (PartData[]) info.GetValue("Childs", typeof(PartData[]));
            return data;
        }
    }
}
