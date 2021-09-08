using System;

namespace TracerResources
{
    public class TraceRes
    {
        public String methodName;
        public String className;
        public double time;
        public DateTime start;

        public TraceRes()
        {
            methodName = null;
            className = null;
        }
    }
}