using System.Collections.Generic;

namespace TestFramework.Models
{
    public class Element
    {
        public string Name { get; set; }
        public string Locator { get; set; }
        public bool? IsMandatory { get; set; }
    }
}