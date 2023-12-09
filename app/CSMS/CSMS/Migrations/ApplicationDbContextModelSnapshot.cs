﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CSMS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CSMS.Models.ContractModel", b =>
                {
                    b.Property<Guid>("ContractId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContractCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ContractName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.HasKey("ContractId");

                    b.ToTable("Contracts", (string)null);
                });

            modelBuilder.Entity("CSMS.Models.CustomerModel", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("character varying(35)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("character varying(35)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("CSMS.Models.TaskModel", b =>
                {
                    b.Property<Guid>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Contents")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("TaskId");

                    b.ToTable("Task", (string)null);
                });

            modelBuilder.Entity("CSMS.Models.ContractModel", b =>
                {
                    b.OwnsOne("CSMS.Models.ValueObject.AmountExcludingTax", "Money", b1 =>
                        {
                            b1.Property<Guid>("ContractModelContractId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("Money");

                            b1.HasKey("ContractModelContractId");

                            b1.ToTable("Contracts");

                            b1.WithOwner()
                                .HasForeignKey("ContractModelContractId");
                        });

                    b.OwnsOne("CSMS.Models.ValueObject.TaxRate", "TaxRate", b1 =>
                        {
                            b1.Property<Guid>("ContractModelContractId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric")
                                .HasColumnName("TaxRate");

                            b1.HasKey("ContractModelContractId");

                            b1.ToTable("Contracts");

                            b1.WithOwner()
                                .HasForeignKey("ContractModelContractId");
                        });

                    b.Navigation("Money")
                        .IsRequired();

                    b.Navigation("TaxRate")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
