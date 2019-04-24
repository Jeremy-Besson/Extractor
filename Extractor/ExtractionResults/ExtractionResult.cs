using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Extractor.ExtractionResults
{
    class ExtractionResult
    {
        public DateTime DateTime { get; set; }
        public Version Version { get; set; }
        public DataSetting DataSetting { get; set; }
        public Results ExtractionResults { get; set; }
        public Dictionary<string, string> stats { get; set; }
    }

    internal class Results
    {
    }

    internal class DataSetting
    {
    }
}
