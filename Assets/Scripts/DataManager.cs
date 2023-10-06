using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WorldTicket.Utils;
using WorldTicket.Data;
using System.IO;

namespace WorldTicket
{
    public class DataManager : Singleton<DataManager>
    {
        const string ConfigFilePath = "config.json";

        public ReadConfig Config => _config;
        ReadConfig _config;

        public IReadOnlyDictionary<int, TeamData> Teams = null;


        // TEMP
        const string path = "C:\\Users\\s_kwy3381\\Desktop\\robotex\\sample.xlsx";

        private void Awake()
        {
            
        }

        
    }
}
