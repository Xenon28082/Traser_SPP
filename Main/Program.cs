using System;
using TracerResources;

class Program
    {
        static Tracer tracer = new Tracer();

        static int getMult(int x, int y)
        {
            tracer.StartTrace();
            int result = x * y;
            tracer.StopTrace();
            return result;
        }
        static int getSum(int x, int y)
        {
            tracer.StartTrace();
            int result = x + y;
            getMult(5, 3);
            tracer.StopTrace();
            return result;
        }
        
        static int getDif(int x, int y)
        {
            tracer.StartTrace();
            int result = x - y;
            tracer.StopTrace();
            return result;
        }
        
        static void Main(string[] args)
        {
            tracer.StartTrace();
            getSum(3, 5);
            getDif(5, 3);
            tracer.StopHeadTrace();
            tracer.list.print();
        }
    }