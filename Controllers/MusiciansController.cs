using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APBDkol2.Services;
using Microsoft.EntityFrameworkCore;
using APBDkol2.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;



namespace APBDkol2.Controllers
{
    [Route("[controller]")]
    public class MusiciansController : Controller
    {
        public IDbService _service;

       public MusiciansController(IDbService service){
        _service=service;
       }

       [HttpDelete("{idMusician}")]
       public async Task<IActionResult> DeleteMusician(int id){

         using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

        var tracks = _service.GetMusician(id).Select(e=>e.Musician_Tracks).ToList();

        var albums = _service.GetAlbums().ToList();

            foreach(Musician_Track track in tracks){
                foreach(var album in albums)
                if( !await _service.IsTrackOnAlbum(track.IdTrack,album.IdAlbum)){
                   await _service.DeleteMusician(id);
                }
            }

            transactionScope.Complete();

       }catch(Exception){
            return Problem("Błąd serwera");
       }

            }
            return NoContent();
       }
    }
}