dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Startup.WinForms --context AppDbContext


1. dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Startup.WinForms --context AppDbContext
2. Проверить созданную миграцию
3. Запустить WinForms-приложение
4. Database.Migrate() создаст/обновит drcost.sqlite