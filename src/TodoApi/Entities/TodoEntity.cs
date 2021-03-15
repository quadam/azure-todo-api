using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApi.Entities
{
    public class TodoEntity : TableEntity
    {
        public string Content { get; set; }
        public bool Completed { get; set; }
    }
}
