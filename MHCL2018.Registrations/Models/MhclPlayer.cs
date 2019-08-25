//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MHCL2018.Registrations.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MhclPlayer
    {
        public MhclPlayer()
        {
            this.MhclTeam = new HashSet<MhclTeam>();
        }
    
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string Email { get; set; }
        public string MID { get; set; }
        public string Gender { get; set; }
        public string Batsman { get; set; }
        public int BatsmanRating { get; set; }
        public string Bowler { get; set; }
        public int BowlerRating { get; set; }
        public string AvailabilityComments { get; set; }
        public string OtherComments { get; set; }
        public Nullable<int> PurchasePrice { get; set; }
    
        public virtual ICollection<MhclTeam> MhclTeam { get; set; }
    }
}
