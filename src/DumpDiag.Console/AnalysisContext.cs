﻿using Microsoft.Diagnostics.Runtime;

namespace DumpDiag.Console
{
    internal class AnalysisContext
    {
        public DataTarget DataTarget { get; }

        public ClrRuntime Runtime { get; }

        private AnalysisContext(DataTarget dataTarget, ClrRuntime runtime)
        {
            DataTarget = dataTarget;
            Runtime = runtime;
        }

        public static AnalysisContext FromProcessDump(string processDump, string dacPath)
        {
            var dataTarget = DataTarget.LoadCrashDump(processDump);
            ClrInfo runtimeInfo = dataTarget.ClrVersions[0];  // just using the first runtime
            ClrRuntime runtime = runtimeInfo.CreateRuntime(dacPath, true);
            return new AnalysisContext(dataTarget, runtime);
        }
    }
}