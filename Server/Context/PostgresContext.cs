using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StellarJadeManager.Shared;

namespace StellarJadeManager.Server;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Patch> Patches { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBannerInfo> UserBannerInfos { get; set; }

    public virtual DbSet<Warp> Warps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User Id=postgres.nyeujbavubnlpyjssdsa;Password=Rjnjdfcbz2002;Server=aws-0-eu-central-1.pooler.supabase.com;Port=5432;Database=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("pgsodium", "key_status", new[] { "default", "valid", "invalid", "expired" })
            .HasPostgresEnum("pgsodium", "key_type", new[] { "aead-ietf", "aead-det", "hmacsha512", "hmacsha256", "auth", "shorthash", "generichash", "kdf", "secretbox", "secretstream", "stream_xchacha20" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "pgjwt")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("graphql", "pg_graphql")
            .HasPostgresExtension("pgsodium", "pgsodium")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("banner_pkey");

            entity.ToTable("banner");

            entity.HasIndex(e => e.Id, "banner_id_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CharacterId).HasColumnName("character_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.PatchId).HasColumnName("patch_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_date");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Patch).WithMany(p => p.Banners)
                .HasForeignKey(d => d.PatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("banner_patch_id_fkey");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("event_pkey");

            entity.ToTable("event");

            entity.HasIndex(e => e.Id, "event_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.PatchId).HasColumnName("patch_id");
            entity.Property(e => e.ReleaseDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("release_date");
            entity.Property(e => e.StellarJadesAmount).HasColumnName("stellar_jades_amount");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");

            entity.HasOne(d => d.Patch).WithMany(p => p.Events)
                .HasForeignKey(d => d.PatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("event_patch_id_fkey");
        });

        modelBuilder.Entity<Patch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patch_pkey");

            entity.ToTable("patch");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.ReleaseDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("release_date");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
            entity.Property(e => e.Version)
                .HasColumnType("character varying")
                .HasColumnName("version");
            entity.Property(e => e.WeeksCount).HasColumnName("weeks_count");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("profile_pkey");

            entity.ToTable("profile");

            entity.HasIndex(e => e.Id, "profile_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CurrentJades).HasColumnName("current_jades");
            entity.Property(e => e.MoCStars).HasColumnName("MoC_stars");
            entity.Property(e => e.PfStars).HasColumnName("PF_stars");
            entity.Property(e => e.ProfileName)
                .HasColumnType("character varying")
                .HasColumnName("profile_name");
            entity.Property(e => e.SelectedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("selected_date");
            entity.Property(e => e.SupplyPass).HasColumnName("supply_pass");
            entity.Property(e => e.Uid)
                .HasColumnType("character varying")
                .HasColumnName("uid");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profile_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "user_email_key").IsUnique();

            entity.HasIndex(e => e.Id, "user_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Hash)
                .HasColumnType("character varying")
                .HasColumnName("hash");
            entity.Property(e => e.LastActive)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_active");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Salt)
                .HasColumnType("character varying")
                .HasColumnName("salt");
        });

        modelBuilder.Entity<UserBannerInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_banner_info_pkey");

            entity.ToTable("user_banner_info");

            entity.HasIndex(e => new { e.Uid, e.BannerTypeId }, "unique_uid_banner_id").IsUnique();

            entity.HasIndex(e => e.Id, "user_banner_info_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BannerTypeId)
                .HasDefaultValueSql("1")
                .HasColumnName("banner_type_id");
            entity.Property(e => e.CurrentEpicPity).HasColumnName("current_epic_pity");
            entity.Property(e => e.CurrentLegendaryPity).HasColumnName("current_legendary_pity");
            entity.Property(e => e.GuaranteedEpic).HasColumnName("guaranteed_epic");
            entity.Property(e => e.GuaranteedLegendary).HasColumnName("guaranteed_legendary");
            entity.Property(e => e.ProfileId).HasColumnName("profile_id");
            entity.Property(e => e.Uid)
                .HasColumnType("character varying")
                .HasColumnName("uid");

            entity.HasOne(d => d.Profile).WithMany(p => p.UserBannerInfos)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_banner_info_profile_id_fkey");
        });

        modelBuilder.Entity<Warp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("warp_pkey");

            entity.ToTable("warp");

            entity.HasIndex(e => e.Id, "warp_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("character varying")
                .HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.GachaId).HasColumnName("gacha_id");
            entity.Property(e => e.GachaType).HasColumnName("gacha_type");
            entity.Property(e => e.IsGuaranteed).HasColumnName("is_guaranteed");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ItemType)
                .HasColumnType("character varying")
                .HasColumnName("item_type");
            entity.Property(e => e.Lang)
                .HasColumnType("character varying")
                .HasColumnName("lang");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.RankType).HasColumnName("rank_type");
            entity.Property(e => e.Time)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("time");
            entity.Property(e => e.Uid)
                .HasColumnType("character varying")
                .HasColumnName("uid");

            entity.HasOne(d => d.Gacha).WithMany(p => p.Warps)
                .HasForeignKey(d => d.GachaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warp_gacha_id_fkey");

            entity.HasOne(d => d.UserBannerInfo).WithMany(p => p.Warps)
                .HasPrincipalKey(p => new { p.Uid, p.BannerTypeId })
                .HasForeignKey(d => new { d.Uid, d.GachaType })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warp_uid_gacha_type_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
