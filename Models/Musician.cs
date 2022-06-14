using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDkol2.Models
{
    public class Musician
    {
        public int IdMusician {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Nickname{get; set;}
        public ICollection<Musician_Track> Musician_Tracks{get;set;}


    }
}