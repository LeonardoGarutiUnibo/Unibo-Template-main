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
                    Id = Guid.Parse("a89cfb3e-cac6-4721-b52c-3a97ecba4695"), // Forced to specific Guid for tests
                    Email = "Amministratore@test.it",
                    Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", // SHA-256 of text "Prova"
                    FirstName = "Amministratore",
                    LastName = "Amministratore",
                    NickName = "Amministratore",
                    Role = "Admin",
                    TimesheetId = Guid.Parse("b7f5e3c8-4e2b-4703-9879-9b1f60e5c2a7")
                },
                new User { Id = Guid.Parse("a89cfb3e-cac6-4721-b51c-3a97ecfa4695"), Email = "email1@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Nome1",LastName = "Cognome1",NickName = "Nickname1",Role = "Admin",TimesheetId = Guid.Parse("b7f5e3c8-4e2b-4703-9879-9b1f60e5c2a7"),TeamId = Guid.Parse("c2e4b7f0-1ad3-4536-82e9-61e8e3bdc91f")  },
                new User { Id = Guid.Parse("f62487cd-b3e8-4fa7-a2cb-6f879d1c9a99"), Email = "email2@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Nome2", LastName = "Cognome2", NickName = "Nickname2", Role = "Admin", TimesheetId = Guid.Parse("b7f5e3c8-4e2b-4703-9879-9b1f60e5c2a7"), TeamId = Guid.Parse("c2e4b7f0-1ad3-4536-82e9-61e8e3bdc91f") },
                new User { Id = Guid.Parse("e9130fb8-39e7-4c44-a33f-9e0cc4d144c5"), Email = "email3@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Nome3", LastName = "Cognome3", NickName = "Nickname3", Role =  "Admin", TimesheetId = Guid.Parse("b7f5e3c8-4e2b-4703-9879-9b1f60e5c2a7"), TeamId = Guid.Parse("c2e4b7f0-1ad3-4536-82e9-61e8e3bdc91f") },
                new User { Id = Guid.Parse("8b0e308e-59fa-4c28-9bfc-1c2878e2ffb2"), Email = "alice.rossi@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Alice", LastName = "Rossi", NickName = "Ali", Role = "Admin", TimesheetId = Guid.Parse("4e2df5a2-0aa6-405e-9865-02e3692ef0c1"), TeamId = Guid.Parse("d3a2e9b4-7b6f-4d13-a3ef-fd2b3dc802bc") },
                new User { Id = Guid.Parse("fa18d9ce-3cd9-4d55-82e9-1a19494f174b"), Email = "marco.verdi@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Marco", LastName = "Verdi", NickName = "Mark", Role = "Admin", TimesheetId = Guid.Parse("a1c1f8a2-7c1e-4d3c-9d4f-21b0a5fd30de"), TeamId = Guid.Parse("d3a2e9b4-7b6f-4d13-a3ef-fd2b3dc802bc") },
                new User { Id = Guid.Parse("02e3d368-78c5-4b3e-84dc-b8a9c5971cc5"), Email = "giulia.bianchi@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Giulia", LastName = "Bianchi", NickName = "Jules", Role = "Admin", TimesheetId = Guid.Parse("4e2df5a2-0aa6-405e-9865-02e3692ef0c1"), TeamId = Guid.Parse("d3a2e9b4-7b6f-4d13-a3ef-fd2b3dc802bc") },
                new User { Id = Guid.Parse("3f6f5f91-3ac0-44e4-bd77-72a9b24534f5"), Email = "luca.neri@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Luca", LastName = "Neri", NickName = "Luke", Role = "Admin", TimesheetId = Guid.Parse("a1c1f8a2-7c1e-4d3c-9d4f-21b0a5fd30de"), TeamId = Guid.Parse("3f8cdd68-0c3d-4d85-b4b6-b7f09e99940d") },
                new User { Id = Guid.Parse("bdfb79f3-cc8d-4f39-90ff-54f0d18e6be5"), Email = "francesca.moro@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Francesca", LastName = "Moro", NickName = "Fran", Role = "Admin", TimesheetId = Guid.Parse("b7f5e3c8-4e2b-4703-9879-9b1f60e5c2a7"), TeamId = Guid.Parse("3f8cdd68-0c3d-4d85-b4b6-b7f09e99940d") },
                new User { Id = Guid.Parse("1e743c1a-2264-4626-ae19-e097d214f6e3"), Email = "andrea.ferri@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Andrea", LastName = "Ferri", NickName = "Andy", Role = "Admin", TimesheetId = Guid.Parse("b7f5e3c8-4e2b-4703-9879-9b1f60e5c2a7"), TeamId = Guid.Parse("3f8cdd68-0c3d-4d85-b4b6-b7f09e99940d") },
                new User { Id = Guid.Parse("ef74c64d-1d1b-440e-8df2-3432b690ff5d"), Email = "martina.valli@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Martina", LastName = "Valli", NickName = "Marty", Role = "Admin", TimesheetId = Guid.Parse("4e2df5a2-0aa6-405e-9865-02e3692ef0c1"), TeamId = Guid.Parse("3f8cdd68-0c3d-4d85-b4b6-b7f09e99940d") },
                new User { Id = Guid.Parse("621ef3b7-df3d-47f6-8905-9490fd47be44"), Email = "giovanni.mazzi@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Giovanni", LastName = "Mazzi", NickName = "Gio", Role = "User", TimesheetId = Guid.Parse("b7f5e3c8-4e2b-4703-9879-9b1f60e5c2a7"), TeamId = Guid.Parse("9abf1332-7699-4a3b-a88b-dfbc59b5b034") },
                new User { Id = Guid.Parse("a67ddf3b-4be4-4c36-b62c-69bb23cb62b1"), Email = "elena.riva@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Elena", LastName = "Riva", NickName = "Ely", Role = "User", TimesheetId = Guid.Parse("b7f5e3c8-4e2b-4703-9879-9b1f60e5c2a7"), TeamId = Guid.Parse("9abf1332-7699-4a3b-a88b-dfbc59b5b034") },
                new User { Id = Guid.Parse("bd031cf4-34a2-453e-8739-d03eb6eb960c"), Email = "filippo.gallo@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Filippo", LastName = "Gallo", NickName = "Flip", Role = "User", TimesheetId = Guid.Parse("a1c1f8a2-7c1e-4d3c-9d4f-21b0a5fd30de"), TeamId = Guid.Parse("9abf1332-7699-4a3b-a88b-dfbc59b5b034") },
                new User { Id = Guid.Parse("d6a888ff-b1ff-4960-9f3b-65ed2383de61"), Email = "chiara.pelle@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Chiara", LastName = "Pelle", NickName = "Kia", Role = "User", TimesheetId = Guid.Parse("a1c1f8a2-7c1e-4d3c-9d4f-21b0a5fd30de"), TeamId = Guid.Parse("9abf1332-7699-4a3b-a88b-dfbc59b5b034") },
                new User { Id = Guid.Parse("7794622c-88ae-49fd-a20a-0dd3a46a6d47"), Email = "davide.marino@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Davide", LastName = "Marino", NickName = "Dave", Role = "User", TimesheetId = Guid.Parse("4e2df5a2-0aa6-405e-9865-02e3692ef0c1"), TeamId = Guid.Parse("9abf1332-7699-4a3b-a88b-dfbc59b5b034") },
                new User { Id = Guid.Parse("e252dbf9-f44a-4374-a180-5b1ae3d2f3f1"), Email = "ilaria.conti@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Ilaria", LastName = "Conti", NickName = "Ila", Role = "User", TimesheetId = Guid.Parse("4e2df5a2-0aa6-405e-9865-02e3692ef0c1"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") },
                new User { Id = Guid.Parse("0f51e242-c419-4ee9-9a9f-38b7e41b76f1"), Email = "paolo.bassi@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Paolo", LastName = "Bassi", NickName = "Paul", Role = "User", TimesheetId = Guid.Parse("a1c1f8a2-7c1e-4d3c-9d4f-21b0a5fd30de"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") },
                new User { Id = Guid.Parse("3a13ec30-ec8d-4de3-b053-6f07b5c8e4c4"), Email = "roberta.fontana@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Roberta", LastName = "Fontana", NickName = "Robby", Role = "User", TimesheetId = Guid.Parse("a1c1f8a2-7c1e-4d3c-9d4f-21b0a5fd30de"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") },
                new User { Id = Guid.Parse("552ac54b-dc6e-472e-8f7a-c837d65b51ff"), Email = "simone.riva@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Simone", LastName = "Riva", NickName = "Simo", Role = "User", TimesheetId = Guid.Parse("b7f5e3c8-4e2b-4703-9879-9b1f60e5c2a7"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") },
                new User { Id = Guid.Parse("a8f86f82-c17e-4edb-ae8d-ef4e70c331c4"), Email = "laura.fabbri@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Laura", LastName = "Fabbri", NickName = "Lau", Role = "User", TimesheetId = Guid.Parse("b7f5e3c8-4e2b-4703-9879-9b1f60e5c2a7"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") },
                new User { Id = Guid.Parse("b9e022db-2a56-4a3c-bf1d-185f3e6f9c77"), Email = "nicola.basili@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Nicola", LastName = "Basili", NickName = "Nick", Role = "User", TimesheetId = Guid.Parse("a1c1f8a2-7c1e-4d3c-9d4f-21b0a5fd30de"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") },
                new User { Id = Guid.Parse("7baf7af2-4d4c-4d41-acc3-8e867167f879"), Email = "stefania.lodi@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Stefania", LastName = "Lodi", NickName = "Stefy", Role = "User", TimesheetId = Guid.Parse("b7f5e3c8-4e2b-4703-9879-9b1f60e5c2a7"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") },
                new User { Id = Guid.Parse("cd93a776-f3eb-4b39-87c9-1447bb80ff91"), Email = "riccardo.mori@test.it", Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", FirstName = "Riccardo", LastName = "Mori", NickName = "Rick", Role = "User", TimesheetId = Guid.Parse("b7f5e3c8-4e2b-4703-9879-9b1f60e5c2a7"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") }
            );
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
                    Id = Guid.Parse("c2e4b7f0-1ad3-4536-82e9-61e8e3bdc91f"),
                    Name = "Direzione Generale",
                },
                new Team
                {
                    Id = Guid.Parse("d3a2e9b4-7b6f-4d13-a3ef-fd2b3dc802bc"),
                    Name = "Area Amministrazione e Finanza",
                },
                new Team
                {
                    Id = Guid.Parse("3f8cdd68-0c3d-4d85-b4b6-b7f09e99940d"),
                    Name = "Risorse Umane",
                },
                new Team
                {
                    Id = Guid.Parse("9abf1332-7699-4a3b-a88b-dfbc59b5b034"),
                    Name = "Ricerca e Sviluppo",
                },
                new Team
                {
                    Id = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1"),
                    Name = "Vendite / Commerciale",
                });
            context.SaveChanges();
            
        }

        public static void InitializeTeamMembers(TemplateDbContext context){
            if (context.TeamMembers.Any())
            {
                return;   // Data was already seeded
            }
            context.TeamMembers.AddRange(
                new TeamMember
                {
                    Id = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3a4c1d1e1"),
                    UserId = Guid.Parse("a89cfb3e-cac6-4721-b52c-3a97ecba4695"), //Amministratore
                    TeamId = Guid.Parse("c2e4b7f0-1ad3-4536-82e9-61e8e3bdc91f"), //"Direzione Generale"
                    IsManager = true
                },
                new TeamMember
                {
                    Id = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3e4c1f1e1"),
                    UserId = Guid.Parse("a89cfb3e-cac6-4721-b51c-3a97ecfa4695"), //email1@test.it
                    TeamId = Guid.Parse("d3a2e9b4-7b6f-4d13-a3ef-fd2b3dc802bc"), //"Area Amministrazione e Finanza"
                    IsManager = true
                },
                new TeamMember
                {
                    Id = Guid.Parse("61a0bca2-7632-4fe5-b6f3-19b3e4c1d1e1"),
                    UserId = Guid.Parse("8b0e308e-59fa-4c28-9bfc-1c2878e2ffb2"), //alice.rossi@test.it
                    TeamId = Guid.Parse("3f8cdd68-0c3d-4d85-b4b6-b7f09e99940d"), //"Risorse Umane"
                    IsManager = true
                },
                new TeamMember
                {
                    Id = Guid.Parse("61a0bca2-7632-4fe5-b7f3-19b3e4c1d1e2"),
                    UserId = Guid.Parse("fa18d9ce-3cd9-4d55-82e9-1a19494f174b"), //marco.verdi@test.it
                    TeamId = Guid.Parse("9abf1332-7699-4a3b-a88b-dfbc59b5b034"), //"Ricerca e Sviluppo"
                    IsManager = true
                },
                new TeamMember
                {
                    Id = Guid.Parse("61a0bca2-7632-4fe5-b6f3-19b3e4c1d1e2"),
                    UserId = Guid.Parse("3f6f5f91-3ac0-44e4-bd77-72a9b24534f5"), //luca.neri@test.it
                    TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1"), //"Vendite / Commerciale"
                    IsManager = true
                },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("a89cfb3e-cac6-4721-b51c-3a97ecfa4695"), TeamId = Guid.Parse("c2e4b7f0-1ad3-4536-82e9-61e8e3bdc91f") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("f62487cd-b3e8-4fa7-a2cb-6f879d1c9a99"), TeamId = Guid.Parse("c2e4b7f0-1ad3-4536-82e9-61e8e3bdc91f") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("e9130fb8-39e7-4c44-a33f-9e0cc4d144c5"), TeamId = Guid.Parse("c2e4b7f0-1ad3-4536-82e9-61e8e3bdc91f") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("8b0e308e-59fa-4c28-9bfc-1c2878e2ffb2"), TeamId = Guid.Parse("d3a2e9b4-7b6f-4d13-a3ef-fd2b3dc802bc") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("fa18d9ce-3cd9-4d55-82e9-1a19494f174b"), TeamId = Guid.Parse("d3a2e9b4-7b6f-4d13-a3ef-fd2b3dc802bc") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("02e3d368-78c5-4b3e-84dc-b8a9c5971cc5"), TeamId = Guid.Parse("d3a2e9b4-7b6f-4d13-a3ef-fd2b3dc802bc") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("3f6f5f91-3ac0-44e4-bd77-72a9b24534f5"), TeamId = Guid.Parse("3f8cdd68-0c3d-4d85-b4b6-b7f09e99940d") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("bdfb79f3-cc8d-4f39-90ff-54f0d18e6be5"), TeamId = Guid.Parse("3f8cdd68-0c3d-4d85-b4b6-b7f09e99940d") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("1e743c1a-2264-4626-ae19-e097d214f6e3"), TeamId = Guid.Parse("3f8cdd68-0c3d-4d85-b4b6-b7f09e99940d") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("ef74c64d-1d1b-440e-8df2-3432b690ff5d"), TeamId = Guid.Parse("3f8cdd68-0c3d-4d85-b4b6-b7f09e99940d") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("621ef3b7-df3d-47f6-8905-9490fd47be44"), TeamId = Guid.Parse("9abf1332-7699-4a3b-a88b-dfbc59b5b034") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("a67ddf3b-4be4-4c36-b62c-69bb23cb62b1"), TeamId = Guid.Parse("9abf1332-7699-4a3b-a88b-dfbc59b5b034") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("bd031cf4-34a2-453e-8739-d03eb6eb960c"), TeamId = Guid.Parse("9abf1332-7699-4a3b-a88b-dfbc59b5b034") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("d6a888ff-b1ff-4960-9f3b-65ed2383de61"), TeamId = Guid.Parse("9abf1332-7699-4a3b-a88b-dfbc59b5b034") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("7794622c-88ae-49fd-a20a-0dd3a46a6d47"), TeamId = Guid.Parse("9abf1332-7699-4a3b-a88b-dfbc59b5b034") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("e252dbf9-f44a-4374-a180-5b1ae3d2f3f1"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("0f51e242-c419-4ee9-9a9f-38b7e41b76f1"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("3a13ec30-ec8d-4de3-b053-6f07b5c8e4c4"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("552ac54b-dc6e-472e-8f7a-c837d65b51ff"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("a8f86f82-c17e-4edb-ae8d-ef4e70c331c4"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("b9e022db-2a56-4a3c-bf1d-185f3e6f9c77"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("7baf7af2-4d4c-4d41-acc3-8e867167f879"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") },
                new TeamMember { Id = Guid.NewGuid(), UserId = Guid.Parse("cd93a776-f3eb-4b39-87c9-1447bb80ff91"), TeamId = Guid.Parse("61a0bca2-7632-4fe4-b6f3-19b3f4c1d1e1") }
            );   
            context.TeamMembers.AddRange(
            );
            context.SaveChanges();
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
                    Name = "Full Time",
                    StartTime = TimeSpan.Parse("09:00:00"),
                    EndTime = TimeSpan.Parse("18:00:00"),
                    WeekDay = "0-1-2-3-4"
                },
                new Timesheet
                {
                    Id = Guid.Parse("a1c1f8a2-7c1e-4d3c-9d4f-21b0a5fd30de"), // Forced to specific Guid for tests
                    Name = "Part Time Mattino",
                    StartTime = TimeSpan.Parse("09:00:00"),
                    EndTime = TimeSpan.Parse("13:00:00"),
                    WeekDay = "0-2-4"
                },
                new Timesheet
                {
                    Id = Guid.Parse("4e2df5a2-0aa6-405e-9865-02e3692ef0c1"), // Forced to specific Guid for tests
                    Name = "Part Time Verticale",
                    StartTime = TimeSpan.Parse("09:00:00"),
                    EndTime = TimeSpan.Parse("18:00:00"),
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
