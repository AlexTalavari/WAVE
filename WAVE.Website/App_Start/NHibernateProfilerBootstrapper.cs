using WAVE.Website.App_Start;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof (NHibernateProfilerBootstrapper), "PreStart")]

namespace WAVE.Website.App_Start
{
    public static class NHibernateProfilerBootstrapper
    {
        public static void PreStart()
        {
            // Initialize the profiler
            //NHibernateProfiler.Initialize();

            // You can also use the profiler in an offline manner.
            // This will generate a file with a snapshot of all the NHibernate activity in the application,
            // which you can use for later analysis by loading the file into the profiler.
            // var filename = @"c:\profiler-log";
            // NHibernateProfiler.InitializeOfflineProfiling(filename);
        }
    }
}