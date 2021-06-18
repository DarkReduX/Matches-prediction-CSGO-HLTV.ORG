using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CSGO_match_result_prediction_курсовая_практика_.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : DbContext
    {
        public DbSet<MatchInfo> MatchesInfo { get; set; }
        public DbSet<TeamInfo> TeamsInfo { get; set; }
        public DbSet<TeamMapStats> TeamMapStats { get; set; }
        public DbSet<MatchNotLoaded> MatchesNotLoaded { get; set; }
        public DbSet<MatchResult> MatchResults { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MatchInfo>()
                .HasOptional(e => e.Result)
                .WithRequired(e => e.MatchInfo);
            //modelBuilder.Entity<MatchInfo>()
            //    .HasOptional(e => e.MatchResults)
            //    .WithRequired(e => e.MatchInfoes);
            modelBuilder.Entity<MatchInfo>()
                .HasMany(e => e.TeamsInfo)
                .WithMany(e => e.MatchInfo)
                .Map(m => m.ToTable("Teams_Matches").MapLeftKey("Match_Id").MapRightKey("Team_Id"));

            //modelBuilder.Entity<MatchInfo>()
            //    .HasMany(e => e.TeamInfoes)
            //    .WithMany(e => e.MatchInfoes)
            //    .Map(m => m.ToTable("TeamInfoMatchInfoes").MapLeftKey("MatchInfo_Id").MapRightKey("TeamInfo_Id"));
            modelBuilder.Entity<TeamInfo>()
                .HasMany(e => e.PredictedIn);
            //modelBuilder.Entity<TeamInfo>()
            //    .HasMany(e => e.PredictedIn)
            //    .WithRequired(e => e.PredictedTeam)
            //    .HasForeignKey(e => e.Predicted)
            //    .WillCascadeOnDelete(false);
            modelBuilder.Entity<TeamInfo>()
                .HasMany(e => e.Stats)
                .WithOptional(e => e.TeamInfo);
            //modelBuilder.Entity<TeamInfo>()
            //    .HasMany(e => e.TeamMapStats)
            //    .WithOptional(e => e.TeamInfoes)
            //    .HasForeignKey(e => e.TeamInfo_Id);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}