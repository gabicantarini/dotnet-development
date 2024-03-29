﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Core.Entities
{
    public class Donator : BaseEntity
    {
        public Donator(string name, string email, DateTime birthDate, string gender, double weight, string bloodType, string rhFactor, string address, bool active)
        {
            FullName = name;
            Email = email;
            BirthDate = birthDate;
            Gender = gender;
            Weight = weight;
            BloodType = bloodType;
            RhFactor = rhFactor;
            Active = true;
            Donations = new List<Donation>();
            Active = active;
        }
        protected Donator() { }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Gender { get; private set; }
        public double Weight { get; private set; }
        public string BloodType { get; private set; }
        public string RhFactor { get; private set; }
        public List<Donation> Donations { get; private set; }    
        public bool Active { get; private set; }
    }

}

