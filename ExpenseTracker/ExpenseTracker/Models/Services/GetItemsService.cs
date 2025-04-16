using MongoDB.Driver;

namespace ExpenseTracker.Models.Services
{
    public class GetItemService
    {
        public async Task<List<Expense>> GetUserExpensesAsync(string userId)
        {
            var filter = Builders<Expense>.Filter.And(Builders<Expense>.Filter.Eq("UserId", userId));
            return await MongoManipulator.GetAllObjectsByFilter<Expense>(filter);
        }

        public async Task<List<Income>> GetUserIncomesAsync(string userId)
        {
            var filter = Builders<Income>.Filter.And(Builders<Income>.Filter.Eq("UserId", userId));
            return await MongoManipulator.GetAllObjectsByFilter(filter);
        }

    }
}
