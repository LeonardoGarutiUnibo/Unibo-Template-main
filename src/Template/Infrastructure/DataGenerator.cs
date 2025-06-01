using Template.Services.Shared;
using System;
using System.Linq;
using Template.Services;

namespace Template.Infrastructure
{
    public class DataGenerator
    {
        public static void InitializeUsers(TemplateDbContext context)
        {
            if (context.Users.Any())
            {
                return;   // Data was already seeded
            }

            context.Users.AddRange(
                new User
                {
                    Id = Guid.Parse("a89cfb3e-cac6-4721-b51c-3a97ecf4695b"), // Forced to specific Guid for tests
                    Email = "email1@test.it",
                    Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", // SHA-256 of text "Prova"
                    FirstName = "Nome1",
                    LastName = "Cognome1",
                    NickName = "Nickname1",
                    Role = "Admin",
                    
                },
                new User
                {
                    Id = Guid.Parse("f62487cd-b3e8-4fa7-a2cb-6f879d1c9a99"), // Forced to specific Guid for tests
                    Email = "email2@test.it",
                    Password = "Uy6qvZV0iA2/drm4zACDLCCm7BE9aCKZVQ16bg80XiU=", // SHA-256 of text "Test"
                    FirstName = "Nome2",
                    LastName = "Cognome2",
                    NickName = "Nickname2",
                    Role = "Admin",
                    
                },
                new User
                {
                    Id = Guid.Parse("e9130fb8-39e7-4c44-a33f-9e0cc4d144c5"), // Forced to specific Guid for tests
                    Email = "email3@test.it",
                    Password = "Uy6qvZV0iA2/drm4zACDLCCm7BE9aCKZVQ16bg80XiU=", // SHA-256 of text "Test"
                    FirstName = "Nome3",
                    LastName = "Cognome3",
                    NickName = "Nickname3",
                    Role =  "User",
                    

                });

            context.SaveChanges();
        }
        
        public static void InitializeTeams(TemplateDbContext context){
            if (context.Teams.Any())
            {
                return;   // Data was already seeded
            }
            
            
            context.Teams.AddRange(
                new Team
                {
                    Id = Guid.Parse("c2e4b7f0-1ad3-4536-82e9-61e8e3bdc91f"), // Forced to specific Guid for tests
                    Name = "Team A",
                },
                new Team
                {
                    Id = Guid.Parse("d3a2e9b4-7b6f-4d13-a3ef-fd2b3dc802bc"), // Forced to specific Guid for tests
                    Name = "Team B",
                });
            context.SaveChanges();
            
        }

        public static void InitializeTeamMembers(TemplateDbContext context){
            if (context.TeamMembers.Any())
            {
                return;   // Data was already seeded
            }
                   
            context.TeamMembers.AddRange(
            );
        }

        public static void InitializeTimesheets(TemplateDbContext context){
            if (context.Timesheets.Any())
            {
                return;   // Data was already seeded
            }
            
            context.Timesheets.AddRange(
                new Timesheet
                {
                    Id = Guid.Parse("b7f5e3c8-4e2b-4703-9879-9b1f60e5c2a7"), // Forced to specific Guid for tests
                    Name = "Timesheet A",
                    WeekDay = "0-1-2-3-4"
                },
                new Timesheet
                {
                    Id = Guid.Parse("a1c1f8a2-7c1e-4d3c-9d4f-21b0a5fd30de"), // Forced to specific Guid for tests
                    Name = "Timesheet B",
                    WeekDay = "0-2-4"
                });
            context.SaveChanges();
            
        }

        public static void InitializeAbsenceEvents(TemplateDbContext context){
            if (context.AbsenceEvents.Any())
            {
                return;   // Data was already seeded
            }
                   
            context.AbsenceEvents.AddRange();
            context.SaveChanges();
        }

    }
}
