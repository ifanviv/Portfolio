namespace WebCourseWorkApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Security.Claims;
    using System.Linq;
    using WebCourseWorkApplication.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<WebCourseWorkApplication.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebCourseWorkApplication.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            AddUsers(context);
            AddClaims(context);
        }
        void AddUsers(WebCourseWorkApplication.Models.ApplicationDbContext context)
        {
            var user1 = new ApplicationUser { UserName = "lecturer1@email.com" };
            var user2 = new ApplicationUser { UserName = "Student1@email.com" };
            var user3 = new ApplicationUser { UserName = "Student2@email.com" };
            var user4 = new ApplicationUser { UserName = "Student3@email.com" };
            var user5 = new ApplicationUser { UserName = "Student4@email.com" };
            var user6 = new ApplicationUser { UserName = "Student5@email.com" };
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            um.Create(user1, "password");
            um.Create(user2, "password");
            um.Create(user3, "password");
            um.Create(user4, "password");
            um.Create(user5, "password");
            um.Create(user6, "password");
        }
        void AddClaims(WebCourseWorkApplication.Models.ApplicationDbContext context)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            Claim canCreate = new Claim("canPOST","createOrModifyValue");
            um.AddClaim(context.Users.FirstOrDefault(x=>x.UserName == "lecturer1@email.com").Id, canCreate);
        }
    }
}
