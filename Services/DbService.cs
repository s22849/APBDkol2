using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBDkol2.Models;
using Microsoft.EntityFrameworkCore;
// using APBDkol2.Models.DTOs;


namespace APBDkol2.Services
{
    public class DbService : IDbService
    {

        MyDbContext _context;
        public DbService (MyDbContext context){
            _context=context;
        }

        public IQueryable<Models.DTOs.Album> GetAllTracks(int id){
            
            return _context.Albums.Include(e=>e.Tracks).Where(e=>e.IdAlbum==id).Select(e=>new Models.DTOs.Album {
                IdAlbum=e.IdAlbum,
                AlbumName=e.AlbumName,
                PublishDate=e.PublishDate,
                IdMusicLabel=e.IdMusicLabel,
                Tracks = e.Tracks.Select(e=>new Models.DTOs.Tracks{
                    IdTrack=e.IdTrack,
                    Name=e.TrackName
                }).ToList()
            }).OrderBy(e=>e.PublishDate);
        }
        public async Task<bool> DoesAlbumExist(int id){

            return await _context.Albums.Where(e=>e.IdAlbum==id).AnyAsync();
        }

        public IQueryable<Musician> GetMusician(int idMusician){
            return _context.Musicians.Where(e=>e.IdMusician==idMusician).
            Include(e=>e.Musician_Tracks);

        }
        // public IQueryable<Track> GetTracks(int idMusician){
        //     return _context.Tracks.
        //     Include(e=>e.Musician_Tracks).Where(e=>e.IdMusician)
        //     Include(e=>e.Album).ThenInclude(e=>e.IdAlbum);
        // }
        public IQueryable<Album> GetAlbums(){
            return _context.Albums.Include(e=>e.Tracks).ThenInclude(e=>e.IdTrack);
        }

        public async Task<bool> IsTrackOnAlbum(int idTrack, int idAlbum){
            return await _context.Tracks.Where(e=>e.IdMusicAlbum==idAlbum && e.IdTrack==idTrack).AnyAsync();
        }
        public async Task<bool> DoesMusicianWriteTrack(int idMusician, int idTrack){
            return await _context.Musician_Tracks.Where(e=>e.IdMusician==idMusician && e.IdTrack==idTrack).AnyAsync();
        }


        public async Task DeleteMusician(int id) {
            var musician = await _context.Musicians.Where(e=>e.IdMusician==id).FirstOrDefaultAsync();
            var entry = _context.Entry(musician);
             entry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            

        }

    }
}