using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplePoll.CoreServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var initialData = @"
                declare @PollId INT
                declare @PollElementId INT

                INSERT INTO [Polls].[Polls] ([Name])
	                VALUES ('Testowy formularz')

                SET @PollId = SCOPE_IDENTITY()

                INSERT INTO [Polls].[Elements] ([Name], [Type], [Subtype], [Min], [Max], [PollId])
	                VALUES
		                ('Co myślisz o formularzu?', 0, NULL, NULL, 250, @PollId),
		                ('Podaj Twój dochód za poprzedni rok', 1, 2, NULL, NULL, @PollId),
		                ('Czy lubisz książki?', 2, NULL, NULL, NULL, @PollId)

                SET @PollElementId = SCOPE_IDENTITY()

                INSERT INTO [Polls].[Values] ([Name], [PollElementId])
	                VALUES
		                ('TAK', @PollElementId),
		                ('NIE', @PollElementId)

                INSERT INTO [Polls].[Elements] ([Name], [Type], [Subtype], [Min], [Max], [PollId])
	                VALUES
		                ('Wybierz ulubiony kolor', 3, NULL, NULL, NULL, @PollId)

                SET @PollElementId = SCOPE_IDENTITY()

                INSERT INTO [Polls].[Values] ([Name], [PollElementId])
	                VALUES
		                ('Czerwony', @PollElementId),
		                ('Zielony', @PollElementId),
		                ('Niebieski', @PollElementId),
		                ('Fioletowy', @PollElementId),
		                ('Różowy', @PollElementId),
		                ('Żółty', @PollElementId)

                INSERT INTO [Polls].[Elements] ([Name], [Type], [Subtype], [Min], [Max], [PollId])
	                VALUES
		                ('Wybierz ulubiony dzień tygodnia', 4, NULL, NULL, NULL, @PollId)

                SET @PollElementId = SCOPE_IDENTITY()

                INSERT INTO [Polls].[Values] ([Name], [PollElementId])
	                VALUES
		                ('Poniedziałek', @PollElementId),
		                ('Wtorek', @PollElementId),
		                ('Środa', @PollElementId),
		                ('Czwartek', @PollElementId),
		                ('Piątek', @PollElementId),
		                ('Sobota', @PollElementId),
		                ('Niedziela', @PollElementId)
            ";
            migrationBuilder.Sql(initialData);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var cleanup = @"
                    DELETE FROM [Polls].[Values] WHERE 1=1
                    DELETE FROM [Polls].[Elements] WHERE 1=1
                    DELETE FROM [Polls].[Polls] WHERE 1=1
                ";
            migrationBuilder.Sql(cleanup);
        }
    }
}
