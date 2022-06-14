using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDkol2.Models.DTOs
{
    public class Album
    {
        public int IdAlbum {get;set;}
        public string AlbumName {get; set;}
        public DateTime PublishDate {get; set;}
        public int IdMusicLabel {get;set;}
        public ICollection<Tracks> Tracks {get;set;}
    }
    public class Tracks {
        public int IdTrack {get; set;}
        public string Name {get; set;}
    }
}