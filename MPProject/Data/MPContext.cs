using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPProject.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MPProject.Data
{
    public class MPContext : DbContext
    {
        //public MPContext()
        //{
        //}

        public MPContext(DbContextOptions<MPContext> options) : base(options) { }

        //public MPContext()
        //{
        //}
        //public MPContext() : base("name=MPContext")
        //{
        //    this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        //}

        public DbSet<MPProject.Models.User> UserModel { get; set; }
        public DbSet<MPProject.Models.ActivityLog> ActivityLogsModel { get; set; }
        public DbSet<MPProject.Models.ActivityCategory> ActivityCategory { get; set; }
        public DbSet<MPProject.Models.ActivityType> ActivityType { get; set; } 
        public DbSet<MPProject.Models.TrainingInstance> TrainingInstance { get; set; }
        public DbSet<MPProject.Models.TrainingScene> TrainingScene { get; set; }
        public DbSet<MPProject.Models.TrainingScenario> TrainingScenario { get; set; }
        public DbSet<MPProject.Models.TrainingSceneStage> TrainingSceneStage { get; set; }
        public DbSet<MPProject.Models.Patient> Patients { get; set; }
        public DbSet<MPProject.Models.PatientMedicalData> PatientMedicalDatas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<ActivityLog>().ToTable("ActivityLog");
            modelBuilder.Entity<ActivityCategory>().ToTable("ActivityCategory");
            modelBuilder.Entity<ActivityType>().ToTable("ActivityType");
            modelBuilder.Entity<TrainingInstance>().ToTable("TrainingInstance");
            modelBuilder.Entity<TrainingScene>().ToTable("TrainingScene");
            modelBuilder.Entity<TrainingScenario>().ToTable("TrainingScenario");
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<PatientMedicalData>().ToTable("PatientMedicalData");
            modelBuilder.Entity<Drug>().ToTable("Drug");

        }
        public DbSet<MPProject.Models.Drug> Drug { get; set; }
        //this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        //this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        

    }
}
