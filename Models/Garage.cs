using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Seido.Utilities.SeedGenerator;

namespace Models
{
	public class Garage  : ISeed<Garage>
	{   
        [Key]
        public Guid GarageId {get; set;}
        public string Name {get; set;}

        //Nav props
        public List<Car> Cars {get; set;} = null;


        public bool Seeded { get; set; } = false;
        public Garage Seed(SeedGenerator seeder)
        {
            string name = $"{seeder.PetName}\'s Garage" ;

            return new Garage{
                GarageId= Guid.NewGuid(),
                Name = name,
                Seeded = true
            };
        }
    }
}