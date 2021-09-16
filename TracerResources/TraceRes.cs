using System;
using System.Xml.Serialization;

namespace TracerResources
{
    public class TraceRes
    {
        public String methodName;
        public String className;
        public double time;
        
        [XmlIgnore]
        public DateTime start;
        [XmlIgnore]
        public String parentName;
        public int threadId;
        [XmlIgnore]
        public bool isTreadNode;
        public TraceRes()
        {
            parentName = "first";
            methodName = null;
            className = null;
            isTreadNode = false;
        }
    }
}