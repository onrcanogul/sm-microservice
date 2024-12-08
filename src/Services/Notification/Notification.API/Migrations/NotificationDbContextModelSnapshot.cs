﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notification.API.Contexts;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Notification.API.Migrations
{
    [DbContext(typeof(NotificationDbContext))]
    partial class NotificationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.2.24474.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Notification.API.Models.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("NotificationTypeId")
                        .HasColumnType("integer");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("NotificationTypeId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Notification.API.Models.NotificationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NotificationType")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("NotificationTypes");

                    b.HasDiscriminator<string>("NotificationType").HasValue("NotificationType");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Notification.API.Models.FriendshipRequestNotification", b =>
                {
                    b.HasBaseType("Notification.API.Models.NotificationType");

                    b.HasDiscriminator().HasValue("FriendshipRequest");
                });

            modelBuilder.Entity("Notification.API.Models.LikedPostNotification", b =>
                {
                    b.HasBaseType("Notification.API.Models.NotificationType");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.HasDiscriminator().HasValue("LikedPost");
                });

            modelBuilder.Entity("Notification.API.Models.SentCommentNotification", b =>
                {
                    b.HasBaseType("Notification.API.Models.NotificationType");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CommentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.ToTable("NotificationTypes", t =>
                        {
                            t.Property("PostId")
                                .HasColumnName("SentCommentNotification_PostId");
                        });

                    b.HasDiscriminator().HasValue("SentComment");
                });

            modelBuilder.Entity("Notification.API.Models.Notification", b =>
                {
                    b.HasOne("Notification.API.Models.NotificationType", "NotificationType")
                        .WithMany()
                        .HasForeignKey("NotificationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NotificationType");
                });
#pragma warning restore 612, 618
        }
    }
}
