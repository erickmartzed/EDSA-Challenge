﻿using MASDataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASBusiness.DTOs
{
    public class ColorDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}