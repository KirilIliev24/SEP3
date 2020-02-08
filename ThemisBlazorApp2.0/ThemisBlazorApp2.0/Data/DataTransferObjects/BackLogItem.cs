using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThemisBlazorApp2._0.Data.DataTransferObjects
{
    public class BackLogItem
    {
        public int Id;
        public String Priority { get; set; }
        public String Description { get; set; }
        public int EstimatedTime { get; set; }
        public String Status { get; set; }
        public User WorksOnIt { get; set; }
        public int ActualTime { get; set; }

        public BackLogItem()
        {
        }

        public BackLogItem(string priority, int estimatedTime, string description)
        {
            this.Priority = priority;
            this.EstimatedTime = estimatedTime;
            this.Description = description;
        }
    }
}
