using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDkol2.Models
{
    public class Album
    {
        public int IdAlbum {get; set;}
        public string AlbumName {get; set;}
        public DateTime PublishDate {get; set;}
        public int IdMusicLabel {get; set;}
        public ICollection<Track> Tracks {get;set;}
        public MusicLabel MusicLabel {get; set;}
    }
}