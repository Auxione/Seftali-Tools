using System.Collections.Generic;
using System;

namespace Seftali.Nodemap {
    /// <summary>
    /// The class that stores and controls id pairs. Pairs are stored as ids in a list. 
    /// Array index operator can be used for manually access the ids stored in the list. 
    /// </summary>
    [Serializable]
    public class ConnectionList {
        private List<int> connections = new List<int>();

        public int  Length => connections.Count;

        /// <summary>
        /// Scans the list for given parameters. If any duplicates found or both ids are same, function returns false without adding. 
        /// </summary>
        /// <param name="id1">First id to add.</param>
        /// <param name="id2">Second id to add.</param>
        /// <returns>True if operation is successiful. If any duplicates found or both ids are same, returns False.</returns>
        public bool AddPair(int id1, int id2) {
            if(id1 == id2) {
                return false;
            }

            for(int i = 0; i < this.connections.Count; i += 2) {
                int pair1 = this.connections[i];
                int pair2 = this.connections[i + 1];
                if(id1 == pair1 && id2 == pair2) {
                    return false;
                } else if(id1 == pair2 && id2 == pair1) {
                    return false;
                }
            }

            this.connections.Add(id1);
            this.connections.Add(id2);

            return true;
        }

       public int this[int index]{
            get => connections[index];
            set => connections[index] = value;
        }

        /// <summary>
        /// Removes all ids with their pair in the list that matches the given id. 
        /// </summary>
        /// <param name="id">Id to remove.</param>
        public void Remove(int id) {
            for(int i = connections.Count - 1; i >= 0; i--) {
                if(connections[i] == id) {
                    RemovePairAt(i);
                }
            }
        }

        /// <summary>
        /// Returns all id pairs for given id.
        /// </summary>
        /// <param name="id">the id to get connections.</param>
        /// <returns>Array of pairs</returns>
        public int[] GetConnections(int id) {
            List<int> templist = new List<int>();
            for(int i = 0; i < this.connections.Count; i++) {
                if(id == this.connections[i]) {
                    templist.Add(this.GetPair(i));
                }
            }
            return templist.ToArray();
        }

        /// <summary>
        /// Removes both index and pair from list.
        /// </summary>
        /// <param name="index">List index to remove.</param>
        private void RemovePairAt(int index) {
            connections.RemoveAt(index);
            if(index % 2 == 0) {
                connections.RemoveAt(index + 1);
            } else {
                connections.RemoveAt(index - 1);
            }
        }

        /// <summary>
        /// Returns pair id from given index.
        /// </summary>
        /// <param name="index">the index to get pair id</param>
        /// <returns>the pair id.</returns>
        private int GetPair(int index) {
            if(index % 2 == 0) {
                return this.connections[index + 1];
            } else {
                return this.connections[index - 1];
            }
        }

    }
}