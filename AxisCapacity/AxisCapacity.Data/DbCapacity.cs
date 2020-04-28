using System;
using System.Text.Json.Serialization;

namespace AxisCapacity.Data
{
    public class DbCapacity
    {
        public string Depot { get; set; }
        public string Day { get; set; }
        
        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime? Date { get; set; }
        
        public string Shift { get; set; }
        public int? Load { get; set; }
        public decimal? Deliveries { get; set; }
        public int? Shifts { get; set; }
        public decimal? Capacity { get; set; }
        
        [JsonIgnoreAttribute]
        public int? GroupId { get; set; }

        public override string ToString()
        {
            return "DbCapacity{" + 
                   "Depot='" + Depot + "'" + 
                   " Day='" + Day + "'" + 
                   " Date='" + Date?.ToString("yyyy/MM/dd") + "'" + 
                   " Shift='" + Shift + "'" + 
                   "}";
        }
    }
}