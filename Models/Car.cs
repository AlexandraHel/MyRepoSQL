using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Seido.Utilities.SeedGenerator;

namespace Models
{
	public class Car  : ISeed<Car>
	{   
        [Key]
        public Guid CarId {get; set;}

        public string RegNumber {get; set;}
        public string Make {get; set;}
        public string Model {get; set;}

        //Nav props
        public Owner Owner {get; set;} = null;

        public Garage Garage {get; set;} = null;
        

        public bool Seeded { get; set; } = false;

        public Car Seed(SeedGenerator seeder)
        {
            char rc1 = (char) seeder.Next('A', 'Z');
            char rc2 = (char) seeder.Next('A', 'Z');
            char rc3 = (char) seeder.Next('A', 'Z');
            int regnr = seeder.Next(111,999);

            return new Car {
                CarId = Guid.NewGuid(),
                RegNumber = $"{rc1}{rc2}{rc3} {regnr}",
                Make = seeder.FromString("BMW, Fiat, Volvo, VW, Audi, Suzuki, Toyota"),
                Model = seeder.FromString("S500, S300, Ciao, V70, Polo, Quatro, Swift, Corolla"),
                Seeded = true
            };
        }
    }
}