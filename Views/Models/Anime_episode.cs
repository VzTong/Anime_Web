//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Anime_Web.Views.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Anime_episode
    {
        public int id { get; set; }
        public int Anime_id_episode { get; set; }
        public int Anime_ep { get; set; }
        public string Anime_ep_vid { get; set; }
    
        public virtual Anime Anime { get; set; }
    }
}
