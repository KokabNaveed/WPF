using System;

namespace SubsrciptionSystem.Models
{
    public class DomainEntity
    {
        public int Id { get; set; }

        public string DomainName { get; set; }
        public string Registrar { get; set; }

        public DateTime RegisteredDate { get; set; }
        public DateTime RenewalDate { get; set; }

        public string NameServer1 { get; set; }
        public string NameServer2 { get; set; }

        public decimal Amount { get; set; }
        public bool AutoRenew { get; set; }
    }
}
