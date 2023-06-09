﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MELDv2.Repositories
{
    static public class TagsRepository
    {
        public static List<Tag> Tags = new List<Tag>();
        public static void Add(Tag tag) =>
            Tags.Add(tag);
    }
    public class Tag
    {
        public Tag(string tagName, string connection, string folder, string adress)
        {
            TagName = tagName;
            Connection = connection;
            Folder = folder;
            Adress = adress;
        }
        private string TagName { get; set; }
        private string Connection { get; set; }
        private string Folder { get; set; }
        private string Adress { get; set; }

        private string Filler = "0;1;1;4;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0";

        public override string ToString()
        {
            return String.Format($"{TagName};{Connection};{Folder};{Adress};{Filler}");
        }
    }

}
