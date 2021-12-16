using System.Collections.Generic;
using System.Threading.Tasks;
using iTechArt.iTechQuiz.Domain;
using iTechArt.iTechQuiz.Repositories.Context;
using iTechArt.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.iTechQuiz.Repositories.Repositories
{
    public class UserRepository : IRepositoryAsync<User>
    {
        private iTechQuizContext _context;
        public UserRepository(iTechQuizContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void CreateAsync(User item)
        {
            _context.Users.AddAsync(item);
        }

        public void UpdateAsync(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async void DeleteAsync(int id)
        {
            User user = await _context.Users.FindAsync(id);
            if(user != null)
                _context.Users.Remove(user);
        }

        public void SaveAsync()
        {
            _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}
