using System;
using System.Collections.Generic;
using System.Text;

namespace LOR.Pizzeria
{
    public class Recipe
    {
        public string Name { get; set; }

        public List<string> Ingredients { get; set; } = new List<string>();


        public List<BakeingInstructions> BakeingSteps { get; set; }

        public int Slices { get; set; }

    }
}
