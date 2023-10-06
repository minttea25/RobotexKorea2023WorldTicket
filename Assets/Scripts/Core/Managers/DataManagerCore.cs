using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

using WorldTicket.Data;

namespace Core
{
    public class DataManagerCore : IManager
    {
        const string ReadConfigPath = "config.json";
        const string TicketConfigPath = "ticket.json";

        public ReadConfig ReadConfig => _readConfig;
        ReadConfig _readConfig;
        public TicketConfig TicketConfig => _ticketConfig;
        TicketConfig _ticketConfig;

        public IReadOnlyDictionary<int, TeamData> Teams = null;
        public IReadOnlyDictionary<Sports, List<TeamData>> SportsTeams = null;


        // TEMP
        //const string path = "C:\\Users\\s_kwy3381\\Desktop\\robotex\\sample.xlsx";


        // add members to load

        public bool Loaded()
        {


            return true;
        }

        public void Init()
        {
            
        }

        public bool LoadConfigs()
        {
            try
            {
                LoadReadConfig();
                LoadTicketConfig();
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public bool Load(string path, ref string err)
        {
            return LoadExcelFile(path, ref err);
        }

        public bool CheckConfigs()
        {
            bool flag = true;

            if (File.Exists(ReadConfigPath) == false)
            {
                flag = false;
                ReadConfig t = new();
                var t_text = JsonUtility.ToJson(t, true);
                File.WriteAllText(ReadConfigPath, t_text);
            }

            if (File.Exists(TicketConfigPath) == false)
            {
                flag = false;
                TicketConfig t = new();
                var t_text = JsonUtility.ToJson(t, true);
                File.WriteAllText(TicketConfigPath, t_text);
            }

            return flag;
        }

        void LoadReadConfig()
        {
            if (File.Exists(ReadConfigPath) == false)
            {
                ReadConfig t = new();
                var t_text = JsonUtility.ToJson(t, true);
                File.WriteAllText(ReadConfigPath, t_text);
            }

            // check encoding utf8? unicode?
            var text = File.ReadAllText(ReadConfigPath);
            _readConfig = JsonUtility.FromJson<ReadConfig>(text);

            if (_readConfig == null) Debug.Log("Failed to parse json file.");
        }

        bool LoadExcelFile(string path, ref string err)
        {
            if (File.Exists(path) == false)
            {
                err = "파일을 찾을 수 없습니다.";
                return false;
            }

            try
            {
                ExcelReader reader = new(path, _readConfig);
                bool flag = reader.ReadFile(ref err);
                if (flag == false) Debug.LogError("Failed to read excel file.");

                Teams = reader.Teams;
                SportsTeams = reader.SportsTeams;

                return flag;
            }
            catch(Exception e)
            {
                Debug.Log(e);
                err = "파일을 읽는 중에 문제가 발생했습니다.";
                return false;
            }
        }

        void LoadTicketConfig()
        {
            if (File.Exists(TicketConfigPath) == false)
            {
                TicketConfig t = new();
                var t_text = JsonUtility.ToJson(t, true);
                File.WriteAllText(TicketConfigPath, t_text);
            }

            // check encoding utf8? unicode?
            var text = File.ReadAllText(TicketConfigPath);
            _ticketConfig = JsonUtility.FromJson<TicketConfig>(text);

            if (_ticketConfig == null) Debug.Log("Failed to parse json file.");
        }

        void IManager.ClearManager()
        {
        }

        void IManager.InitManager()
        {
            Init();
        }
    }
}
