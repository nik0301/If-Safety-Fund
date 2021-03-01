using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IFLike.DAL.Context;
using IFLike.Domain;

namespace IFLike.DAL.Seeds
{
    public class CountrySeeder : IIFLikeSeeder
    {
        public void Seed(IFLikeContext context)
        {
            if (context.Countries.Any())
                return;
            Country[] countries =
            {
                new Country() {CountryCode = "dk", IsAllowed = true, Name = "Denmark"},
                new Country() {CountryCode = "no", IsAllowed = true, Name = "Norway"},
                new Country() {CountryCode = "se", IsAllowed = true, Name = "Sweden"},
                new Country() {CountryCode = "fi", IsAllowed = true, Name = "Finland"},
                new Country() {CountryCode = "es", IsAllowed = true, Name = "Estonia"},
                new Country() {CountryCode = "lv", IsAllowed = true, Name = "Latvia"},
                new Country() {CountryCode = "lt", IsAllowed = true, Name = "Lithuania"},
            };
            foreach (var country in countries)
            {
                context.Add(country);
            }
            context.SaveChanges();
        }
    }
}
