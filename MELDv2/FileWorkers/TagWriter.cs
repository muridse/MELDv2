﻿using MELDv2;
using MELDv2.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MELDv2.FileWorkers
{
    internal class TagWriter
    {
        public TagWriter(string path, string connectionName, string db, int offset) 
        {
            Path = path;
            ConnectionName = connectionName;
            DB = db;
            Offset = offset;
            ParseS5Repository();
            WriteFile();
        }
        private string Path { get; set; }
        private string ConnectionName { get; set; }
        private string DB { get; set; }
        private int Offset { get; set; }
        private void ParseS5Repository() 
        {
            foreach (S5Message message in S5MessagesRepository.S5Messages)
            {
                TagsRepository.Add(new Tag
                    (
                        MakeName(message.BigAdress),
                        ConnectionName,
                        ConnectionName + "_Alarms",
                        AdressConverter.ConvertAdressS5toS7(DB, message.Adress, Offset)
                    ));
            }
        }
        private void WriteFile() 
        {
            var newFileName = Path.Replace("_vex.csv", "_MELDv2_vex.csv");
            if (File.Exists(newFileName))
                File.Delete(newFileName);
            File.Copy(Path, newFileName);

            using (StreamWriter tagWriter = new StreamWriter(newFileName, true))
            {
                foreach (var tag in TagsRepository.Tags)
                {
                    tagWriter.WriteLine(tag.ToString());
                }
            }
        }
        private string MakeName(string adress) =>
            ConnectionName + "_" + adress.Replace('.', '_');
    }
}
