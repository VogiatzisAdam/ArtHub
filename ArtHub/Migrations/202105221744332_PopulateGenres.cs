namespace ArtHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GENRES (Id, Name) VALUES (1,'Rock')");
            Sql("INSERT INTO GENRES (Id, Name) VALUES (2,'Pop')");
            Sql("INSERT INTO GENRES (Id, Name) VALUES (3,'Laika')");
            Sql("INSERT INTO GENRES (Id, Name) VALUES (4,'Rebetika')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM GENRES WHERE Id IN (1,2,3,4)");
        }
    }
}
