using System;

namespace TracerResources
{
    public class Node
    {
        public TraceRes result;
        public Node next;
        public List children;
        public int childrenCount;
        public Node()
        {
            children = new List();
            this.result = null;
            this.next = null;
        }
    }
}