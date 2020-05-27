﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityProject.Models.Address
{
    public class Continent
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ApplicationUser Added_User { get; set; }

        public List<Country> Countries { get; set; }
    }
}