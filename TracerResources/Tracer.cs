using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace TracerResources
{
    public class Tracer : ITracer
    {
        public List list = new List();

        private Stack<StackFrame> stack = new Stack<StackFrame>();

        private StackFrame frame;
        public void StartTrace()
        {
            list.Insert(new TraceRes());
            list.currentNode.result.start = DateTime.Now;
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            list.currentNode.result.methodName = methodBase.Name;
            list.currentNode.result.className = methodBase.ReflectedType.Name;
            try
            {
                list.currentNode.result.parentName = stackTrace.GetFrame(2).GetMethod().Name;
            }
            catch (Exception e)
            {
                Console.WriteLine("It's main; there is no parent method");
            }
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

        public int ordinate(List nextStepList)
        {

            Node curr = nextStepList.startNode;
            while (curr != null)
            {
                String currName = curr.result.methodName;
                Node currMainList = list.startNode;
                while (currMainList != null)
                {
                    if (String.Compare(currMainList.result.parentName, currName) == 0)
                    {
                        list.startNode.childrenCount++;
                        curr.children.Insert(currMainList.result);
                    }
                    currMainList = currMainList.next;
                }
                curr = curr.next;
            }
            if (list.startNode.childrenCount == list.size)
            {
                return 0;
            }
            try
            {
                return ordinate(nextStepList.startNode.children);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception");
            }
            return 0;
        }

        private int k = 0;
        public int printChilds(Node start)
        {

            String buf = "";
            list.startNode.next = null;

            Node curr = start;
            while (curr != null)
            {
                for (int i = 0; i < k; i++)
                {
                    buf += "      ";
                }
                Console.WriteLine(buf + "Time - " + curr.result.time);
                Console.WriteLine(buf + "Method - " + curr.result.methodName);
                Console.WriteLine(buf + "Parent Method - " + curr.result.parentName);
                Console.WriteLine(buf + "Class - " + curr.result.className + "\n");
                if (curr.children != null)
                {
                    k++;
                    printChilds(curr.children.startNode);
                }

                curr = curr.next;
            }
            k = 0;
            return 0;
        }


    }
}