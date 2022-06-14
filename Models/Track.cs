using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDkol2.Models
{
    public class Track
    {
        public int IdTrack {get; set;}
        public string TrackName {get; set;}
        public double Duration {get;set;}
        public int IdMusicAlbum {get; set;}
        public Album Album {get;set;}
        public ICollection<Musician_Track> Musician_Tracks {get;set;}
    }
}