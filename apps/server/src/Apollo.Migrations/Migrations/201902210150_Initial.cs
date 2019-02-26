using Apollo.Core;
using Apollo.Core.Models;

using FluentMigrator;

namespace Apollo.Migrations.Migrations
{
    [Migration(2019_02_21_01_50)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table(TableNames.Users)
                  .WithIdColumn()
                  .WithColumn(nameof(AppUser.UserName)).AsString(64).NotNullable().Unique()
                  .WithColumn(nameof(AppUser.DisplayUserName)).AsString(64).NotNullable().Unique()
                  .WithColumn(nameof(AppUser.FullName)).AsString(64).NotNullable()
                  .WithColumn(nameof(AppUser.PasswordHash)).AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table(TableNames.Users);
        }
    }
}
