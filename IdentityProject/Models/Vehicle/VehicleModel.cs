﻿using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace IdentityProject.Models.Vehicle
{
    public class VehicleModel 
    {

        
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model_Name { get; set; }
        
        public float Popularity { get; set; }
        
        public bool IsActive { get; set; } 
        public float User_Rating { get; set; }
        public DateTime AddedDate { get; set; }
       
        public virtual ApplicationUser Added_User {get;set;}
        public virtual List<Variant> Variant { get; set; }



    }
}