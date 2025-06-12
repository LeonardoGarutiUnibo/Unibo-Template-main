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
            context.AbsenceEvents.AddRange(
                new AbsenceEvent{
                    Id = Guid.Parse("e4ae8166-49f9-4369-a129-ae79f8f8757d"),
                    UserId = Guid.Parse("a89cfb3e-cac6-4721-b51c-3a97ecfa4695"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-03 03:51:43"),
                    EndEventDate = DateTime.Parse("2025-06-05 16:50:43"),
                    EventType = "Ferie Intera",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("cd82fd90-c458-4ea1-9db0-879a5a6a361c"),
                    UserId = Guid.Parse("a89cfb3e-cac6-4721-b51c-3a97ecfa4695"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-28 10:34:22"),
                    EndEventDate = DateTime.Parse("2025-06-29 10:36:22"),
                    EventType = "Permessi Mezza",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("f9b9ffdf-4726-4d20-a411-c6f7ec295b7b"),
                    UserId = Guid.Parse("8b0e308e-59fa-4c28-9bfc-1c2878e2ffb2"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-04 07:42:13"),
                    EndEventDate = DateTime.Parse("2025-06-04 18:01:13"),
                    EventType = "smartworking",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("064734cb-1fa7-4900-adb4-456383f7073b"),
                    UserId = Guid.Parse("8b0e308e-59fa-4c28-9bfc-1c2878e2ffb2"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-05 04:20:55"),
                    EndEventDate = DateTime.Parse("2025-06-07 03:18:55"),
                    EventType = "Permessi Intera",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("61eeeb3c-aebf-402f-aef4-8ab0434c467b"),
                    UserId = Guid.Parse("8b0e308e-59fa-4c28-9bfc-1c2878e2ffb2"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-28 22:21:19"),
                    EndEventDate = DateTime.Parse("2025-06-30 08:05:19"),
                    EventType = "Ferie Mezza",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("e67c7c2d-8dd6-45ca-ba1e-d84c2438ca7b"),
                    UserId = Guid.Parse("fa18d9ce-3cd9-4d55-82e9-1a19494f174b"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-02 18:05:18"),
                    EndEventDate = DateTime.Parse("2025-06-05 13:34:18"),
                    EventType = "smartworking",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("f85fee12-067b-41dd-9cc2-eee65e25f5ed"),
                    UserId = Guid.Parse("fa18d9ce-3cd9-4d55-82e9-1a19494f174b"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-15 08:51:38"),
                    EndEventDate = DateTime.Parse("2025-06-15 13:48:38"),
                    EventType = "Permessi Mezza",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("128ff0d3-46d7-4991-adf1-b9ae082c1a21"),
                    UserId = Guid.Parse("3f6f5f91-3ac0-44e4-bd77-72a9b24534f5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-13 10:52:08"),
                    EndEventDate = DateTime.Parse("2025-06-14 20:55:08"),
                    EventType = "Permessi Mezza",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("987317b7-939a-4ff0-aec6-f3e59c62a87a"),
                    UserId = Guid.Parse("3f6f5f91-3ac0-44e4-bd77-72a9b24534f5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-12 05:16:40"),
                    EndEventDate = DateTime.Parse("2025-06-14 17:32:40"),
                    EventType = "Ferie Mezza",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("75f09dc3-61e3-43ff-b388-115f782896a6"),
                    UserId = Guid.Parse("3f6f5f91-3ac0-44e4-bd77-72a9b24534f5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-03 15:41:15"),
                    EndEventDate = DateTime.Parse("2025-06-04 20:47:15"),
                    EventType = "Ferie Mezza",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("c09e905a-d1cf-46a0-a38b-f74639e56df3"),
                    UserId = Guid.Parse("3f6f5f91-3ac0-44e4-bd77-72a9b24534f5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-10 03:59:23"),
                    EndEventDate = DateTime.Parse("2025-06-11 17:17:23"),
                    EventType = "smartworking",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("d611841b-72e1-401d-8fe0-dfa2d95bd146"),
                    UserId = Guid.Parse("f62487cd-b3e8-4fa7-a2cb-6f879d1c9a99"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-04 02:56:39"),
                    EndEventDate = DateTime.Parse("2025-06-06 12:03:39"),
                    EventType = "Permessi Intera",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("6f5f466a-04f9-40d9-a465-d6c79363dcd8"),
                    UserId = Guid.Parse("f62487cd-b3e8-4fa7-a2cb-6f879d1c9a99"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-20 17:35:07"),
                    EndEventDate = DateTime.Parse("2025-06-23 06:00:07"),
                    EventType = "Ferie Mezza",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("2d42a91a-7073-4c55-b4ec-c92f892c9408"),
                    UserId = Guid.Parse("e9130fb8-39e7-4c44-a33f-9e0cc4d144c5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-03 15:20:20"),
                    EndEventDate = DateTime.Parse("2025-06-04 20:31:20"),
                    EventType = "Ferie Intera",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("865e0e8d-cf12-4e53-943c-8d9798578553"),
                    UserId = Guid.Parse("e9130fb8-39e7-4c44-a33f-9e0cc4d144c5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-07 23:36:51"),
                    EndEventDate = DateTime.Parse("2025-06-10 01:25:51"),
                    EventType = "smartworking",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("2d859509-6aa6-4696-b18d-08ea19fd3bc8"),
                    UserId = Guid.Parse("e9130fb8-39e7-4c44-a33f-9e0cc4d144c5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-09 06:40:10"),
                    EndEventDate = DateTime.Parse("2025-06-10 03:38:10"),
                    EventType = "Permessi Intera",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("96d10539-62dd-463a-ade9-1cea7aecbceb"),
                    UserId = Guid.Parse("e9130fb8-39e7-4c44-a33f-9e0cc4d144c5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-08 08:35:41"),
                    EndEventDate = DateTime.Parse("2025-06-08 17:29:41"),
                    EventType = "Permessi Intera",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("eced185e-aa94-4483-a33e-03baeafae5c4"),
                    UserId = Guid.Parse("02e3d368-78c5-4b3e-84dc-b8a9c5971cc5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-28 18:51:11"),
                    EndEventDate = DateTime.Parse("2025-06-29 11:56:11"),
                    EventType = "Permessi Mezza",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("c014c075-c279-401e-98d4-bfbd1532f899"),
                    UserId = Guid.Parse("02e3d368-78c5-4b3e-84dc-b8a9c5971cc5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-06 18:32:11"),
                    EndEventDate = DateTime.Parse("2025-06-07 14:09:11"),
                    EventType = "smartworking",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("eecc7124-fd73-4c33-8466-ae5770f099db"),
                    UserId = Guid.Parse("02e3d368-78c5-4b3e-84dc-b8a9c5971cc5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-20 07:42:45"),
                    EndEventDate = DateTime.Parse("2025-06-20 17:09:45"),
                    EventType = "Ferie Intera",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("a2b35ac5-41ca-42bd-b723-7f1f70a0e3b9"),
                    UserId = Guid.Parse("bdfb79f3-cc8d-4f39-90ff-54f0d18e6be5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-25 12:09:30"),
                    EndEventDate = DateTime.Parse("2025-06-27 23:59:30"),
                    EventType = "Ferie Mezza",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("e27dd22c-802a-494f-b5b6-b4f5e74c1221"),
                    UserId = Guid.Parse("bdfb79f3-cc8d-4f39-90ff-54f0d18e6be5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-18 14:26:18"),
                    EndEventDate = DateTime.Parse("2025-06-19 18:41:18"),
                    EventType = "smartworking",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("1cb559fb-259c-4d34-acee-1a8648bb7d04"),
                    UserId = Guid.Parse("bdfb79f3-cc8d-4f39-90ff-54f0d18e6be5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-13 04:26:07"),
                    EndEventDate = DateTime.Parse("2025-06-13 14:52:07"),
                    EventType = "smartworking",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("3ede2e77-938f-4c7a-ba74-d48593085d6f"),
                    UserId = Guid.Parse("bdfb79f3-cc8d-4f39-90ff-54f0d18e6be5"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-26 00:37:57"),
                    EndEventDate = DateTime.Parse("2025-06-26 22:57:57"),
                    EventType = "Ferie Intera",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("244e3373-d2be-4e67-a48a-3320d64fe99e"),
                    UserId = Guid.Parse("1e743c1a-2264-4626-ae19-e097d214f6e3"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-29 20:13:49"),
                    EndEventDate = DateTime.Parse("2025-06-29 22:10:49"),
                    EventType = "Permessi Mezza",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("2f0ceffd-8bdb-4076-b276-85cd3fa70af6"),
                    UserId = Guid.Parse("1e743c1a-2264-4626-ae19-e097d214f6e3"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-12 19:18:51"),
                    EndEventDate = DateTime.Parse("2025-06-12 20:32:51"),
                    EventType = "Permessi Mezza",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("5a24962e-9ead-402d-8a73-00d4113f9674"),
                    UserId = Guid.Parse("1e743c1a-2264-4626-ae19-e097d214f6e3"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-15 14:43:31"),
                    EndEventDate = DateTime.Parse("2025-06-16 14:16:31"),
                    EventType = "Ferie Intera",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("57a35540-450e-4173-9ff8-ec2211a29110"),
                    UserId = Guid.Parse("1e743c1a-2264-4626-ae19-e097d214f6e3"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-12 12:26:33"),
                    EndEventDate = DateTime.Parse("2025-06-12 22:13:33"),
                    EventType = "Ferie Mezza",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("a6b3591b-7c5a-4d82-9558-f810ae4b0177"),
                    UserId = Guid.Parse("ef74c64d-1d1b-440e-8df2-3432b690ff5d"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-16 21:07:32"),
                    EndEventDate = DateTime.Parse("2025-06-17 23:18:32"),
                    EventType = "smartworking",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("7ec705c0-da55-4e65-9c17-ad81230f895a"),
                    UserId = Guid.Parse("ef74c64d-1d1b-440e-8df2-3432b690ff5d"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-02 15:58:43"),
                    EndEventDate = DateTime.Parse("2025-06-04 23:01:43"),
                    EventType = "Ferie Intera",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("a3a2327d-0071-4059-8873-ede184ac38bc"),
                    UserId = Guid.Parse("621ef3b7-df3d-47f6-8905-9490fd47be44"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-03 02:39:32"),
                    EndEventDate = DateTime.Parse("2025-06-05 19:25:32"),
                    EventType = "Ferie Mezza",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("1345efe6-8701-4d6b-bddf-c89880fdc4dd"),
                    UserId = Guid.Parse("621ef3b7-df3d-47f6-8905-9490fd47be44"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-11 02:34:45"),
                    EndEventDate = DateTime.Parse("2025-06-11 11:27:45"),
                    EventType = "smartworking",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("a7fce348-2db2-4c72-8dff-331816f5a79d"),
                    UserId = Guid.Parse("a67ddf3b-4be4-4c36-b62c-69bb23cb62b1"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-04 14:34:48"),
                    EndEventDate = DateTime.Parse("2025-06-06 06:05:48"),
                    EventType = "Permessi Intera",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("1ab8d9d8-86c4-4bf6-8fdd-e70d070a24f8"),
                    UserId = Guid.Parse("a67ddf3b-4be4-4c36-b62c-69bb23cb62b1"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-10 18:03:05"),
                    EndEventDate = DateTime.Parse("2025-06-13 15:53:05"),
                    EventType = "Permessi Mezza",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("27b41a90-bfa4-479e-a0ba-d348b3284be0"),
                    UserId = Guid.Parse("a67ddf3b-4be4-4c36-b62c-69bb23cb62b1"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-10 13:54:28"),
                    EndEventDate = DateTime.Parse("2025-06-12 15:19:28"),
                    EventType = "Permessi Intera",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("da703334-d50d-4723-a4dd-898bd7d5b608"),
                    UserId = Guid.Parse("bd031cf4-34a2-453e-8739-d03eb6eb960c"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-04 06:09:40"),
                    EndEventDate = DateTime.Parse("2025-06-07 04:00:40"),
                    EventType = "Ferie Mezza",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("59ab6d3a-e602-4a63-8ed1-f2a28bfc2daf"),
                    UserId = Guid.Parse("bd031cf4-34a2-453e-8739-d03eb6eb960c"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-02 18:39:14"),
                    EndEventDate = DateTime.Parse("2025-06-03 05:12:14"),
                    EventType = "Permessi Intera",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("9399d5f0-85ae-46ff-8b1c-e40e8138d41c"),
                    UserId = Guid.Parse("bd031cf4-34a2-453e-8739-d03eb6eb960c"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-18 14:03:44"),
                    EndEventDate = DateTime.Parse("2025-06-21 11:47:44"),
                    EventType = "Permessi Intera",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("2dc4d33b-9411-4ce9-a5e5-1776c456f6c0"),
                    UserId = Guid.Parse("bd031cf4-34a2-453e-8739-d03eb6eb960c"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-12 12:39:55"),
                    EndEventDate = DateTime.Parse("2025-06-13 00:48:55"),
                    EventType = "Permessi Intera",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("cdd1e0df-9675-480b-a2b5-5e8dd66b78d9"),
                    UserId = Guid.Parse("d6a888ff-b1ff-4960-9f3b-65ed2383de61"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-19 01:35:30"),
                    EndEventDate = DateTime.Parse("2025-06-19 16:46:30"),
                    EventType = "Ferie Mezza",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("14b3f0e2-8f46-4807-b95d-64487b01f9e5"),
                    UserId = Guid.Parse("d6a888ff-b1ff-4960-9f3b-65ed2383de61"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-12 21:38:09"),
                    EndEventDate = DateTime.Parse("2025-06-14 15:44:09"),
                    EventType = "Ferie Intera",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("666ed979-4f15-4644-a4d2-005180723078"),
                    UserId = Guid.Parse("d6a888ff-b1ff-4960-9f3b-65ed2383de61"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-17 17:59:45"),
                    EndEventDate = DateTime.Parse("2025-06-19 03:44:45"),
                    EventType = "Permessi Intera",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("0a2403d6-94fe-41fd-aed1-210379467dfe"),
                    UserId = Guid.Parse("d6a888ff-b1ff-4960-9f3b-65ed2383de61"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-28 04:44:08"),
                    EndEventDate = DateTime.Parse("2025-07-01 03:23:08"),
                    EventType = "Permessi Intera",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("e03fc6d3-e69f-4c97-b60d-d27db4dfe470"),
                    UserId = Guid.Parse("7794622c-88ae-49fd-a20a-0dd3a46a6d47"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-23 12:25:55"),
                    EndEventDate = DateTime.Parse("2025-06-24 03:01:55"),
                    EventType = "Permessi Intera",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("eadaf28c-d3d2-41b4-92c0-62ed3b899290"),
                    UserId = Guid.Parse("7794622c-88ae-49fd-a20a-0dd3a46a6d47"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-20 09:23:56"),
                    EndEventDate = DateTime.Parse("2025-06-22 23:30:56"),
                    EventType = "Ferie Mezza",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("0d71fc8a-f439-4bee-80a7-0a55fe08be60"),
                    UserId = Guid.Parse("7794622c-88ae-49fd-a20a-0dd3a46a6d47"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-25 23:56:19"),
                    EndEventDate = DateTime.Parse("2025-06-26 20:44:19"),
                    EventType = "Ferie Intera",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("fc1cc580-afd2-4dd6-989b-6acf01b92c92"),
                    UserId = Guid.Parse("7794622c-88ae-49fd-a20a-0dd3a46a6d47"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-25 09:44:25"),
                    EndEventDate = DateTime.Parse("2025-06-26 17:22:25"),
                    EventType = "Ferie Intera",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("1d6be36c-ea0b-48f8-88a9-a1a18f422f3c"),
                    UserId = Guid.Parse("e252dbf9-f44a-4374-a180-5b1ae3d2f3f1"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-09 22:35:13"),
                    EndEventDate = DateTime.Parse("2025-06-10 16:19:13"),
                    EventType = "Permessi Mezza",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("41e912ac-7647-4e01-b22f-f022c63b04b9"),
                    UserId = Guid.Parse("e252dbf9-f44a-4374-a180-5b1ae3d2f3f1"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-03 06:58:47"),
                    EndEventDate = DateTime.Parse("2025-06-04 20:24:47"),
                    EventType = "Permessi Intera",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("01b676a0-c5a1-4b2c-aafe-c2c881ebc0c5"),
                    UserId = Guid.Parse("e252dbf9-f44a-4374-a180-5b1ae3d2f3f1"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-20 22:10:06"),
                    EndEventDate = DateTime.Parse("2025-06-22 23:14:06"),
                    EventType = "Ferie Intera",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("158f6569-89d3-4c43-911d-be00dfd7381e"),
                    UserId = Guid.Parse("0f51e242-c419-4ee9-9a9f-38b7e41b76f1"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-11 16:04:02"),
                    EndEventDate = DateTime.Parse("2025-06-12 22:57:02"),
                    EventType = "Permessi Intera",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("8e73acc1-21b7-4e37-9678-bd8d5cc999f3"),
                    UserId = Guid.Parse("0f51e242-c419-4ee9-9a9f-38b7e41b76f1"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-11 22:46:09"),
                    EndEventDate = DateTime.Parse("2025-06-14 04:36:09"),
                    EventType = "Permessi Mezza",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("fe7ac5bc-b641-4885-82f5-d9f21e8e5ff8"),
                    UserId = Guid.Parse("3a13ec30-ec8d-4de3-b053-6f07b5c8e4c4"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-10 17:53:48"),
                    EndEventDate = DateTime.Parse("2025-06-12 12:09:48"),
                    EventType = "smartworking",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("554ec0ea-a7f0-49b0-89a7-0d7c8e3a0c24"),
                    UserId = Guid.Parse("3a13ec30-ec8d-4de3-b053-6f07b5c8e4c4"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-13 03:21:01"),
                    EndEventDate = DateTime.Parse("2025-06-14 13:08:01"),
                    EventType = "Permessi Mezza",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("d82316f4-38f8-4b49-b9a3-353cee1e043f"),
                    UserId = Guid.Parse("3a13ec30-ec8d-4de3-b053-6f07b5c8e4c4"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-29 09:58:46"),
                    EndEventDate = DateTime.Parse("2025-07-02 10:34:46"),
                    EventType = "Permessi Intera",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("33325005-b14e-48fb-97a0-0131b71e6da8"),
                    UserId = Guid.Parse("552ac54b-dc6e-472e-8f7a-c837d65b51ff"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-18 01:49:55"),
                    EndEventDate = DateTime.Parse("2025-06-19 17:16:55"),
                    EventType = "Ferie Mezza",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("a045f553-9e94-47a7-91a0-b752c114f066"),
                    UserId = Guid.Parse("552ac54b-dc6e-472e-8f7a-c837d65b51ff"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-25 10:02:19"),
                    EndEventDate = DateTime.Parse("2025-06-26 15:12:19"),
                    EventType = "Permessi Mezza",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("ce6fc0cd-e679-4949-be7b-d732b08112fd"),
                    UserId = Guid.Parse("552ac54b-dc6e-472e-8f7a-c837d65b51ff"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-11 20:44:57"),
                    EndEventDate = DateTime.Parse("2025-06-13 19:58:57"),
                    EventType = "Ferie Mezza",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("aca79acd-e622-4b1e-abb7-f92a270256f1"),
                    UserId = Guid.Parse("a8f86f82-c17e-4edb-ae8d-ef4e70c331c4"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-14 02:16:59"),
                    EndEventDate = DateTime.Parse("2025-06-17 01:26:59"),
                    EventType = "Ferie Intera",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("40d857f9-5f0d-4518-a3be-b9a052eb8d0e"),
                    UserId = Guid.Parse("a8f86f82-c17e-4edb-ae8d-ef4e70c331c4"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-21 17:48:57"),
                    EndEventDate = DateTime.Parse("2025-06-24 16:50:57"),
                    EventType = "smartworking",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("99210d9a-3032-4213-a289-a5701bf9eda1"),
                    UserId = Guid.Parse("a8f86f82-c17e-4edb-ae8d-ef4e70c331c4"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-23 17:09:03"),
                    EndEventDate = DateTime.Parse("2025-06-25 12:33:03"),
                    EventType = "Permessi Intera",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("8f5e1000-5369-4e1c-b941-f9351b129a72"),
                    UserId = Guid.Parse("b9e022db-2a56-4a3c-bf1d-185f3e6f9c77"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-29 00:43:24"),
                    EndEventDate = DateTime.Parse("2025-06-29 09:12:24"),
                    EventType = "smartworking",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("b085aa9c-e111-4990-b4ca-00807c2dfafb"),
                    UserId = Guid.Parse("b9e022db-2a56-4a3c-bf1d-185f3e6f9c77"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-22 03:42:10"),
                    EndEventDate = DateTime.Parse("2025-06-24 15:02:10"),
                    EventType = "Permessi Mezza",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("7ebab9fa-a971-49ba-8555-86c24663cdd7"),
                    UserId = Guid.Parse("b9e022db-2a56-4a3c-bf1d-185f3e6f9c77"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-18 18:52:05"),
                    EndEventDate = DateTime.Parse("2025-06-20 03:41:05"),
                    EventType = "Ferie Mezza",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("b511c3b8-0fc1-4887-8eeb-88bf4e02f0f5"),
                    UserId = Guid.Parse("7baf7af2-4d4c-4d41-acc3-8e867167f879"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-21 12:11:28"),
                    EndEventDate = DateTime.Parse("2025-06-21 23:43:28"),
                    EventType = "Ferie Intera",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("d8e2975e-d910-44a4-b806-4a6802cc6011"),
                    UserId = Guid.Parse("7baf7af2-4d4c-4d41-acc3-8e867167f879"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-22 23:43:09"),
                    EndEventDate = DateTime.Parse("2025-06-25 02:17:09"),
                    EventType = "Ferie Intera",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("2f554861-517a-4284-bc5a-6d2861b47212"),
                    UserId = Guid.Parse("7baf7af2-4d4c-4d41-acc3-8e867167f879"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-06 05:41:00"),
                    EndEventDate = DateTime.Parse("2025-06-07 10:02:00"),
                    EventType = "Ferie Intera",
                    EventState = "Approvato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("9126bbd4-0898-462d-b655-c77d49864394"),
                    UserId = Guid.Parse("cd93a776-f3eb-4b39-87c9-1447bb80ff91"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-25 08:45:06"),
                    EndEventDate = DateTime.Parse("2025-06-26 22:06:06"),
                    EventType = "Ferie Mezza",
                    EventState = "Rifiutato"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("7c636840-1c35-4d02-ba6b-8bd8d9fe1f63"),
                    UserId = Guid.Parse("cd93a776-f3eb-4b39-87c9-1447bb80ff91"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-19 15:46:23"),
                    EndEventDate = DateTime.Parse("2025-06-21 16:32:23"),
                    EventType = "Ferie Intera",
                    EventState = "Pending"
                },
                new AbsenceEvent{
                    Id = Guid.Parse("3f8c192c-17f4-4c19-aa9f-c901ae6b9a36"),
                    UserId = Guid.Parse("cd93a776-f3eb-4b39-87c9-1447bb80ff91"),
                    RequestDate = DateTime.Parse("2025-06-09 02:59:41"),
                    StartEventDate = DateTime.Parse("2025-06-08 19:47:31"),
                    EndEventDate = DateTime.Parse("2025-06-11 08:41:31"),
                    EventType = "smartworking",
                    EventState = "Rifiutato"
                }

            );
            context.AbsenceEvents.AddRange();
            context.SaveChanges();
        }

    }
}
