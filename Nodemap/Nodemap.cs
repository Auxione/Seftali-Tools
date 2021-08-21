using System;
using System.Collections.Generic;

namespace Seftali.Nodemap {
    [Serializable]
    public class Nodemap<T> {
        private readonly List<T> nodes;
        private ConnectionList connectionList;

        public int Length => nodes.Count;

        public Nodemap() {
            this.nodes = new List<T>();
            connectionList = new ConnectionList();
        }

        public T this[int index] {
            get => this.nodes[index];
            set => this.nodes[index] = value;
        }

        /// <summary>
        /// Girilen iki indeksi baglanti listesine ekler.
        /// </summary>
        /// <param name="index1">Bağlanacak birinci indeks</param>
        /// <param name="index2">Bağlanacak ikinci indeks</param>
        /// <returns>Listeye ekleme işlemi basarili olursa True değilse False </returns>
        public bool Connect(int index1, int index2) {
            if(index1 >= Length || index2 >= Length) {
                return false;
            }
            return connectionList.AddPair(index1, index2);
        }

        /// <summary>
        /// Girilen indeksteki nodu listeden siler.
        /// </summary>
        /// <param name="index">Silinecek indeks.</param>
        public void RemoveAt(int index) {
            this.nodes.RemoveAt(index);
            connectionList.Remove(index);
            for(int i = 0; i < this.connectionList.Length; i++) {
                if(index >= this.connectionList[i]) {
                    this.connectionList[i]--;
                }
            }
        }

        /// <summary>
        /// Girilen indekse bağlı bütün eşlerini geri döndürür.
        /// </summary>
        /// <param name="id">Eşleri bulunacak id</param>
        /// <returns>idnin eşleri</returns>
        public int[] GetConnections(int index) {
            if(index >= Length) {
                throw new IndexOutOfRangeException();
            }
            return connectionList.GetConnections(index);
        }
    }
}
