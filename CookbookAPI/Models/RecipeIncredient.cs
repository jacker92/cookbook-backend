using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookbookAPI.Models
{
    public class RecipeIncredient
    {
        public Incredient Ingredient { get; set; }
        public double Amount { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; } = UnitOfMeasurement.g;
    }

    public enum UnitOfMeasurement
    {
        g,
        kg,
        ml,
        cl,
        dl,
        l,
        rkl,
        tl,
        mm
    }
}
