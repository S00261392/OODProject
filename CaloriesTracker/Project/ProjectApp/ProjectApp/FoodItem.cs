//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class FoodItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public string FoodItemImage { get; set; }
        public int FoodTypeId { get; set; }
    
        public virtual FoodType FoodType { get; set; }
    }
}