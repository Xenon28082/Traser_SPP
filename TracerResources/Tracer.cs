using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;

namespace TracerResources
{
    public class Tracer : ITracer
    {
        public List list = new List();

        private Stack <StackFrame>stack = new Stack<StackFrame>();
        
        private StackFrame frame;
        public void StartTrace()
        {
            list.Insert(new TraceRes());
            list.currentNode.result.start = DateTime.Now;
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            list.currentNode.result.methodName = methodBase.Name;
            list.currentNode.result.className = methodBase.ReflectedType.Name;
            frame.className = methodBase.ReflectedType.Name;
            frame.methName = methodBase.Name;
            stack.Push(frame);
        }

        public void StopTrace()
        {
            StackFrame fr = stack.Pop();
            Node curr = list.startNode;
            int count = 0;
            while (curr.next != null) 
            {
                if (String.Compare(curr.result.className, fr.className) == 0 &&
                    String.Compare(curr.result.methodName, fr.methName) == 0)
                {
                    break;
                }
                
                curr = curr.next;
            }
            Console.WriteLine("\n");
            curr.result.time = (DateTime.Now - curr.result.start).TotalMilliseconds;
        }

        public void StopHeadTrace()
        {
            list.startNode.result.time = (DateTime.Now - list.startNode.result.start).TotalMilliseconds;
        }

        public void GetTraceResult()
        {
            // list.currentNode.result.time = (DateTime.Now - list.currentNode.result.start).TotalMilliseconds;
        }
    }
}