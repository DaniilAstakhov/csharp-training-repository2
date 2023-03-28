﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    internal class GroupData
    {
        private string name;
        private string header = "";
        private string footer = "";

        public GroupData(string name) //конструктор только для имени
        {
            this.name = name;
        }

        public GroupData(string name,string header, string footer) //конструктор для имени хэдэра и футера
        {
            this.name = name; 
            this.header = header;
            this.footer = footer;
        }
        public string Name { get { return name; } set { name = value; } }

        public string Header { get { return header; } set { header = value; } }
        public string Footer { get { return footer; } set { footer = value; } }
    }
}