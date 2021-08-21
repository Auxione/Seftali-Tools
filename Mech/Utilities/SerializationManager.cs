using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Seftali.Mech {
    public class SerializationManager {
        public static void Save(string filepath, object data) {
            BinaryFormatter formatter = GetBinaryFormatter();
            FileStream stream = File.Create(filepath);

            try {
                formatter.Serialize(stream, data);
            } catch {
                Debug.LogErrorFormat("Failed to save File to {0}", filepath);
            } finally {
                stream.Close();
            }
        }

        public static object Load(string path) {
            if(!File.Exists(path)) {
                return null;
            }

            BinaryFormatter formatter = GetBinaryFormatter();
            FileStream stream = File.Open(path, FileMode.Open);

            try {
                object save = formatter.Deserialize(stream);
                return save;
            } catch {
                Debug.LogErrorFormat("Failed to load File at {0}", path);
                return null;
            } finally {
                stream.Close();
            }
        }

        public static BinaryFormatter GetBinaryFormatter() {
            SurrogateSelector selector = new SurrogateSelector();
            selector.AddSurrogate(typeof(MechData), new StreamingContext(StreamingContextStates.All), new MechDataSurrogate());
            selector.AddSurrogate(typeof(PartData), new StreamingContext(StreamingContextStates.All), new PartDataSurrogate());

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.SurrogateSelector = selector;
            return formatter;
        }
    }
}
