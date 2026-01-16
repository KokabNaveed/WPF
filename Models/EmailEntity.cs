using System;

namespace SubsrciptionSystem.Models
{
    public class EmailEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }


        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Company { get; set; }

        public string Password { get; set; }

        public int StorageGB { get; set; }
    }
}

