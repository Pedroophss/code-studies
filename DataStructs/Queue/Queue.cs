namespace DataStructs.Queue
{
    public class LinkedListQueue
    {
        private class Node
        {
            public readonly int Value;
            public Node Next { get; set; }
            public Node Previus { get; set; }

            public Node(int value)
            {
                Value = value;
            }
        }

        private Node _Rear { get; set; }
        private Node _Front { get; set; }

        public int Rear => _Rear.Value;
        public int Front => _Front.Value;
        public bool IsEmpty => _Front == null;

        public void Enqueue(int value)
        {
            var aux = new Node(value);

            if (_Front == null)
                _Front = aux;

            aux.Next = _Rear;
            if (_Rear != null)
                _Rear.Previus = aux;

            _Rear = aux;
        }

        public int Dequeue()
        {
            var aux = _Front;
            _Front = aux.Previus;
            if (_Front != null)
                _Front.Next = null;

            return aux.Value;
        }
    }
}