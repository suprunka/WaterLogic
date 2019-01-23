using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

using System.Web;

namespace WebApplication.Models
{
    public class Machines
    {
    }

    public class MachineOfferModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //1- small, 2- medium, 3- big
        public int Type { get; set; }

        public double Price { get; set; }
    }
    public class AddMachineModel
    {
        public int Id { get; set; }
        [MinLength(6)]
        public string Name { get; set; }
        [MinLength(15)]
        public string Description { get; set; }
        //1- small, 2- medium, 3- big
        public int Type { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "The price must be a number.")]
        public double Price { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "The quantity must be a number.")]
        public int Quantity { get; set; }
        public bool Succed { get; set; }
    }
}