using CourseWork.BLL.Interfaces;
using CourseWork.BLL.Models;
using System;
using System.Collections.Generic;

namespace CourseWork.BLL.Services.ElementLoaders.Txt
{
    public class TxtLoadsProvider : BaseTxtProvider, ILoadsProvider
    {
        private const string Separator = "\t";
        private readonly string _nodeLoadsPath;

        public TxtLoadsProvider(string nodeLoadsPath)
        {
            _nodeLoadsPath = nodeLoadsPath;
        }

        public List<NodeLoad> GetNodeLoads()
        {
            var loads = new List<NodeLoad>();
            var rows = GetValuesFromTextFile(_nodeLoadsPath, Separator, 1);
            foreach (var row in rows)
            {
                if (row.Length != 5)
                {
                    throw new InvalidOperationException($"Invalid format of node load '{string.Join(" ", row)}");
                }
                if (int.TryParse(row[0], out var nodeId)
                    && Enum.TryParse<LoadType>(row[1], true, out var loadType)
                    && double.TryParse(row[2], out var x)
                    && double.TryParse(row[3], out var y)
                    && double.TryParse(row[4], out var z))
                {
                    loads.Add(new NodeLoad
                    {
                        NodeId = nodeId-1,
                        LoadType = loadType,
                        X = x,
                        Y = y,
                        Z = z,
                    });
                }
                else
                {
                    throw new InvalidOperationException($"Couldn't parse node load values '{string.Join(" ", row)}'.");
                }
            }

            return loads;
        }
    }
}
