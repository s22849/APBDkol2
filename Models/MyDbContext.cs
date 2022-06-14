using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace APBDkol2.Models
{
    public class MyDbContext : DbContext
    {

        public DbSet<Album> Albums {get; set;}
        public DbSet<Musician> Musicians {get; set;}
        public DbSet<Musician_Track> Musician_Tracks {get; set;}
        public DbSet<Track> Tracks {get; set;}
        public DbSet<MusicLabel> MusicLabels {get; set;}


        // public DbSet<Klient> Klient {get; set;}
        // public DbSet<Pracownik> Pracownik {get; set;}
        // public DbSet<Zamowienie> Zamowienie {get; set;}
        // public DbSet<WyrobCukierniczy> WyrobCukierniczy {get; set;}
        // public DbSet<Zamowienie_WyrobCukierniczy> Zamowienie_WyrobCukierniczu {get; set;}
         public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Album>(e=>{
                e.ToTable("Album");
                e.HasKey(e=>e.IdAlbum);
                e.Property(e=>e.AlbumName).HasMaxLength(30).IsRequired();
                e.Property(e=>e.PublishDate).IsRequired();

                e.HasOne(e=>e.MusicLabel).WithMany(e=>e.Albums).HasForeignKey(e=>e.IdMusicLabel).OnDelete(DeleteBehavior.ClientSetNull);

                // e.HasOne(e => e.Track).WithMany(e => e.Album).HasForeignKey(e => e.IdMusicAlbum).OnDelete(DeleteBehavior.ClientSetNull);


                e.HasData(
                    new Album {
                        IdAlbum=1,
                        AlbumName="abc",
                        PublishDate=new System.DateTime(2002,7,5),
                        IdMusicLabel=1
                    }
                );
            });

            modelBuilder.Entity<Musician>(e=>{
                e.ToTable("Musician");
                e.HasKey(e=>e.IdMusician);
                e.Property(e=>e.FirstName).HasMaxLength(30).IsRequired();
                e.Property(e=>e.LastName   ).HasMaxLength(50).IsRequired();
                e.Property(e=>e.Nickname).HasMaxLength(20);


                e.HasData(
                    new Musician {
                        IdMusician=1,
                        FirstName="John",
                        LastName="Kowalski",
                        Nickname=""
                    }
                );
            });

             modelBuilder.Entity<Musician_Track>(e=>{
                e.ToTable("Musician_Track");
                e.HasKey(e=>new {e.IdMusician,e.IdTrack});

                e.HasOne(e=>e.Musician).WithMany(e=>e.Musician_Tracks).HasForeignKey(e=>e.IdMusician).OnDelete(DeleteBehavior.ClientSetNull);
                e.HasOne(e=>e.Track).WithMany(e=>e.Musician_Tracks).HasForeignKey(e=>e.IdTrack).OnDelete(DeleteBehavior.ClientSetNull);

                e.HasData(
                   new Musician_Track{
                    IdMusician=1,
                    IdTrack=1
                   }
                );
            });

            modelBuilder.Entity<Track>(e=>{
                e.ToTable("Track");
                e.HasKey(e=>e.IdTrack);
                e.Property(e=>e.TrackName).HasMaxLength(20).IsRequired();
                e.Property(e=>e.Duration   ).IsRequired();

                e.HasOne(e=>e.Album).WithMany(e=>e.Tracks).HasForeignKey(e=>e.IdMusicAlbum).OnDelete(DeleteBehavior.ClientSetNull);

                e.HasData(
                    new Track {
                        IdTrack=1,
                        TrackName="abc",
                        Duration=3.5,
                        IdMusicAlbum=1
                    }
                );
            });

                modelBuilder.Entity<MusicLabel>(e=>{
                e.ToTable("MusicLabel");
                e.HasKey(e=>e.IdMusicLabel);
                e.Property(e=>e.Name).HasMaxLength(50).IsRequired();

                e.HasData(
                    new MusicLabel {
                        IdMusicLabel=1,
                        Name="xyz"
                    }
                );
            });

        //     modelBuilder.Entity<Pracownik>(e=> {
        //         e.ToTable("Pracownik");
        //         e.HasKey(e=>e.IdPracownik);

        //         e.Property(e=>e.Imie).HasMaxLength(100).IsRequired();
        //         e.Property(e=>e.Nazwisko).HasMaxLength(100).IsRequired();

        //         e.HasData(
        //         new Pracownik{
        //             IdPracownik=1,
        //             Imie="Jan",
        //             Nazwisko="Jankowski"
        //         }
        //     );
        //     });
        //     modelBuilder.Entity<Zamowienie>(e=> {
        //         e.ToTable("Zamowienie");
        //         e.HasKey(e=>e.IdZamowienie);

        //         e.Property(e=>e.DataPrzyjecia).IsRequired();
        //         e.Property(e=>e.Uwagi).HasMaxLength(100);

        //         e.HasOne(e=>e.Klient).WithMany(e=>e.Zamowienia).HasForeignKey(e=>e.IdKlient).OnDelete(DeleteBehavior.ClientSetNull);
        //         e.HasOne(e=>e.Pracownik).WithMany(e=>e.Zamowienia).HasForeignKey(e=>e.IdPracownik).OnDelete(DeleteBehavior.ClientSetNull);

        //         e.HasData(
        //             new Zamowienie{
        //                 IdZamowienie=1,
        //                 IdKlient=1,
        //                 IdPracownik=1,
        //                 DataPrzyjecia=new System.DateTime(2002,7,5)
        //             }
        //         );
        //     });

        //     modelBuilder.Entity<WyrobCukierniczy>(e=>{
        //         e.ToTable("WyrobCukierniczy");
        //         e.HasKey(e=>e.IdWyrobuCukierniczego);

        //         e.Property(e=>e.Nazwa).HasMaxLength(200).IsRequired();
        //         e.Property(e=>e.CenaZaSzt).IsRequired();
        //         e.Property(e=>e.Typ).HasMaxLength(40).IsRequired();

        //         e.HasData(
        //             new WyrobCukierniczy{
        //                 IdWyrobuCukierniczego = 1,
        //                 Nazwa = "Magdalenek",
        //                 CenaZaSzt = 1.50,
        //                 Typ = "Cukierek"
        //             },
        //             new WyrobCukierniczy
        //             {
        //                 IdWyrobuCukierniczego = 2,
        //                 Nazwa = "Tofik",
        //                 CenaZaSzt = 0.99,
        //                 Typ = "Cukierek"
        //             }
                    
        //         );
        //     });

        //     modelBuilder.Entity<Zamowienie_WyrobCukierniczy>(e=>{

        //         e.ToTable("Zamowienie_WyrobCukierniczy");
        //         e.HasKey(e=>new {e.IdWyrobuCukierniczego, e.IdZamowienie});

        //         e.Property(e=>e.Ilosc).IsRequired();
        //         e.Property(e=>e.Uwagi).HasMaxLength(200);

        //         e.HasOne(e=>e.WyrobCukierniczy).WithMany(e=>e.Zamowienie_WyrobyCukiernicze).HasForeignKey(e=>e.IdWyrobuCukierniczego).OnDelete(DeleteBehavior.ClientSetNull);
        //         e.HasOne(e=>e.Zamowienie).WithMany(e=>e.Zamowienie_WyrobCukierniczy).HasForeignKey(e=>e.IdZamowienie).OnDelete(DeleteBehavior.ClientSetNull);


        //             e.HasData(
        //             new Zamowienie_WyrobCukierniczy
        //             {
        //                 IdZamowienie = 1,
        //                 IdWyrobuCukierniczego = 1,
        //                 Ilosc = 10
        //             },
        //             new Zamowienie_WyrobCukierniczy
        //             {
        //                 IdZamowienie = 1,
        //                 IdWyrobuCukierniczego = 2,
        //                 Ilosc = 20
        //             }
        //         );
        //     });
        }
    }
}