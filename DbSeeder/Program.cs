using CommBank.Models;
using MongoDB.Driver;
using System.Text.Json;

var connectionString = "mongodb+srv://Seifudin:pOLCEw0Ej8E7iYul@cluster0.u5kzeth.mongodb.net/?appName=Cluster0";
var mongoClient = new MongoClient(connectionString);
var mongoDatabase = mongoClient.GetDatabase("CommBank");

Console.WriteLine("Seeding database...");

var seedDataDir = Path.Combine(Directory.GetCurrentDirectory(), "..", "CommBank-Server", "DataSeed");

// Seed Users
var usersPath = Path.Combine(seedDataDir, "users.json");
var usersJson = await File.ReadAllTextAsync(usersPath);
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
var accountsPath = Path.Combine(seedDataDir, "accounts.json");
var accountsJson = await File.ReadAllTextAsync(accountsPath);
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
var goalsPath = Path.Combine(seedDataDir, "goals.json");
var goalsJson = await File.ReadAllTextAsync(goalsPath);
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
var tagsPath = Path.Combine(seedDataDir, "tags.json");
var tagsJson = await File.ReadAllTextAsync(tagsPath);
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
var transactionsPath = Path.Combine(seedDataDir, "transactions.json");
var transactionsJson = await File.ReadAllTextAsync(transactionsPath);
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