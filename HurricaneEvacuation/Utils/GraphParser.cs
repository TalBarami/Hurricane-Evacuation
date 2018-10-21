using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HurricaneEvacuation.Utils
{
    class GraphParser
    {
        public void CreateGraphFromFile(string path)
        {
            CreateGraphFromString(File.ReadAllLines(path).ToList());
        }

        public void CreateGraphFromString(List<string> data)
        {

        }

        public void ParseLine(string line)
        {
            
        }
    }
}
