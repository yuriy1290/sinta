using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiSocialNetwork.Models
{
    public partial class SocialNetworkContext : DbContext
    {
        public SocialNetworkContext()
        {
        }

        public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<Correspondence> Correspondences { get; set; } = null!;
        public virtual DbSet<Friend> Friends { get; set; } = null!;
        public virtual DbSet<MessageUser> MessageUsers { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<NewsLikesInfo> NewsLikesInfos { get; set; } = null!;
        public virtual DbSet<NewsPuctire> NewsPuctires { get; set; } = null!;
        public virtual DbSet<Picture> Pictures { get; set; } = null!;
        public virtual DbSet<Progress> Progresses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Support> Supports { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAchievement> UserAchievements { get; set; } = null!;
        public virtual DbSet<UserFriend> UserFriends { get; set; } = null!;
        public virtual DbSet<UserPhoto> UserPhotos { get; set; } = null!;
        public virtual DbSet<UserPuctire> UserPuctires { get; set; } = null!;
        public virtual DbSet<UserRolesInfo> UserRolesInfos { get; set; } = null!;
        public DbSet<Like> Likes { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }
        public DbSet<FcmToken> FcmTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-SQB4B00\\DELUR;Initial Catalog=SocialNetwork;Persist Security Info=True;User ID=sa;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.IdApplications);

                entity.Property(e => e.IdApplications).HasColumnName("ID_Applications");

                entity.Property(e => e.RecipientId).HasColumnName("Recipient_ID");

                entity.Property(e => e.SenderId).HasColumnName("Sender_ID");
            });

            modelBuilder.Entity<Correspondence>(entity =>
            {
                entity.HasKey(e => e.IdCorrespondence);

                entity.ToTable("Correspondence");

                entity.Property(e => e.IdCorrespondence).HasColumnName("ID_Correspondence");

                entity.Property(e => e.MessageId).HasColumnName("Message_ID");

                entity.Property(e => e.SenderId).HasColumnName("Sender_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => e.IdFriends);

                entity.Property(e => e.IdFriends).HasColumnName("ID_Friends");

                entity.Property(e => e.FriendsId).HasColumnName("Friends_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

         
            });

            modelBuilder.Entity<MessageUser>(entity =>
            {
                entity.HasKey(e => e.IdMessage)
                    .HasName("PK_Message");

                entity.ToTable("Message_User");

                entity.Property(e => e.IdMessage).HasColumnName("ID_Message");

                entity.Property(e => e.SenderId).HasColumnName("Sender_ID");

                entity.Property(e => e.SendingTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Sending_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TextMessage)
                    .IsUnicode(false)
                    .HasColumnName("Text_Message");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

             
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.IdNews);

                entity.Property(e => e.IdNews).HasColumnName("ID_News");

                entity.Property(e => e.DescriptionNews)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Description_News");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.PictureId).HasColumnName("Picture_ID");

                entity.Property(e => e.SendingTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Sending_time")
                    .HasDefaultValueSql("(getdate())");

            });

            modelBuilder.Entity<Like>()
           .HasKey(l => new { l.NewsId, l.UserId });

            modelBuilder.Entity<Dislike>()
                .HasKey(d => new { d.NewsId, d.UserId });

            modelBuilder.Entity<NewsLikesInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("NewsLikesInfo");

                entity.Property(e => e.DescriptionNews)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Description_News");

                entity.Property(e => e.IdNews).HasColumnName("ID_News");

                entity.Property(e => e.UserName).HasMaxLength(101);
            });

            modelBuilder.Entity<NewsPuctire>(entity =>
            {
                entity.HasKey(e => e.IdNewsPuctires);

                entity.ToTable("News_puctires");

                entity.Property(e => e.IdNewsPuctires).HasColumnName("ID_News_puctires");

                entity.Property(e => e.NewsId).HasColumnName("News_ID");

                entity.Property(e => e.PictureId).HasColumnName("Picture_ID");

            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.HasKey(e => e.IdPicture);

                entity.ToTable("Picture");

                entity.Property(e => e.IdPicture).HasColumnName("ID_Picture");

                entity.Property(e => e.UploadDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Progress>(entity =>
            {
                entity.HasKey(e => e.IdProgress);

                entity.ToTable("Progress");

                entity.Property(e => e.IdProgress).HasColumnName("ID_Progress");

                entity.Property(e => e.DescriptionProgress)
                    .HasMaxLength(100)
                    .HasColumnName("Description_Progress");

                entity.Property(e => e.IdPicture).HasColumnName("ID_Picture");

                entity.Property(e => e.NameProgress)
                    .HasMaxLength(50)
                    .HasColumnName("Name_Progress");

            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role");

                entity.HasIndex(e => e.NameRole, "UQ_Name_Role")
                    .IsUnique();

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Role");
            });

            modelBuilder.Entity<Support>(entity =>
            {
                entity.HasKey(e => e.IdSupports);

                entity.Property(e => e.IdSupports).HasColumnName("ID_Supports");

                entity.Property(e => e.MessageId).HasColumnName("Message_ID");

                entity.Property(e => e.SpecialistId).HasColumnName("Specialist_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

             
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.Token1)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.TokenDatetime)
                    .HasColumnName("token_datetime")
                    .HasDefaultValueSql("(sysdatetime())");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK_User");

                entity.HasIndex(e => e.LoginUser, "UQ_Login_User")
                    .IsUnique();

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("First_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("Last_name");

                entity.Property(e => e.LoginUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Login_User");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .HasColumnName("Middle_name");

                entity.Property(e => e.NumberOfMessages).HasColumnName("Number_of_messages");

                entity.Property(e => e.PasswordUser)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Password_User");

                entity.Property(e => e.PhotoId).HasColumnName("Photo_ID");

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.Salt)
                    .HasMaxLength(256)
                    .IsUnicode(false);
                entity.Property(e => e.FcmToken).HasMaxLength(255); // Укажите максимальную длину токена

                entity.Property(e => e.TimeInTheApp).HasColumnName("Time_in_the_app");

            });

            modelBuilder.Entity<FcmToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Token).IsRequired();

                // Добавьте внешний ключ для связи с таблицей Users
                entity.HasOne(e => e.User)
                    .WithMany(user => user.FcmTokens)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade); // При удалении пользователя удалять все его токены
            });


            modelBuilder.Entity<UserAchievement>(entity =>
            {
                entity.HasKey(e => e.IdUserAchievements);

                entity.ToTable("User_achievements");

                entity.Property(e => e.IdUserAchievements).HasColumnName("ID_User_achievements");

                entity.Property(e => e.ProgressId).HasColumnName("Progress_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.Progress)
                    .WithMany(p => p.UserAchievements)
                    .HasForeignKey(d => d.ProgressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Progress_User_achievements");

            });

            modelBuilder.Entity<UserFriend>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("UserFriends");

                entity.Property(e => e.FriendFullName).HasMaxLength(101);

                entity.Property(e => e.UserFullName).HasMaxLength(101);
            });

            modelBuilder.Entity<UserPhoto>(entity =>
            {
                entity.HasKey(e => e.IdPhoto);

                entity.Property(e => e.IdPhoto).HasColumnName("ID_Photo");

                entity.Property(e => e.UploadDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

            });

            modelBuilder.Entity<UserPuctire>(entity =>
            {
                entity.HasKey(e => e.PhotoId)
                    .HasName("PK_UserPhoto");

                entity.ToTable("UserPuctire");

                entity.Property(e => e.PhotoId).HasColumnName("Photo_ID");

                entity.Property(e => e.UploadDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

            });

            modelBuilder.Entity<UserRolesInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("UserRolesInfo");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("First_name");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("Last_name");

                entity.Property(e => e.LoginUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Login_User");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
