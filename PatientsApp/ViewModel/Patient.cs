using PatientsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientsApp.ViewModel
{
    public class Patient
    {
        public BasicVM basic { get; set; }
        public NextOfKinVM nextOfSkin { get; set; }
        public GpDetailVM gpDetail { get; set; }
    }
}