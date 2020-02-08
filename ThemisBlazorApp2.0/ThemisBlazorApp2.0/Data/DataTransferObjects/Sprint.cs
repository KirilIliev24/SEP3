using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThemisBlazorApp2._0.Data.DataTransferObjects
{
    public class Sprint
    {
        public int Number { get; set; }
        public List<BackLogItem> BacklogItems { get; set; }
        public Sprint()
        {
        }
        public Sprint(int number)
        {
            Number = number;            
        }
        public int totalEstimatedHours()
        {
            int hours = 0;
            if(BacklogItems != null)
                for(int i=0; i<BacklogItems.Count; i++)
                {
                    hours += BacklogItems[i].EstimatedTime;
                }
            return hours;
        }
    }
}
