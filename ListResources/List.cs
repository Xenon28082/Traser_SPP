using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TracerResources
{
    public class List
    {
        public Node startNode;
        [XmlIgnore]
        public Node currentNode;
        [XmlIgnore]
        public int size;

        public HashSet<String> parentNames;
        public void print() {
            Node node = startNode;
            while (node != null) {
                Console.WriteLine("Time - " + node.result.time);
                Console.WriteLine("Method - " + node.result.methodName);
                Console.WriteLine("Parent Method - " + node.result.parentName);
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
            currentNode = node;
        }
        
    }
}