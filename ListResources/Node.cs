using System;

namespace TracerResources
{
    public class Node
    {
        public TraceRes result;
        public Node next;
        public Node right;
        public Node()
        {
            this.result = null;
            this.next = null;
            this.right = null;
        }
    }
}