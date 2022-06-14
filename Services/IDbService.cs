using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBDkol2.Models;
// using APBDkol2.Models.DTOs;

namespace APBDkol2.Services
{
    public interface IDbService
    {

        public IQueryable<Models.DTOs.Album> GetAllTracks(int id);
        public Task<bool> DoesAlbumExist(int id);
        public IQueryable<Musician> GetMusician(int idMusician);
        // public IQueryable<Track> GetTracks();
        public IQueryable<Album> GetAlbums();
        public Task<bool> IsTrackOnAlbum(int idTrack, int idAlbum);
        public Task<bool> DoesMusicianWriteTrack(int idMusician, int idTrack);
        public Task DeleteMusician(int id);

    }
}