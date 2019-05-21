
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatientsApp.ViewModel
{
    public class BasicVM
    {
        public int PasNumber { get; set; }
        public string Forenames { get; set; }
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string SexCode { get; set; }
        public string HomeTelephoneNumber { get; set; }
    }
}