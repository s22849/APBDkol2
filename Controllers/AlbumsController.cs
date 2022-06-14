using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APBDkol2.Services;
using APBDkol2.Models.DTOs;
using Microsoft.EntityFrameworkCore;
#pragma warning restore format

namespace APBDkol2.Controllers
{
    [Route("[controller]")]
    public class AlbumsController : Controller
    {
         public IDbService _service;

       public AlbumsController(IDbService service){
        _service=service;
       }

       [HttpGet("{idAlbum}")]
       public async Task<IActionResult> GetTracksFromAlbum(int idAlbum){

            if(!await _service.DoesAlbumExist(idAlbum)){
                return NotFound("Brak albumu");
            }

            return Ok(await _service.GetAllTracks(idAlbum).ToListAsync());
       }
    }
}