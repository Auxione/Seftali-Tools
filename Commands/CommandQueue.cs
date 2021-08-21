using System.Collections.Generic;

namespace Seftali.Commands {
    /// <summary>
    /// FIFO Queue class for <see cref="ICommand"/> interface.
    /// </summary>
    public class CommandQueue {
        protected readonly List<ICommand> list = new List<ICommand>();

        /// <summary>
        /// Inserts the specified <see cref="ICommand"/> into this queue.
        /// </summary>
        public void Add(ICommand command) => this.list.Add(command);

        /// <summary>
        /// Removes the last <see cref="ICommand"/>.
        /// </summary>
        public void Remove() => this.list.RemoveAt(this.list.Count - 1);

        public void Clear() => this.list.Clear();
        public int Count => this.list.Count;

        public ICommand this[int index] {
            get => this.list[index];
        }

        /// <summary>
        /// Removes all commands added after index.
        /// </summary>
        public void RemoveAfter(int index) {
            while(index < this.list.Count) {
                this.Remove();
            }
        }

        /// <summary>
        /// Retrieves and removes the head of this queue, returns null if this queue is empty.
        /// </summary>
        /// <returns><see cref="ICommand"/> on head of the queue or null.</returns>
        public ICommand Poll() {
            if(this.list.Count == 0) {
                return null;
            }

            var command = this.list[0];
            this.list.RemoveAt(0);
            return command;
        }

        /// <summary>
        /// Retrieves but does not remove the head of this queue, returns null if this queue is empty.
        /// </summary>
        /// <returns><see cref="ICommand"/> on head of the queue or null.</returns>
        public ICommand Peek() {
            if(this.list.Count == 0) {
                return null;
            }
            return this.list[0];
        }
    }
}
