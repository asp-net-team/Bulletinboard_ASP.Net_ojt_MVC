using Bulletinboard.Web.Models;
using System;
using System.Collections.Generic;

namespace Bulletinboard.Web.DAL
{
    public class BulletinboardInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BulletinboardContext>
    {
        protected override void Seed(BulletinboardContext context)
        {
            var users = new List<User>
            {
                new User{
                    Name="Soe Htet Aung",Email="sha@gmail.com",Password="1111111",Type=1,
                    Birthday=DateTime.Parse("1994-09-01"),Address="Yangon",Profile="1.jpg"
                },
                new User{
                    Name="Than Zin Oo",Email="tzo@gmail.com",Password="2222222",Type=0,
                    Birthday=DateTime.Parse("2000-09-01"),Address="Mandalay",Profile="2.jpg"
                },
                new User{
                    Name="Chit Su",Email="cssh@gmail.com",Password="3333333",Type=1,
                    Birthday=DateTime.Parse("1998-09-01"),Address="Bago",Profile="3.jpg"
                },
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }
    }
}