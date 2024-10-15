using ValueObjects.Data;
using System;
using System.Collections.Generic;


namespace ValueObjects.Data
{
    [Serializable]
    public struct LevelData
    {
        public List<PoolData> Pools;
    }
}