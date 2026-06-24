using CommBank.Models;
using MongoDB.Driver;
using System.Text.Json;

var connectionString = "mongodb+srv://Seifudin:pOLCEw0Ej8E7iYul@cluster0.u5kzeth.mongodb.net/?appName=Cluster0";
var mongoClient = new MongoClient(connectionString);
var mongoDatabase = mongoClient.GetDatabase("CommBank");

Console.WriteLine("Seeding database...");

var basePath = Path.Combine(Directory.GetCurrentDirectory(), "DataSeed");

// Seed Users
var usersJson = await File.ReadAllTextAsync(Path.Combine(basePath, "users.json"));
var users = JsonSerializer.Deserialize<List<User>>(usersJson);
if (users != null)
{
    var usersCollection = mongoDatabase.GetCollection<User>("Users");
    foreach (var user in users)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword("password123");
        await usersCollection.InsertOneAsync(user);
        Console.WriteLine($"Seeded user: {user.Name}");
    }
}

// Seed Accounts
var accountsJson = await File.ReadAllTextAsync(Path.Combine(basePath, "accounts.json"));
var accounts = JsonSerializer.Deserialize<List<Account>>(accountsJson);
if (accounts != null)
{
    var accountsCollection = mongoDatabase.GetCollection<Account>("Accounts");
    foreach (var account in accounts)
    {
        await accountsCollection.InsertOneAsync(account);
        Console.WriteLine($"Seeded account: {account.Name}");
    }
}

// Seed Goals
var goalsJson = await File.ReadAllTextAsync(Path.Combine(basePath, "goals.json"));
var goals = JsonSerializer.Deserialize<List<Goal>>(goalsJson);
if (goals != null)
{
    var goalsCollection = mongoDatabase.GetCollection<Goal>("Goals");
    foreach (var goal in goals)
    {
        await goalsCollection.InsertOneAsync(goal);
        Console.WriteLine($"Seeded goal: {goal.Name}");
    }
}

// Seed Tags
var tagsJson = await File.ReadAllTextAsync(Path.Combine(basePath, "tags.json"));
var tags = JsonSerializer.Deserialize<List<Tag>>(tagsJson);
if (tags != null)
{
    var tagsCollection = mongoDatabase.GetCollection<Tag>("Tags");
    foreach (var tag in tags)
    {
        await tagsCollection.InsertOneAsync(tag);
        Console.WriteLine($"Seeded tag: {tag.Name}");
    }
}

// Seed Transactions
var transactionsJson = await File.ReadAllTextAsync(Path.Combine(basePath, "transactions.json"));
var transactions = JsonSerializer.Deserialize<List<Transaction>>(transactionsJson);
if (transactions != null)
{
    var transactionsCollection = mongoDatabase.GetCollection<Transaction>("Transactions");
    foreach (var transaction in transactions)
    {
        await transactionsCollection.InsertOneAsync(transaction);
        Console.WriteLine($"Seeded transaction: {transaction.Description}");
    }
}

Console.WriteLine("Database seeding complete!");