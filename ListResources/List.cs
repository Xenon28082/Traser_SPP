using System;

namespace TracerResources
{
    public class List
    {
        public Node startNode;
        public Node currentNode;
        public int size;
        public void print() {
            Node node = startNode;
            while (node != null) {
                Console.WriteLine("Time - " + node.result.time);
                Console.WriteLine("Method - " + node.result.methodName);
                Console.WriteLine("Class - " + node.result.className);
                node = node.next;
                Console.WriteLine("\n");
            }
            
        }
        
        public void Insert(TraceRes value)
        {
            Node node = new Node();
            node.result = value;
            if (startNode == null)
            {
                startNode = node;
                currentNode = startNode;
                size++;
                return;
            }

            size++;
            currentNode.next = node;
            node.prev = currentNode;
            currentNode = node;
        }
        
    }
}