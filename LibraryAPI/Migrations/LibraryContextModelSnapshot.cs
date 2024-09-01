﻿// <auto-generated />
using System;
using LibraryAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryAPI.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LibraryAPI.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "United Kingdom",
                            DateOfBirth = new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "George",
                            LastName = "Orwell"
                        },
                        new
                        {
                            Id = 2,
                            Country = "United Kingdom",
                            DateOfBirth = new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "J.K.",
                            LastName = "Rowling"
                        },
                        new
                        {
                            Id = 3,
                            Country = "United States",
                            DateOfBirth = new DateTime(1926, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Harper",
                            LastName = "Lee"
                        });
                });

            modelBuilder.Entity("LibraryAPI.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ReturnBy")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TakenAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Description = "A novel about totalitarianism.",
                            Genre = "Dystopian",
                            ISBN = "9780451524935",
                            ImagePath = "path/to/image1",
                            Title = "1984"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            Description = "A novel about a farm.",
                            Genre = "Political satire",
                            ISBN = "9780451526342",
                            ImagePath = "path/to/image2",
                            Title = "Animal Farm"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 2,
                            Description = "A novel about a young wizard.",
                            Genre = "Fantasy",
                            ISBN = "9780747532699",
                            ImagePath = "path/to/image3",
                            Title = "Harry Potter and the Philosopher's Stone"
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 3,
                            Description = "A novel about racial injustice.",
                            Genre = "Southern Gothic",
                            ISBN = "9780061120084",
                            ImagePath = "path/to/image4",
                            Title = "To Kill a Mockingbird"
                        });
                });

            modelBuilder.Entity("LibraryAPI.Models.Book", b =>
                {
                    b.HasOne("LibraryAPI.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("LibraryAPI.Models.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
