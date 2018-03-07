using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace LinkedListAnotherImplementation
{
    class LinkedList_<T> : ICollection<T>, System.Collections.ICollection, IReadOnlyCollection<T>
           , ISerializable, IDeserializationCallback
    {

        public sealed class LinkedListNode_<T>
        {
            internal LinkedList_<T> list;
            internal LinkedListNode_<T> next;
            internal LinkedListNode_<T> prev;
            internal T item;

            public LinkedListNode_(T value)
            {
                item = value;
            }

            public LinkedListNode_(LinkedList_<T> list, T value)
            {
                this.list = list;
                item = value;
            }

            public LinkedList_<T> List
            {
                get { return list; }
            }
            public LinkedListNode_<T> Next
            {
                get { return next; }
            }
            public LinkedListNode_<T> Prev
            {
                get { return prev; }
            }

            public T Item
            {
                get { return item; }
                set { item = value; }
            }
            internal void Invalidate()
            {
                list = null;
                next = null;
                prev = null;
            }

        }

        internal LinkedListNode_<T> head;
        internal int count;
        internal int version;
        private Object _syncRoot;



        // names for serialization
        const String VersionName = "Version";
        const String CountName = "Count";
        const String ValuesName = "Data";
        //
        public LinkedList_()
        {

        }

        public LinkedList_(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("Collection");
            }

            foreach (T t in collection)
            {
                addLast(t);
            }
        }

        public int Count
        {
            get { return count; }
        }

        public LinkedListNode_<T> First
        {
            get { return head; }
        }
        public LinkedListNode_<T> Last
        {
            get
            {
                LinkedListNode_<T> last = head;
                while (last.next != null)
                {
                    last = last.next;
                }
                return last;
            }
        }
        public void addFirst(T value)
        {
            LinkedListNode_<T> newNode = new LinkedListNode_<T>(this, value);
            if (head == null)
                InternalInsertNodeToEmptyList(newNode);
            else
            {
                InternalInsertNodeBefore(head, newNode);
            }
        }

        public void addLast(T value)
        {
            LinkedListNode_<T> newNode = new LinkedListNode_<T>(this,value);
            if (head == null)
                InternalInsertNodeToEmptyList(newNode);
            else
            {
                InternalInsertNodeBefore(Last, newNode);
            }
        }
        public void addLast(LinkedListNode_<T> node)
        {
            
            if (head == null)
                InternalInsertNodeToEmptyList(node);
            else
            {
                InternalInsertNodeBefore(Last, node);
            }
        }
        public void AddAfter(LinkedListNode_<T> node, T value)
        {
            ValidateNode(node);//check that node is from this linked list
            LinkedListNode_<T> newNode = new LinkedListNode_<T>(node.list,value);
           
            InternalInsertNodeBefore(node.next, newNode);


        }

        public void AddBefore(LinkedListNode_<T> node, T value)
        {
            ValidateNode(node);
            LinkedListNode_<T> newNode = new LinkedListNode_<T>(node.list, value);

            InternalInsertNodeBefore(node, newNode);
        }

        private void InternalInsertNodeBefore(LinkedListNode_<T> node, LinkedListNode_<T> newNode)
        {
            //newNode.next = node;//for circuce linked list
            //newNode.prev = head.prev;
            //newNode.prev = node;
            //node.next = newNode;


            newNode.next = node;
            newNode.prev = node.prev;
            node.prev = newNode;
            count++;

        }

        private void InternalInsertNodeToEmptyList(LinkedListNode_<T> newNode)
        {
            Debug.Assert(head == null && count == 0, "LinkedList must be empty when this method called!");
            //newNode.next = newNode;//to circure linked link
            //newNode.prev = null;
            head = newNode;
            count++;
        }

        public bool IsReadOnly { get { return false; } }

        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public void Add(T item)
        {
            addLast(item);
        }

        public void Clear()
        {
            LinkedListNode_<T> current = head;
            while (current.next != null)
            {
                LinkedListNode_<T> temp = current;
                current = current.next;
                temp.Invalidate();
            }

            head = null;
            count = 0;
        }

        public bool Contains(T item)
        {
            return true;
        }

        public LinkedListNode_<T> Find(T item)
        {
            LinkedListNode_<T> temp = head;
            while (temp.next!=null)
            {
                if (temp.Item == temp.next.item)
                    return temp;
            }
            return null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode_<T> temp = head;
            while (temp.next != null)
            {
                yield return temp.item;
                temp = temp.next;
            }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void OnDeserialization(object sender)
        {
            throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        
        internal void ValidateNode(LinkedListNode_<T> node)
        {
            if (node == null)
                throw new ArgumentNullException("Node");

            if (node.list != this)
            {
                throw new InvalidOperationException();
            }


        }
    }
}