﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspfunda.ViewModel
{
    public class EmployeeEditViewModel : EmployeeCreateViewmodel
    {
        public int  Id { get; set; }
        public string  ExistingPhotoPath { get; set; }
    }
}