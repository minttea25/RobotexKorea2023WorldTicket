using OfficeOpenXml;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

using WorldTicket.Utils;

namespace WorldTicket.Data
{
    public class ExcelReader
    {
        public string FilePath => _filepath;
        string _filepath;

        readonly Dictionary<string, Sports> _sportsDict;
        readonly ReadConfig _config;

        public IReadOnlyDictionary<int, TeamData> Teams => _teams;
        public IReadOnlyDictionary<Sports, List<TeamData>> SportsTeams => _sportsTeams;
        readonly Dictionary<int, TeamData> _teams;
        readonly Dictionary<Sports, List<TeamData>> _sportsTeams;

        public ExcelReader(string filepath, ReadConfig config)
        {
            _filepath = filepath;
            _config = config;
            _sportsDict = ExcelData.GetSportNameDictionary(_config);
            _teams = new();
            _sportsTeams = new();
            foreach(Sports s in _sportsDict.Values)
            {
                _sportsTeams.Add(s, new());
            }

            InitEPPlus();
        }

        void InitEPPlus()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public bool ReadFile(ref string err)
        {
            bool flag = true;

            flag &= File.Exists(_filepath);
            if (flag == false)
            {
                err = "파일 경로가 잘못되었습니다.";
                return flag;
            }

            using (ExcelPackage package = new ExcelPackage(new FileInfo(_filepath)))
            {
                ExcelWorksheets sheet = package.Workbook.Worksheets;

                HashSet<int> duplicatedCheckSet = new();
                
                foreach (ExcelWorksheet s in sheet)
                {
                    if (s.Name != _config.SheetName) continue;

                    int rows = s.Dimension.Rows;
                    int columns = s.Dimension.Columns;

                    for (int r = 1; r <= rows; r++)
                    {
                        TeamData team = new();

                        var organization = s.Cells[r, _config.OrganizationColumnIndex].Value.ToString();
                        var sport = s.Cells[r, _config.SportsColumnIndex].Value.ToString();
                        var no = s.Cells[r, _config.TeamNumberColumnIndex].Value.ToString();
                        var name = s.Cells[r, _config.TeamNameColumnIndex].Value.ToString();

                        if (string.IsNullOrWhiteSpace(organization) == true) continue;
                        else team.Organization = organization;

                        if (string.IsNullOrWhiteSpace(sport) == true
                            || _sportsDict.ContainsKey(sport) == false) continue;
                        else team.Sport = _sportsDict[sport];

                        if (string.IsNullOrWhiteSpace(no) == true
                            || int.TryParse(no, out int num) == false) continue;
                        else team.TeamNo = num;

                        if (string.IsNullOrWhiteSpace(name) == true) continue;
                        else team.TeamName = name;

                        if (duplicatedCheckSet.Add(num) == false) continue;

                        team.Index = r;

                        _teams.Add(r, team);
                        _sportsTeams[team.Sport].Add(team);
                    }
                }
            }

            //Debug.Log($"Number of teams: {_teams.Count}");
            //Debug.Log($"Number of sumo-1kg teams: {_sportsTeams[Sports.LEGO_ROBOT_SUMO_1KG].Count}");
            //Debug.Log($"Number of sumo-3kg teams: {_sportsTeams[Sports.LEGO_ROBOT_SUMO_3KG].Count}");
            flag &= _teams.Count > 0;

            if (flag == false)
            {
                err = "로드된 팀이 없습니다. 파일을 확인해주세요.";
                return false;
            }
            flag &= _config.NumberOfTeams == _teams.Count;
            if (flag == false)
            {
                err = $"로드된 팀의 수({_teams.Count})와 설정파일에 입력된 팀의 수({_config.NumberOfTeams})가 맞지 않습니다.";
                return false;
            }

            _sportsTeams[Sports.LEGO_ROBOT_SUMO_1KG].Shuffle();
            _sportsTeams[Sports.LEGO_ROBOT_SUMO_3KG].Shuffle();

            return flag;
        }
    }
}
