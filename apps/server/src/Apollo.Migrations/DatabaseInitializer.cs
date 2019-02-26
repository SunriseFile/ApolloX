using FluentMigrator.Runner;

namespace Apollo.Migrations
{
    internal class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly IMigrationRunner _migrationRunner;

        public DatabaseInitializer(IMigrationRunner migrationRunner)
        {
            _migrationRunner = migrationRunner;
        }

        public void Initialize()
        {
            _migrationRunner.MigrateUp();
        }
    }
}
