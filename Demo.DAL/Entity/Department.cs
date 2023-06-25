﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Entity
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(20)]
        public string Name { get; set; }

        [Required, StringLength(10)]

        public string Code { get; set; }
    }
}