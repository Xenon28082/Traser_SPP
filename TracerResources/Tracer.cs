using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TracerResources
{
    public class Tracer : ITracer
    {
        public List list = new List();
        public List threadList = new List();

        [XmlIgnore]
        private Stack<StackFrame> stack = new Stack<StackFrame>();
        [XmlIgnore]
        private HashSet<int> set = new HashSet<int>();
        [XmlIgnore]
        private StackFrame frame;
        
        public void StartTrace()
        {
            list.Insert(new TraceRes());
            list.currentNode.result.start = DateTime.Now;
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            list.currentNode.result.methodName = methodBase.Name;
            list.currentNode.result.className = methodBase.ReflectedType.Name;
            list.currentNode.result.threadId = Thread.CurrentThread.ManagedThreadId;
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
            
            if (!set.Contains(list.currentNode.result.threadId))
            {
                TraceRes res = new TraceRes();
                res.threadId = list.currentNode.result.threadId;
                res.isTreadNode = true;
                threadList.Insert(res);
                set.Add(list.currentNode.result.threadId);
            }
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

        public int ordinate(List nextStepList)
        {
            Node curr = nextStepList.startNode;
            while (curr != null)
            {
                bool isFirst = true;
                String currName = curr.result.methodName;
                Node currMainList = list.startNode;
                while (currMainList != null)
                {
                    if (String.Compare(currMainList.result.parentName, currName) == 0 || (currMainList.result.threadId == curr.result.threadId && curr.result.isTreadNode))
                    {
                        isFirst = false;
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
                k--;
                curr = curr.next;
            }
            k--;
            // k = 0;
            return 0;
        }

        public async Task serialize()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List));
            using (FileStream fs = new FileStream("C:\\test\\test.json", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, threadList);
            }
        }

        public void countThreadTime()
        {
            Node currThread = threadList.startNode;
            while (currThread != null)
            {
                currThread.result.time = currThread.children.startNode.result.time;
                currThread = currThread.next;
            }
        }
    }
}