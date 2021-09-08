namespace TracerResources
{
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();

        void GetTraceResult();
    }
}