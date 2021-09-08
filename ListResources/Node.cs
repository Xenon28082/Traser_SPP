using System;

namespace TracerResources
{
    public class Node
    {
        public TraceRes result;
        public Node next;
        public Node prev;
        public Node()
        {
            this.result = null;
            this.next = null;
            this.prev = null;
        }
    }
}